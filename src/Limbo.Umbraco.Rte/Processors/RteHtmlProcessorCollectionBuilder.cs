using Umbraco.Cms.Core.Composing;

namespace Limbo.Umbraco.Rte.Processors {

    internal sealed class RteHtmlProcessorCollectionBuilder : LazyCollectionBuilderBase<RteHtmlProcessorCollectionBuilder, RteHtmlProcessorCollection, IRteHtmlProcessor> {

        protected override RteHtmlProcessorCollectionBuilder This => this;

    }

}