using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Media;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Infrastructure.Templates;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo RTE", EditorView, ValueType = ValueTypes.Text, Group = "Limbo", Icon = EditorIcon)]
    public class RtePropertyEditor : RichTextPropertyEditor {

        internal const string EditorAlias = "Limbo.Umbraco.Rte";

        internal const string EditorName = "Limbo RTE";

        internal const string EditorIcon = "icon-browser-window color-limbo";

        internal const string EditorView = "rte";

        private readonly IIOHelper _ioHelper;

        public RtePropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IBackOfficeSecurityAccessor backOfficeSecurityAccessor, HtmlImageSourceParser imageSourceParser, HtmlLocalLinkParser localLinkParser, RichTextEditorPastedImages pastedImages, IIOHelper ioHelper, IImageUrlGenerator imageUrlGenerator, IHtmlMacroParameterParser macroParameterParser) : base(dataValueEditorFactory, backOfficeSecurityAccessor, imageSourceParser, localLinkParser, pastedImages, ioHelper, imageUrlGenerator, macroParameterParser) {
            _ioHelper = ioHelper;
        }


        protected override IConfigurationEditor CreateConfigurationEditor() =>
            new RteConfigurationEditor(_ioHelper);

    }

}