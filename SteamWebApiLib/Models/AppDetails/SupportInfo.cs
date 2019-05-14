using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class SupportInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }


        public SupportInfo()
        {
        }
    }
}
