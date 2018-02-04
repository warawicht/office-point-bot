using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficePointBot
{
    public class TrendingNewsInformation
    {
        public string _type { get; set; }
        public string readLink { get; set; }
        public NewsInformation[] value { get; set; }
    }
    public class NewsInformation
    {
        public string name { get; set; }
        public string url { get; set; }
        public Image image { get; set; }
        public string description { get; set; }
        public string category { get; set; }
    }
    public class Image
    {
        public Thumbnail thumbnail { get; set; }
    }
    public class Thumbnail
    {
        public string contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}