using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Common
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }


        public Genre()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Description;

        #endregion
    }

}
