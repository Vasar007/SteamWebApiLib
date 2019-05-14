using Newtonsoft.Json;

namespace SteamWebApiLib.Models.FeaturedCategories
{
    public class FeaturedCategories
    {
        [JsonProperty("specials")]
        public StoreSpecials Specials { get; set; }

        [JsonProperty("coming_soon")]
        public FeaturedCategory ComingSoon { get; set; }

        [JsonProperty("top_sellers")]
        public FeaturedCategory TopSellers { get; set; }

        [JsonProperty("new_releases")]
        public FeaturedCategory NewReleases { get; set; }

        [JsonProperty("genres")]
        public FeaturedCategoryShort Genres { get; set; }

        [JsonProperty("trailerslideshow")]
        public FeaturedCategoryShort TrailerSlideshow { get; set; }


        public FeaturedCategories()
        {
        }
    }
}
