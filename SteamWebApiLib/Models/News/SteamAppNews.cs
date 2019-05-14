using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.News
{
    public class SteamAppNews
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("newsitems")]
        public NewsItem[] NewsItems { get; set; }


        public SteamAppNews()
        {
        }
    }
}
