using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Limbo.Umbraco.Rte.Manifests {

    /// <inheritdoc />
    public class RteManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                AllowPackageTelemetry = true,
                PackageName = RtePackage.Name,
                Version = RtePackage.InformationalVersion,
                BundleOptions = BundleOptions.Independent,
                Scripts = new[] {
                    $"/App_Plugins/{RtePackage.Alias}/Scripts/Controllers/Processors.js"
                }
            });
        }

    }

}