using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Common
{
    public class SteamAppInfo : IEquatable<SteamAppInfo>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discounted")]
        public bool Discounted { get; set; }

        [JsonProperty("discount_percent")]
        public int DiscountPercent { get; set; }

        [JsonProperty("original_price")]
        [JsonConverter(typeof(SteamPriceStringConverter))]
        public double? OriginalPrice { get; set; }

        [JsonProperty("final_price")]
        [JsonConverter(typeof(SteamPriceStringConverter))]
        public double? FinalPrice { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("large_capsule_image")]
        public string LargeCapsuleImage { get; set; }

        [JsonProperty("small_capsule_image")]
        public string SmallCapsuleImage { get; set; }

        [JsonProperty("windows_available")]
        public bool WindowsAvailable { get; set; }

        [JsonProperty("mac_available")]
        public bool MacAvailable { get; set; }

        [JsonProperty("linux_available")]
        public bool LinuxAvailable { get; set; }

        [JsonProperty("streamingvideo_available")]
        public bool StreamingvideoAvailable { get; set; }

        [JsonProperty("header_image")]
        public string HeaderImage { get; set; }

        [JsonProperty("discount_expiration", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(EpochToDateTimeConverter))]
        public DateTime? DiscountExpiration { get; set; }

        [JsonProperty("controller_support", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ControllerSupportConverter))]
        public ControllerSupport? ControllerSupport { get; set; }

        [JsonProperty("headline", NullValueHandling = NullValueHandling.Ignore)]
        public string Headline { get; set; }

        [JsonProperty("purchase_package", NullValueHandling = NullValueHandling.Ignore)]
        public string PurchasePackage { get; set; }


        public SteamAppInfo()
        {
        }

        #region IEquatable<SteamAppInfo> Implementation

        public bool Equals(SteamAppInfo other)
        {
            if (other is null) return false;

            if (Id == other.Id && Type == other.Type) return true;
        
            return false;
        }

        #endregion

        #region Object Overridden Methods

        public override bool Equals(Object obj)
        {
            if (obj is null) return false;

            if (!(obj is SteamAppInfo personObj)) return false;
            
            return Equals(personObj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;

        #endregion
    }
}
