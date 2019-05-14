using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Reviews
{
    public class ReviewsResponse
    {
        [JsonProperty("query_summary")]
        public ReviewQuerySummary QuerySummary { get; set; }

        [JsonProperty("reviews")]
        public IList<Review> Reviews { get; set; }


        public ReviewsResponse()
        {
        }
    }
}
