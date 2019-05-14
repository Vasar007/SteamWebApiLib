using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.News
{
    public class NewsItem
    {
        [JsonProperty("gid")]
        public long Gid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("is_external_url")]
        public bool IsExternalUrl { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("contents")]
        public string Contents { get; set; }

        [JsonProperty("feedlabel")]
        public string FeedLabel { get; set; }

        [JsonProperty("date")]
        internal long DateInternal { get; set; }

        public DateTimeOffset Date => DateTimeOffset.FromUnixTimeSeconds(DateInternal);

        [JsonProperty("feedname")]
        public string FeedName { get; set; }

        [JsonProperty("feed_type")]
        public int FeedType { get; set; }

        [JsonProperty("appid")]
        public int AppId { get; set; }


        public NewsItem()
        {
        }
    }
}
