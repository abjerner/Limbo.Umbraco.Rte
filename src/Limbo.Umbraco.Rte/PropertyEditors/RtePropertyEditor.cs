﻿using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Media;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Infrastructure.Templates;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo RTE", EditorView, ValueType = ValueTypes.Text, Group = "Limbo", Icon = EditorIcon)]
    public class RtePropertyEditor : RichTextPropertyEditor {

        public const string EditorAlias = "Limbo.Umbraco.Rte";

        public const string EditorName = "Limbo RTE";

        public const string EditorIcon = "icon-browser-window color-limbo";

        public const string EditorView = "rte";

        private readonly IIOHelper _ioHelper;
        private readonly IEditorConfigurationParser _editorConfigurationParser;

        public RtePropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IBackOfficeSecurityAccessor backOfficeSecurityAccessor, HtmlImageSourceParser imageSourceParser, HtmlLocalLinkParser localLinkParser, RichTextEditorPastedImages pastedImages, IIOHelper ioHelper, IImageUrlGenerator imageUrlGenerator, IHtmlMacroParameterParser macroParameterParser, IEditorConfigurationParser editorConfigurationParser) : base(dataValueEditorFactory, backOfficeSecurityAccessor, imageSourceParser, localLinkParser, pastedImages, ioHelper, imageUrlGenerator, macroParameterParser, editorConfigurationParser) {
            _ioHelper = ioHelper;
            _editorConfigurationParser = editorConfigurationParser;
        }


        protected override IConfigurationEditor CreateConfigurationEditor() =>
            new RteConfigurationEditor(_ioHelper, _editorConfigurationParser);

    }

}