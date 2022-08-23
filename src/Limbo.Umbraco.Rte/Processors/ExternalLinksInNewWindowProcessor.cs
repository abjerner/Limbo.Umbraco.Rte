using HtmlAgilityPack;
using Umbraco.Cms.Core.Models.PublishedContent;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.Processors {

    public class ExternalLinksInNewWindowProcessor : IRteHtmlProcessor {

        public string Name => "External links in new window";

        public string Description => "Forces links to external sites to open in new windows if a '_target' value isn't already set.";

        public string Icon => null;

        public bool PostProcess(IPublishedElement owner, IPublishedPropertyType propertyType, bool preview, HtmlDocument document) {

            bool modified = false;

            foreach (HtmlNode a in document.DocumentNode.SelectNodes("//a")) {

                string href = a.GetAttributeValue("href", null);
                string target = a.GetAttributeValue("target", null);

                if (string.IsNullOrWhiteSpace(href)) continue;
                if (href.StartsWith("/")) continue;
                if (!string.IsNullOrWhiteSpace(target)) continue;

                a.SetAttributeValue("target", "_blank");
                modified = true;

            }

            return modified;

        }

    }

}