using Newtonsoft.Json;

namespace SteamWebApiLib.Models.FeaturedCategories
{
    public class FeaturedCategoryShort
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        public FeaturedCategoryShort()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }
}
