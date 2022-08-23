using System.Globalization;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using J2N.Collections.Generic;
using Limbo.Umbraco.Rte.Processors;
using Newtonsoft.Json.Linq;
using Umbraco.Cms.Core.Macros;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Macros;
using Umbraco.Extensions;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    public class RteValueConverter : SimpleTinyMceValueConverter {

        private readonly HtmlImageSourceParser _imageSourceParser;
        private readonly RteHtmlProcessorCollection _processors;
        private readonly HtmlLocalLinkParser _linkParser;
        private readonly IMacroRenderer _macroRenderer;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly HtmlUrlParser _urlParser;

        public RteValueConverter(IUmbracoContextAccessor umbracoContextAccessor, IMacroRenderer macroRenderer,
            HtmlLocalLinkParser linkParser, HtmlUrlParser urlParser, HtmlImageSourceParser imageSourceParser, RteHtmlProcessorCollection processors) {
            _umbracoContextAccessor = umbracoContextAccessor;
            _macroRenderer = macroRenderer;
            _linkParser = linkParser;
            _urlParser = urlParser;
            _imageSourceParser = imageSourceParser;
            _processors = processors;
        }

        public override bool IsConverter(IPublishedPropertyType propertyType)
            => propertyType.EditorAlias == RtePropertyEditor.EditorAlias;

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) =>

            // because that version of RTE converter parses {locallink} and executes macros, its value has
            // to be cached at the published snapshot level, because we have no idea what the macros may depend on actually.
            PropertyCacheLevel.Snapshot;

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {

            RteConfiguration config = propertyType.DataType.ConfigurationAs<RteConfiguration>();

            string converted = Convert(owner, propertyType, inter, preview, config);
            return new HtmlEncodedString(converted ?? string.Empty);
        }

        // NOT thread-safe over a request because it modifies the
        // global UmbracoContext.Current.InPreviewMode status. So it
        // should never execute in // over the same UmbracoContext with
        // different preview modes.
        private string RenderRteMacros(string source, bool preview) {
            IUmbracoContext umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            using (umbracoContext.ForcedPreview(preview)) // force for macro rendering
            {
                var sb = new StringBuilder();

                MacroTagParser.ParseMacros(
                    source,

                    // callback for when text block is found
                    textBlock => sb.Append(textBlock),

                    // callback for when macro syntax is found
                    (macroAlias, macroAttributes) => sb.Append(_macroRenderer.RenderAsync(
                        macroAlias,
                        umbracoContext.PublishedRequest?.PublishedContent,

                        // needs to be explicitly casted to Dictionary<string, object>
                        macroAttributes.ConvertTo(x => (string) x, x => x)!).GetAwaiter().GetResult().Text));

                return sb.ToString();
            }
        }

        private string Convert(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview, RteConfiguration config) {
            if (source == null) {
                return null;
            }

            // Parse and load the selected processors
            List<IRteHtmlProcessor> processors = new();
            if (config is { Processors: JArray processorReferences }) {
                foreach (JToken token in processorReferences) {
                    if (token.Type != JTokenType.String) continue;
                    string typeName = token.Value<string>()!;
                    if (_processors.TryGet(typeName, out IRteHtmlProcessor processor)) {
                        processors.Add(processor!);
                    } else {
                        // TODO: Log error as processor was not found
                    }
                }
            }




            var sourceString = source.ToString()!;

            // ensures string is parsed for {localLink} and URLs and media are resolved correctly
            sourceString = _linkParser.EnsureInternalLinks(sourceString, preview);
            sourceString = _urlParser.EnsureUrls(sourceString);
            sourceString = _imageSourceParser.EnsureImageSources(sourceString);

            // ensure string is parsed for macros and macros are executed correctly
            sourceString = RenderRteMacros(sourceString, preview);

            foreach (var processor in processors) {
                sourceString = processor.PreProcess(owner, propertyType, preview, sourceString);
            }

            // find and remove the rel attributes used in the Umbraco UI from img tags
            var doc = new HtmlDocument();
            doc.LoadHtml(sourceString);

            if (doc.ParseErrors.Any() == false && doc.DocumentNode != null) {
                // Find all images with rel attribute
                HtmlNodeCollection imgNodes = doc.DocumentNode.SelectNodes("//img[@rel]");

                var modified = false;
                if (imgNodes != null) {
                    foreach (HtmlNode img in imgNodes) {
                        var nodeId = img.GetAttributeValue("rel", string.Empty);
                        if (int.TryParse(nodeId, NumberStyles.Integer, CultureInfo.InvariantCulture, out _)) {
                            img.Attributes.Remove("rel");
                            modified = true;
                        }
                    }
                }

                // Find all a and img tags with a data-udi attribute
                HtmlNodeCollection dataUdiNodes = doc.DocumentNode.SelectNodes("(//a|//img)[@data-udi]");
                if (dataUdiNodes != null) {
                    foreach (HtmlNode node in dataUdiNodes) {
                        node.Attributes.Remove("data-udi");
                        modified = true;
                    }
                }

                foreach (var processor in processors) {
                    if (processor.PostProcess(owner, propertyType, preview, doc)) modified = true;
                }

                if (modified) {
                    return doc.DocumentNode.OuterHtml;
                }
            }

            return sourceString;

        }

    }

}