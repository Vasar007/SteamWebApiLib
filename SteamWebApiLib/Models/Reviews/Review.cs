using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Reviews
{
    public class Review
    {
        [JsonProperty("recommendationid")]
        public int RecommendationId { get; set; }

        [JsonProperty("author")]
        public ReviewAuthor Author { get; set; }

        [JsonProperty("language")]
        internal string LanguageInternal { get; set; }

        public Language Language => 
            Enum.TryParse<Language>(LanguageInternal, true, out var language) 
            ? language 
            : Language.Unknown;

        [JsonProperty("review")]
        public string Comment { get; set; }

        [JsonProperty("timestamp_created")]
        internal int CreatedInternal { get; set; }

        public DateTimeOffset Created => DateTimeOffset.FromUnixTimeSeconds(CreatedInternal);

        [JsonProperty("timestamp_updated")]
        internal int UpdatedInternal { get; set; }

        public DateTimeOffset Updated => DateTimeOffset.FromUnixTimeSeconds(UpdatedInternal);

        [JsonProperty("voted_up")]
        public bool VotedUp { get; set; }

        [JsonProperty("votes_up")]
        public int VotesUp { get; set; }

        [JsonProperty("votes_funny")]
        public int VotesFunny { get; set; }

        [JsonProperty("weighted_vote_score")]
        public float WeightedVoteScore { get; set; }

        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("steam_purchase")]
        public bool SteamPurchase { get; set; }

        [JsonProperty("received_for_free")]
        public bool ReceivedForFree { get; set; }

        [JsonProperty("written_during_early_access")]
        public bool WrittenDuringEarlyAccess { get; set; }


        public Review()
        {
        }
    }
}
