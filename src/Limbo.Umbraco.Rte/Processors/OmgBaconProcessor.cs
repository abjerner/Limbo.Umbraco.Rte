using System;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Skybrud.Essentials.Strings.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Limbo.Umbraco.Rte.Processors {

    internal class OmgBaconProcessor : IRteHtmlProcessor {

        public string Name => "OMG!!! BACON!!!";

        public string Description => "Magically converts all text to Bacon Ipsum. Because, why not?";

        public string Icon => "icon-piggy-bank color-red";

        public bool PostProcess(IPublishedElement owner, IPublishedPropertyType propertyType, bool preview, HtmlDocument document) {

            bool modified = false;

            RecursiveFtw(document.DocumentNode, ref modified);

            return false;

        }

        /// <see>
        ///     <cref>https://github.com/abjerner/Bjerner.LoremIpsum/blob/master/src/Bjerner.LoremIpsum/Providers/BaconProvider.cs</cref>
        /// </see>
        private void RecursiveFtw(HtmlNode node, ref bool modified) {

            bool first = true;

            Random random = new();

            foreach (HtmlNode child in node.ChildNodes.Nodes().ToArray()) {

                if (child is not HtmlTextNode) {
                    RecursiveFtw(child, ref modified);
                    continue;
                }

                string value = child.InnerText;

                if (string.IsNullOrWhiteSpace(value)) continue;

                int words = value.Split(',', ' ', '.', ',', '\n', '\n', '\t').Length;

                StringBuilder sb = new();

                int i = 0;

                if (first) {
                    if (words > 1) sb.Append("Bacon");
                    if (words > 2) sb.Append(" ipsum");
                    if (words > 3) sb.Append(" dolor");
                    if (words > 4) sb.Append(" sit");
                    if (words > 5) sb.Append(" amet");
                    i = 5;
                    first = false;
                }

                for (; i < words; i++) {

                    string word = _meatyWords[random.Next(0, _meatyWords.Length)];

                    if (i == 0) {
                        sb.Append(word.FirstCharToUpper());
                    } else {
                        sb.Append(' ');
                        sb.Append(word);
                    }

                }

                child.ParentNode.ReplaceChild(HtmlNode.CreateNode(sb.ToString()), child);
                modified = true;

            }

        }

        private static readonly string[] _meatyWords = {
            "beef",
            "chicken",
            "pork",
            "bacon",
            "chuck",
            "short loin",
            "sirloin",
            "shank",
            "flank",
            "sausage",
            "pork belly",
            "shoulder",
            "cow",
            "pig",
            "ground round",
            "hamburger",
            "meatball",
            "tenderloin",
            "strip steak",
            "t-bone",
            "ribeye",
            "shankle",
            "tongue",
            "tail",
            "pork chop",
            "pastrami",
            "corned beef",
            "jerky",
            "ham",
            "fatback",
            "ham hock",
            "pancetta",
            "pork loin",
            "short ribs",
            "spare ribs",
            "beef ribs",
            "drumstick",
            "tri-tip",
            "ball tip",
            "venison",
            "turkey",
            "biltong",
            "rump",
            "jowl",
            "salami",
            "bresaola",
            "meatloaf",
            "brisket",
            "boudin",
            "andouille",
            "capicola",
            "swine",
            "kielbasa",
            "frankfurter",
            "prosciutto",
            "filet mignon",
            "leberkas",
            "turducken",
            "doner",
            "kevin",
            "landjaeger",
            "porchetta"
        };

    }

}