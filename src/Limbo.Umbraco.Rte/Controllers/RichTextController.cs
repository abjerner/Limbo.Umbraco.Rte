using System.Linq;
using Limbo.Umbraco.Rte.Processors;
using Newtonsoft.Json.Linq;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.Controllers {

    [PluginController("Limbo")]
    public class RichTextController : UmbracoAuthorizedApiController {

        private readonly RteHtmlProcessorCollection _processors;

        public RichTextController(RteHtmlProcessorCollection processors) {
            _processors = processors;
        }

        public object GetProcessors() {
            return _processors.Select(x => new JObject {
                { "type", x.GetType().AssemblyQualifiedName },
                { "name", x.Name },
                { "icon", x.Icon },
                { "description", x.Description }
            });

        }

    }

}