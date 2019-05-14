using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }


        public Category()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Description;

        #endregion
    }
}
