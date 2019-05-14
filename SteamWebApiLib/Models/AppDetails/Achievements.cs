using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{
    public class Achievements
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("highlighted")]
        public Highlighted[] Highlighted { get; set; }


        public Achievements()
        {
        }
    }
}
