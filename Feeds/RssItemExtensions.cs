using System;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace ProgrammingWeapons.Feeds
{
    public static class RssItemExtensions
    {
        public static XElement ToXElement(this IRssItem item) {
            Contract.Requires<ArgumentNullException>(item != null);
            // Method stolen from https://github.com/abjerner/Skybrud.Umbraco.RssUtils

            // Generate the XML node with mandatory attributes
            var xItem = new XElement(
                "item",
                new XElement("title", item.Title ?? ""),
                new XElement("link", item.Link ?? ""),
                new XElement("pubDate", item.PubDate.ToUniversalTime().ToString("r")),
                new XElement("guid", item.Guid ?? "")
            );

            XNamespace xContent = "http://purl.org/rss/1.0/content";

            // Add optinal attributes
            if (!String.IsNullOrWhiteSpace(item.Content)) xItem.Add(new XElement(xContent + "encoded", new XCData(item.Content)));

            foreach (var rssTag in item.Tags) {
                xItem.Add(new XElement(rssTag.Title, rssTag.Content));
            }

            return xItem;
        }


        public static void Set(this IRssItem item, XElement element) {
            Contract.Requires<ArgumentNullException>( item != null );

            if (element == null)
                return;

            var value = element.Value;
            var name = element.Name.LocalName.ToLower();

            switch (name)
            {
                case "title":
                    item.Title = value;
                    break;

                case "link":
                    item.Link = value;
                    break;

                case "pubdate":
                    item.PubDate = DateTime.Parse(value);
                    break;

                case "description":
                    item.Description = value;
                    break;

                case "guid":
                    item.Guid = value;
                    break;
            }
        }


        public static string[] Descendals = {
            "title", "link", "pubDate", "description", "guid"
        };
        public static void Load(this IRssItem item, XElement doc) {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(doc != null);

            foreach (var descendal in Descendals) {
                item.Set(doc.Element(descendal));
            }
        }

    }
}
