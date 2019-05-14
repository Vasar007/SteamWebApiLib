using Newtonsoft.Json;

namespace SteamWebApiLib.Models.PackageDetails
{
    public class FullGamepadSupport
    {
        [JsonProperty("full_gamepad")]
        public bool FullGamepad { get; set; }


        public FullGamepadSupport()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => FullGamepad.ToString();

        #endregion
    }
}
