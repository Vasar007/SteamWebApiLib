using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.PackageDetails
{
    public partial class PackageApp : IEquatable<PackageApp>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        public PackageApp()
        {
        }

        #region IEquatable<PackageApp> Implementation

        public bool Equals(PackageApp other)
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

            if (!(obj is PackageApp personObj)) return false;

            return Equals(personObj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;

        #endregion
    }
}
