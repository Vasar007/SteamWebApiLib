using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamWebApiLib.Models.AppDetails;
using SteamWebApiLib.Models.Common;

namespace SteamWebApiLib.Models.PackageDetails
{
    public  class PackageInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public int SteamPackageId { get; set; }

        [JsonProperty("purchase_text")]
        public string PurchaseText { get; set; }

        [JsonProperty("page_image")]
        public string PageImage { get; set; }

        [JsonProperty("small_logo")]
        public string SmallLogo { get; set; }

        [JsonProperty("apps")]
        public PackageApp[] Apps { get; set; }

        [JsonProperty("price")]
        public PriceOverview Price { get; set; }

        [JsonProperty("platforms")]
        public Platforms Platforms { get; set; }

        [JsonProperty("controller")]
        public FullGamepadSupport Controller { get; set; }

        [JsonProperty("release_date")]
        public ReleaseDate ReleaseDate { get; set; }


        public PackageInfo()
        {
        }

        public static SteamApp FromJson(string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };

            return JsonConvert.DeserializeObject<SteamApp>(json, serializerSettings);
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }
}
