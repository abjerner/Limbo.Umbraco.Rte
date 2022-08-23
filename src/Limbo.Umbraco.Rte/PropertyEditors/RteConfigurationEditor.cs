using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte.PropertyEditors {

    public class RteConfigurationEditor : ConfigurationEditor<RteConfiguration> {

        public RteConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) { }

    }

}