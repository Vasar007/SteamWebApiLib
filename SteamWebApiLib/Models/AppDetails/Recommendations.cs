using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Recommendations
    {
        [JsonProperty("total")]
        public long Total { get; set; }


        public Recommendations()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Total.ToString();

        #endregion
    }
}
