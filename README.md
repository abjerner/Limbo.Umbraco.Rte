# Limbo RTE

**Limbo.Umbraco.Rte** is small package that brings extended functionality to Umbraco's build-in rich text editor (TinyMCE). The package does so by adding a new **Limbo RTE** property editor, which enables developers to select a number of *processors*, that may modify the HTML - eg. to ensure that external links always open in a new window/tab.




<br /><br />

## Installation

The Umbraco 10 version of this package is only available via [NuGet](https://www.nuget.org/packages/Limbo.Umbraco.Rte/2.0.0-alpha001). To install the package, you can use either .NET CLI:

```
dotnet add package Limbo.Umbraco.Rte --version 2.0.0-alpha001
```

or the older NuGet Package Manager:

```
Install-Package Limbo.Umbraco.Rte -Version 2.0.0-alpha001
```

**Umbraco 9**  
For the Umbraco 9 version of this package, see the [**v1/main**](https://github.com/abjerner/Limbo.Umbraco.Rte/tree/v1/main) branch instead.




<br /><br />

## Screenshots

The new data type introduces a *Processors* option where you can add the desired processors - and in the order that you'd need them:

![image](https://user-images.githubusercontent.com/3634580/185799113-753cbcf0-a18f-434a-81a9-3b612606f122.png)




<br /><br />

## Documentation

The package revolves around the `IRteHtmlProcessor` interface. Classes implementing this interface will be selectable from the **Limbo RTE** data type.

While the package is still a bit experimental, it currently contains to default processors:

- **OmgBaconProcessor**  
Mostly for fun and for illustrative purposes, this will replace text with bacon ipsum auto-generated texts instead. You probably shouldn't use this in your live envrionments :wink:

- **ExternalLinksInNewWindowProcessor**
This processor will use HtmlAgilityPack for scanning the RTE value's DOM tree for any `<a/>`, and then automatically add `target="_blank"` to external links that don't already have a `target` value.

Umbraco's default RTE already does some parsing under the hood (eg. to update internal links and images). This is partially done on the raw HTML string, but then also using HtmlAgilityPack for traversing through the RTE value in a DOM tree.

Based on this, the `IRteHtmlProcessor` interface describes two different methods. The first one being the `PreProcess` method, which lets you modify the raw HTML string. The `PostProcess` method, on the other hand, lets you modify the DOM tree.

An example using the `PostProcess` method is the build-in `ExternalLinksInNewWindowProcessor` class:

```csharp
using HtmlAgilityPack;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Rte.Processors {

    public class ExternalLinksInNewWindowProcessor : IRteHtmlProcessor {

        public string Name => "External links in new window";

        public string Description => "Forces links to external sites to open in new windows if a '_target' value isn't already set.";

        public string? Icon => null;

        public bool PostProcess(IPublishedElement owner, IPublishedPropertyType propertyType, bool preview, HtmlDocument document) {

            bool modified = false;

            foreach (HtmlNode a in document.DocumentNode.SelectNodes("//a")) {

                string? href = a.GetAttributeValue("href", null);
                string? target = a.GetAttributeValue("target", null);

                if (string.IsNullOrWhiteSpace(href)) continue;
                if (href.StartsWith("/")) continue;
                if (!string.IsNullOrWhiteSpace(target)) continue;

                a.SetAttributeValue("target", "_blank");
                modified = true;

            }

            return modified;

        }

    }

}
```
