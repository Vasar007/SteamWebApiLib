namespace SteamWebApiLib
{
    /// <summary>
    /// Contains additional data for client.
    /// </summary>
    public sealed class SteamApiConfig
    {
        /// <summary>
        /// The Steam API URL.
        /// </summary>
        public string SteamPoweredBaseUrl { get; set; } = "https://api.steampowered.com";

        /// <summary>
        /// The Steam Storefront API URL.
        /// </summary>
        public string SteamStoreBaseUrl { get; set; } = "http://store.steampowered.com";

        /// <summary>
        /// The API key for the supplied user.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The number of times requests will be retried.
        /// </summary>
        public int RetryAttempts { get; set; } = 2;

        /// <summary>
        /// The number of retries will be an exponent of this number.
        /// </summary>
        public int RetryBackoffExponent { get; set; } = 2;


        /// <summary>
        /// Initilaizes config by default values.
        /// </summary>
        public SteamApiConfig()
        {
        }
    }
}
