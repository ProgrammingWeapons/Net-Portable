using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace ProgrammingWeapons.Feeds
{
    public interface IRssFeed : INotifyPropertyChanged {
        string Title { get; set; }
        string Url { get; set; }
        string Link { get; set; }
        string Generator { get; set; }
        string Description { get; set; }
        string Language { get; set; }
        DateTime PubDate { get; set; }

        IList<IRssItem> Items { get; }

        XElement Rss { get; set; }

        IRssItem AddItem();
    }

}