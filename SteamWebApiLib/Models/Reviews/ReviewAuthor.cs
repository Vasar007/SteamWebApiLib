using System;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Reviews
{
    public class ReviewAuthor
    {
        [JsonProperty("steamid")]
        public ulong SteamId { get; set; }

        [JsonProperty("num_games_owned")]
        public int NumGamesOwned { get; set; }

        [JsonProperty("num_reviews")]
        public int NumReviews { get; set; }

        [JsonProperty("playtime_forever")]
        internal int PlaytimeForeverInternal { get; set; }

        public TimeSpan PlayTimeForever => TimeSpan.FromMinutes(PlaytimeForeverInternal);

        [JsonProperty("playtime_last_two_weeks")]
        internal int PlayTimeLastTwoWeeksInternal { get; set; }

        public TimeSpan PlayTimeLastTwoWeeks => TimeSpan.FromMinutes(PlayTimeLastTwoWeeksInternal);

        [JsonProperty("last_played")]
        internal int LastPlayedInternal { get; set; }

        public DateTimeOffset LastPlayed => DateTimeOffset.FromUnixTimeSeconds(LastPlayedInternal);


        public ReviewAuthor()
        {
        }
    }
}
