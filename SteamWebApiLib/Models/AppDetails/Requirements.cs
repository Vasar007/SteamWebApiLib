using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Requirements
    {
        [JsonProperty("minimum", NullValueHandling = NullValueHandling.Ignore)]
        public string Minimum { get; set; }

        [JsonProperty("recommended", NullValueHandling = NullValueHandling.Ignore)]
        public string Recommended { get; set; }


        public Requirements()
        {
        }
    }
}
