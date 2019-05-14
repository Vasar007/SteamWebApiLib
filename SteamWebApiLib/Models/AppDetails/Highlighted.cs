using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Highlighted
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        public Highlighted()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }
}
