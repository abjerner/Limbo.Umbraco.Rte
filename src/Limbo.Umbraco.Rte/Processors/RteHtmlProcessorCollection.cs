using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Limbo.Umbraco.Rte.Processors {

    /// <summary>
    /// Class representing a collection of <see cref="IRteHtmlProcessor"/> instances.
    /// </summary>
    public class RteHtmlProcessorCollection : BuilderCollectionBase<IRteHtmlProcessor> {

        private readonly Dictionary<string, IRteHtmlProcessor> _lookup;

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="items"/>.
        /// </summary>
        /// <param name="items">A callback function used for getting the initial items of the collection.</param>
        public RteHtmlProcessorCollection(Func<IEnumerable<IRteHtmlProcessor>> items) : base(items) {

            _lookup = new Dictionary<string, IRteHtmlProcessor>(StringComparer.OrdinalIgnoreCase);

            foreach (IRteHtmlProcessor item in this) {
                string? typeName = item.GetType().AssemblyQualifiedName;
                if (typeName != null && _lookup.ContainsKey(typeName) == false) {
                    _lookup.Add(typeName, item);
                }
            }

        }

        /// <summary>
        /// Attempts to get the processor of the specified type <typeparamref name="TProcessor"/>.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="result">When this method returns, holds the <typeparamref name="TProcessor"/> instance if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
        public bool TryGet<TProcessor>(out TProcessor? result) where TProcessor : IRteHtmlProcessor {
            if (_lookup.TryGetValue(typeof(TProcessor).AssemblyQualifiedName!, out IRteHtmlProcessor? importer)) {
                result = (TProcessor) importer;
                return true;
            }
            result = default;
            return false;
        }

        /// <summary>
        /// Attempts to get the processor with the specified <paramref name="typeName"/>.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="result">When this method returns, holds the <see cref="IRteHtmlProcessor"/> instance if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
        public bool TryGet(string typeName, out IRteHtmlProcessor? result) {
            return _lookup.TryGetValue(typeName, out result);
        }

    }

}