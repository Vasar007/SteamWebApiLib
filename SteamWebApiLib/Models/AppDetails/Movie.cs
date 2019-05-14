using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Movie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("webm")]
        public IReadOnlyDictionary<string, string> Webm { get; set; } = new Dictionary<string, string>();

        [JsonProperty("highlight")]
        public bool Highlight { get; set; }


        public Movie()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }

}
