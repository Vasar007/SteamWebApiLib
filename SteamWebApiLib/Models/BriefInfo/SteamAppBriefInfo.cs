using Newtonsoft.Json;

namespace SteamWebApiLib.Models.BriefInfo
{
    public class SteamAppBriefInfo
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        public SteamAppBriefInfo()
        {
        }
    }
}
