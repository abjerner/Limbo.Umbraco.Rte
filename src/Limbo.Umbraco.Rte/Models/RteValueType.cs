using Limbo.Umbraco.Rte.PropertyEditors;
using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Strings;

namespace Limbo.Umbraco.Rte.Models {

    /// <summary>
    /// Enum class indicating the value type of a <see cref="RtePropertyEditor"/>.
    /// </summary>
    public enum RteValueType {

        /// <summary>
        /// Indicates that the value type should be an instance of <see cref="IHtmlEncodedString"/>.
        /// </summary>
        EncodedString,

        /// <summary>
        /// Indicates that the value type should be an instance of <see cref="IHtmlContent"/>.
        /// </summary>
        HtmlContent,

        /// <summary>
        /// Indicates that the value type should be an instance of <see cref="string"/>.
        /// </summary>
        String

    }

}