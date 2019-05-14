using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{

    public class Screenshot
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("path_thumbnail")]
        public string PathThumbnail { get; set; }

        [JsonProperty("path_full")]
        public string PathFull { get; set; }


        public Screenshot()
        {
        }
    }
}
