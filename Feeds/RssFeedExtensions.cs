using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace ProgrammingWeapons.Feeds
{
    public static class RssFeedExtensions
    {
        public static XDocument ToXDocument(this IRssFeed feed) {
            Contract.Requires<ArgumentNullException>(feed != null);
            //Method stolen from https://github.com/abjerner/Skybrud.Umbraco.RssUtils

            var xChannel = new XElement(
                "channel",
                new XElement("title", feed.Title ?? ""),
                new XElement("link", feed.Link ?? ""),
                new XElement("pubDate", feed.PubDate.ToUniversalTime().ToString("r")),
                from item in feed.Items orderby item.PubDate descending select item.ToXElement()
                );

            if (!String.IsNullOrWhiteSpace(feed.Generator)) xChannel.Add(new XElement("generator", feed.Generator));
            if (!String.IsNullOrWhiteSpace(feed.Description)) xChannel.Add(new XElement("description", feed.Description));
            if (!String.IsNullOrWhiteSpace(feed.Language)) xChannel.Add(new XElement("language", feed.Language));

            return new XDocument(
                new XDeclaration("1.0", "UTF-8", "true"),
                new XElement(
                    "rss",
                    new XAttribute(XNamespace.Xmlns + "content", "http://purl.org/rss/1.0/content"),
                    xChannel
                    )
                );
        }


        public static void Set(this IRssFeed feed, [CanBeNull] XElement element) {
            Contract.Requires<ArgumentNullException>(feed != null);

            if (element == null)
                return;

            var value = element.Value;
            var name = element.Name.LocalName.ToLower();

            switch (name) {
                case "title":
                    feed.Title = value;
                    break;

                case "link":
                    feed.Link = value;
                    break;

                case "pubdate":
                    feed.PubDate = DateTime.Parse(value);
                    break;

                case "description":
                    feed.Description = value;
                    break;

                case "language":
                    feed.Language = value;
                    break;

                case "generator":
                    feed.Generator = value;
                    break;
            }
        }


        public static string[] Descendals = {
            "title", "link", "pubDate", "description", "language", "generator"
        };

        public static void Load(this IRssFeed feed, Stream stream, Action callback = null) {
            feed.Rss = XElement.Load(stream);
            feed.Load(callback);
        }


        public static void Load(this IRssFeed feed, Action callback = null) {
            var channel = feed.Rss.Descendants("channel").SingleOrDefault();
            if (channel == null) {
                //todo: Send some error
                return;
            }

            foreach (var descendant in Descendals) {
                feed.Set(channel.Element(descendant));
            }

            feed.Items.Clear();
            foreach (var rssItem in feed.Rss.Descendants("item"))
            {
                var item = feed.AddItem();
                item.Load(rssItem);
            }

            if (callback != null)
                callback();
        }
    }
}
