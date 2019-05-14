using Newtonsoft.Json;
using SteamWebApiLib.Models.Common;
using System;

namespace SteamWebApiLib.Models.FeaturedCategories
{
    public class FeaturedCategory : IEquatable<FeaturedCategory>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("items")]
        public SteamAppInfo[] Items { get; set; }


        public FeaturedCategory()
        {
        }

        #region IEquatable<FeaturedCategory> Implementation

        public bool Equals(FeaturedCategory other)
        {
            if (other is null) return false;

            if (Id == other.Id) return true;

            return false;
        }

        #endregion

        #region Object Overridden Methods

        public override bool Equals(Object obj)
        {
            if (obj is null) return false;

            if (!(obj is FeaturedCategory personObj)) return false;

            return Equals(personObj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;

        #endregion
    }
}
