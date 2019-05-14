using Newtonsoft.Json;

namespace SteamWebApiLib.Models.AppDetails
{

    public class Sub
    {
        [JsonProperty("packageid")]
        public int Packageid { get; set; }

        [JsonProperty("percent_savings_text")]
        public string PercentSavingsText { get; set; }

        [JsonProperty("percent_savings")]
        public int PercentSavings { get; set; }

        [JsonProperty("option_text")]
        public string OptionText { get; set; }

        [JsonProperty("option_description")]
        public string OptionDescription { get; set; }

        [JsonProperty("can_get_free_license")]
        public string CanGetFreeLicense { get; set; }

        [JsonProperty("is_free_license")]
        public bool IsFreeLicense { get; set; }

        [JsonProperty("price_in_cents_with_discount")]
        public long PriceInCentsWithDiscount { get; set; }


        public Sub()
        {
        }
    }
}
