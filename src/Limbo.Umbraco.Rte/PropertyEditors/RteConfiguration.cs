using Newtonsoft.Json.Linq;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    public class RteConfiguration : RichTextConfiguration {

        internal const string ProccesorsView = $"/App_Plugins/{RtePackage.Alias}/Views/Processors.html";

        [ConfigurationField("processors", "Processors", ProccesorsView)]
        public JToken? Processors { get; set; }

    }

}