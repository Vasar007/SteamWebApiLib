using Newtonsoft.Json;

namespace SteamWebApiLib.Models.Common
{
    public class Platforms
    {
        [JsonProperty("windows")]
        public bool Windows { get; set; }

        [JsonProperty("mac")]
        public bool Mac { get; set; }

        [JsonProperty("linux")]
        public bool Linux { get; set; }

        
        public Platforms()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => string.Join(
            ",", Windows ? "Windows" : null, Linux ? "Linux" : null, Mac ? "Mac" : null
        );

        #endregion
    }

}
