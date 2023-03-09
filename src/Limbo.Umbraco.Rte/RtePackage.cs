﻿using System;
using System.Diagnostics;
using Umbraco.Cms.Core.Semver;

namespace Limbo.Umbraco.Rte {

    /// <summary>
    /// Static class with various information and constants about the package.
    /// </summary>
    internal class RtePackage {

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        public const string Alias = "Limbo.Umbraco.Rte";

        /// <summary>
        /// Gets the friendly name of the package.
        /// </summary>
        public const string Name = "Limbo RTE";

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public static readonly Version Version = typeof(RtePackage).Assembly.GetName().Version!;

        /// <summary>
        /// Gets the informational version of the package.
        /// </summary>
        public static readonly string InformationalVersion = FileVersionInfo.GetVersionInfo(typeof(RtePackage).Assembly.Location).ProductVersion!;

        /// <summary>
        /// Gets the semantic version of the package.
        /// </summary>
        public static readonly SemVersion SemVersion = InformationalVersion;

        /// <summary>
        /// Gets the URL of the GitHub repository for this package.
        /// </summary>
        public const string GitHubUrl = "https://github.com/abjerner/Limbo.Umbraco.Rte";

        /// <summary>
        /// Gets the URL of the issue tracker for this package.
        /// </summary>
        public const string IssuesUrl = "https://github.com/abjerner/Limbo.Umbraco.Rte/issues";

        /// <summary>
        /// Gets the website URL of the package.
        /// </summary>
        public const string WebsiteUrl = "https://packages.limbo.works/limbo.umbraco.rte/v2/";

        /// <summary>
        /// Gets the URL of the documentation for this package.
        /// </summary>
        public const string DocumentationUrl = "https://packages.limbo.works/limbo.umbraco.rte/v2/docs/";

    }

}