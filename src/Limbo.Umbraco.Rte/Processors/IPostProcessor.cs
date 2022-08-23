using HtmlAgilityPack;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Rte.Processors {

    /// <summary>
    /// Interface describing a RTE HTML processor.
    /// </summary>
    public interface IRteHtmlProcessor {

        /// <summary>
        /// Gets the name of the processor.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the processor.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the icon of the processor, if any.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Processes the <paramref name="rawHtml"/>.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyType"></param>
        /// <param name="preview">Whether preview mode is enabled.</param>
        /// <param name="rawHtml">The raw HTML to be processed.</param>
        /// <returns>The processed HTML string.</returns>
        public string PreProcess(IPublishedElement owner, IPublishedPropertyType propertyType, bool preview, string rawHtml) {
            return rawHtml;
        }

        /// <summary>
        /// Processes the specified HTML <paramref name="document"/>.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyType"></param>
        /// <param name="preview">Whether preview mode is enabled.</param>
        /// <param name="document">The HTML document to process.</param>
        /// <returns>A boolean value indicating whether <paramref name="document"/> was modified.</returns>
        public bool PostProcess(IPublishedElement owner, IPublishedPropertyType propertyType, bool preview, HtmlDocument document) {
            return false;
        }

    }

}