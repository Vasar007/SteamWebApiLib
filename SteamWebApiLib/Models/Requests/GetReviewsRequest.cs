using SteamWebApiLib.Models.Reviews;

namespace SteamWebApiLib.Models.Requests
{
    public class GetReviewsRequest
    {
        public ReviewFilter Filter { get; set; }

        public string Language { get; set; } = "all";

        public int? DayRange { get; set; }

        public int? StartOffset { get; set; }

        public ReviewType ReviewType { get; set; }

        public ReviewPurchaseType PurchaseType { get; set; }

        public int AppId { get; set; }


        public GetReviewsRequest(int appId)
        {
            AppId = appId;
        }
    }
}
