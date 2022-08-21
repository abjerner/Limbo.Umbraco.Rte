using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Limbo.Umbraco.Rte.Manifests {

    /// <inheritdoc />
    public class RteManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                PackageName = RtePackage.Alias.Replace(".", "-").ToLowerInvariant(),
                BundleOptions = BundleOptions.Independent,
                Scripts = new[] {
                    $"/App_Plugins/{RtePackage.Alias}/Scripts/Controllers/Processors.js"
                },
                //Stylesheets = new[] {
                //    $"/App_Plugins/{RtePackage.Alias}/Styles/Default.css"
                //}
            });
        }

    }

}