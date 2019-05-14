using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamWebApiLib.Models.Common;

namespace SteamWebApiLib.Models.Featured
{
    public class FeaturedApps
    {
        [JsonProperty("large_capsules")]
        public SteamAppInfo[] LargeCapsules { get; set; }

        [JsonProperty("featured_win")]
        public SteamAppInfo[] FeaturedWin { get; set; }

        [JsonProperty("featured_mac")]
        public SteamAppInfo[] FeaturedMac { get; set; }

        [JsonProperty("featured_linux")]
        public SteamAppInfo[] FeaturedLinux { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }


        public FeaturedApps()
        {
        }

        public static FeaturedApps FromJson(string json)
        {

            var serializerSettings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new ControllerSupportConverter(),
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };

            return JsonConvert.DeserializeObject<FeaturedApps>(json, serializerSettings);
        }
    }
}
