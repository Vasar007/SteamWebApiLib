using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Common
{
    public class PriceOverview : IEquatable<PriceOverview>
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("initial")]
        [JsonConverter(typeof(SteamPriceStringConverter))]
        public double Initial { get; set; }

        [JsonProperty("final")]
        [JsonConverter(typeof(SteamPriceStringConverter))]
        public double Final { get; set; }

        [JsonProperty("discount_percent")]
        public int DiscountPercent { get; set; }

        [JsonProperty("individual", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SteamPriceStringConverter))]
        public double Individual { get; set; }


        public PriceOverview()
        {
        }

        #region IEquatable<PriceOverview> Implementation

        public bool Equals(PriceOverview other)
        {
            if (other is null) return false;

            if (Final == other.Final && Currency == other.Currency && Initial == other.Initial)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Object Overridden Methods

        public override bool Equals(Object obj)
        {
            if (obj is null) return false;

            if (!(obj is PriceOverview personObj)) return false;

            return Equals(personObj);
        }

        public override int GetHashCode() =>
            Final.GetHashCode() ^ Initial.GetHashCode() ^ Currency.GetHashCode();

        public override string ToString() => Final.ToString() + " " + Currency;

        #endregion
    }
}
