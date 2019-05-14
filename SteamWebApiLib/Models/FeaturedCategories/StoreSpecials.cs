using Newtonsoft.Json;
using SteamWebApiLib.Models.Common;

namespace SteamWebApiLib.Models.FeaturedCategories
{
    public class StoreSpecials
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("items")]
        public SteamAppInfo[] Items { get; set; }


        public StoreSpecials()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }
}
