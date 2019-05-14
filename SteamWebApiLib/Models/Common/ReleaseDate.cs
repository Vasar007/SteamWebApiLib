using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Common
{
    public class ReleaseDate
    {
        [JsonProperty("coming_soon")]
        public bool ComingSoon { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }


        public ReleaseDate()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => string.IsNullOrWhiteSpace(Date) ? "Coming Soon" : Date;

        #endregion
    }
}
