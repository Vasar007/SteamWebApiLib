using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{

    public class PackageGroup
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("selection_text")]
        public string SelectionText { get; set; }

        [JsonProperty("save_text")]
        public string SaveText { get; set; }

        [JsonProperty("display_type")]
        public long DisplayType { get; set; }

        [JsonProperty("is_recurring_subscription")]
        public string IsRecurringSubscription { get; set; }

        [JsonProperty("subs")]
        public Sub[] Subs { get; set; }


        public PackageGroup()
        {
        }

        #region Object Overridden Methods

        public override string ToString() => Name;

        #endregion
    }
}
