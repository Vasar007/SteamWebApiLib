using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebApiLib.Models.BriefInfo
{
    public class SteamAppBriefInfoList
    {
        [JsonProperty("apps")]
        public SteamAppBriefInfo[] Apps { get; set; }


        public SteamAppBriefInfoList()
        {
        }
    }
}
