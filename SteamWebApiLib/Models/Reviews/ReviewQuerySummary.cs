using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Reviews
{
    public class ReviewQuerySummary
    {
        [JsonProperty("num_reviews")]
        public int NumReviews { get; set; }

        [JsonProperty("review_score")]
        public int ReviewScore { get; set; }

        [JsonProperty("review_score_desc")]
        public string ReviewScoreDescription { get; set; }

        [JsonProperty("total_positive")]
        public int TotalPositive { get; set; }

        [JsonProperty("total_negative")]
        public int TotalNegative { get; set; }

        [JsonProperty("total_reviews")]
        public int TotalReviews { get; set; }


        public ReviewQuerySummary()
        {
        }
    }
}
