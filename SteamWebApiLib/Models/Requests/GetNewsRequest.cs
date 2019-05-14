using System;

namespace SteamWebApiLib.Models.Requests
{
    public class GetNewsRequest
    {
        public int AppId { get; set; }

        public int? MaxLength { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public int? Count { get; set; }


        public GetNewsRequest(int appId)
        {
            AppId = appId;
        }
    }
}
