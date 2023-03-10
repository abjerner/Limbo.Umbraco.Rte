using Newtonsoft.Json.Linq;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    public class RteConfiguration : RichTextConfiguration {

        internal const string ProccesorsView = $"/App_Plugins/{RtePackage.Alias}/Views/Processors.html";

        [ConfigurationField("processors", "Processors", ProccesorsView)]
        public JToken? Processors { get; set; }

        /// <summary>
        /// Gets or sets whether the property value type should be a nullable type.
        /// </summary>
        [ConfigurationField("nullable", "Nullable", "boolean", Description = "Specify whether the property value type should be a nullable type.")]
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets .NET value type returned by properties using this data type.
        /// </summary>
        [ConfigurationField("valueType", "Value type", "/App_Plugins/Limbo.Umbraco.Rte/Views/ValueType.html", Description = "Select the .NET value type returned by properties using this data type.")]
        public string? ValueType { get; set; }

    }

}