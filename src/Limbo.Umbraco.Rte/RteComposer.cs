using Limbo.Umbraco.Rte.Manifests;
using Limbo.Umbraco.Rte.Processors;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Rte {

    public class RteComposer : IComposer {

        public void Compose(IUmbracoBuilder builder) {

            builder.ManifestFilters().Append<RteManifestFilter>();

            // TODO: Should processors be registered manually as auto discovery may be expensive?
            builder.WithCollectionBuilder<RteHtmlProcessorCollectionBuilder>().Add(() => builder.TypeLoader.GetTypes<IRteHtmlProcessor>());

        }

    }

}