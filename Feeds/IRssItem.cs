using System;
using System.Collections.Generic;

namespace ProgrammingWeapons.Feeds
{
    public interface IRssItem {
        string Title { get; set; }
        string Link { get; set; }
        DateTime PubDate { get; set; }
        string Guid { get; set; }
        string Content { get; set; }
        string Description { get; set; }
        List<IRssTag> Tags { get; set; }
    }
}