using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using SteamWebApiLib.Models.Requests;
using SteamWebApiLib.Models.AppDetails;
using SteamWebApiLib.Models.Featured;
using SteamWebApiLib.Models.FeaturedCategories;
using SteamWebApiLib.Models.PackageDetails;
using SteamWebApiLib.Models.BriefInfo;
using SteamWebApiLib.Models.News;
using SteamWebApiLib.Models.Reviews;

namespace SteamWebApiLib
{
    /// <summary>
    /// Client which can make a requests to the Steam API. Client uses Retry Policy and will try to 
    /// get response several times (you can specify attempts number in config). If no response was 
    /// received or an error code was received, an exception will be thrown.
    /// </summary>
    public sealed class SteamApiClient : IDisposable
    {
        /// <summary>
        /// An HTTP client to use if pooling connections.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Contains additional data which client uses in requests.
        /// </summary>
        private readonly SteamApiConfig _config;

        /// <summary>
        /// Flag to detect redundant dispose calls.
        /// </summary>
        private bool _disposedValue;


        /// <summary>
        /// Initilaizes client to interact with Steam API.
        /// </summary>
        /// <param name="config">Client configuration.</param>
        public SteamApiClient(SteamApiConfig config = null)
        {
            _config = config ?? new SteamApiConfig();
            _httpClient = new HttpClient();
        }


        /// <summary>
        /// Retrieves all available applications in Steam Store (including DLL, VR apps and e.t.c.).
        /// </summary>  
        /// <param name="request">A set of parameters for request.</param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>List of app identifiers and its names.</returns>
        public async Task<SteamAppBriefInfoList> GetAppListAsync(CancellationToken token = default)
        {
            using (var response = await GetRetryPolicy().ExecuteAsync(
                       () => _httpClient.GetAsync(_config.SteamPoweredBaseUrl +
                           "/ISteamApps/GetAppList/v0002/", token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it.
                var jsonData = JToken.Parse(result).First.First;

                return jsonData.ToObject<SteamAppBriefInfoList>();
            }
        }

        /// <summary>
        /// Retrieves reviews data for the specified application.
        /// </summary>  
        /// <param name="request">A set of parameters for request.</param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Reviews data for specified steam application.</returns>
        public async Task<ReviewsResponse> GetReviewsAsync(GetReviewsRequest request,
            CancellationToken token = default)
        {
            // https://partner.steamgames.com/doc/store/getreviews
            string queryParameters = $"{request.AppId}?json=1" +
                $"&filter={request.Filter.ToString().ToLower()}" +
                $"&language={request.Language}" +
                $"&review_type={request.ReviewType.ToString().ToLower()}" +
                $"&purchase_type={request.PurchaseType.ToString().ToLower()}" +
                (request.DayRange.HasValue
                    ? $"&day_range={request.DayRange.Value}"
                    : string.Empty) +
                (request.StartOffset.HasValue
                    ? $"&start_offset={request.StartOffset.Value}"
                    : string.Empty);

            using (var response = await GetRetryPolicy().ExecuteAsync(
                       () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/appreviews/" +
                           queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JToken.Parse(result);
                if (jsonData["success"].ToString() != "1")
                {
                    throw new Exception("Bad request status.");
                }


                return jsonData.ToObject<ReviewsResponse>();
            }
        }

        /// <summary>
        /// Retrieves news data for the specified application.
        /// </summary>  
        /// <param name="request">A set of parameters for request.</param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>News for specified steam application.</returns>
        public async Task<SteamAppNews> GetAppNewsAsync(GetNewsRequest request,
            CancellationToken token = default)
        {
            string queryParameters = GetBaseQuery() + "&appid=" + request.AppId;
            if (request.EndDate.HasValue)
            {
                queryParameters += "&enddate=" + request.EndDate.Value.ToUnixTimeSeconds();
            }

            if (request.Count.HasValue)
            {
                queryParameters += "&count=" + request.Count.Value;
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                       () => _httpClient.GetAsync(_config.SteamPoweredBaseUrl +
                           "/ISteamNews/GetNewsForApp/v0002/" + queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it.
                var jsonData = JToken.Parse(result).First.First;

                return jsonData.ToObject<SteamAppNews>();
            }
        }

        /// <summary>
        /// Retrieves details for the specified application.
        /// </summary>  
        /// <param name="appId">Steam App ID.</param>
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="language">
        /// Full name of the language in english used for string localization e.g. title, 
        /// description, release dates.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for an application in the steam store.</returns>
        public async Task<SteamApp> GetSteamAppAsync(int appId, string countryCode = null,
            string language = null, CancellationToken token = default)
        {
            string queryParameters = $"?appids={appId}";
            queryParameters = string.IsNullOrWhiteSpace(countryCode)
                ? queryParameters
                : $"{queryParameters}&cc={countryCode}";

            queryParameters = string.IsNullOrWhiteSpace(language)
                ? queryParameters
                : $"{queryParameters}&l={language.ToLower()}";

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/appdetails" +
                          queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it.
                var jsonData = JToken.Parse(result).First.First;
                if (!bool.Parse(jsonData["success"].ToString()))
                {
                    throw new Exception("Bad request status.");
                }

                return jsonData["data"].ToObject<SteamApp>();
            }
        }

        /// <summary>
        /// Retrieves a list of featured items.
        /// </summary>  
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="language">
        /// Full name of the language in english used for string localization e.g. name, 
        /// description.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items in the steam store.</returns>
        public async Task<FeaturedApps> GetFeaturedAppsAsync(string countryCode = null,
            string language = null, CancellationToken token = default)
        {
            string queryParameters = string.IsNullOrWhiteSpace(countryCode)
                ? string.Empty
                : $"?cc={countryCode}";

            if (!string.IsNullOrWhiteSpace(language))
            {
                queryParameters += string.IsNullOrWhiteSpace(countryCode) ? "?" : "&";
                queryParameters += $"l={language.ToLower()}";
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/featured" +
                          queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JToken.Parse(result);
                if (jsonData["status"].ToString() != "1")
                {
                    throw new Exception("Bad request status.");
                }

                return FeaturedApps.FromJson(result);
            }
        }

        /// <summary>
        /// Retrieves a list of featured items, grouped by category.
        /// </summary>  
        /// <param name="countryCode">Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="language">
        /// Full name of the language in english used for string localization e.g. name, 
        /// description.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items, grouped by category, in the steam store.</returns>
        public async Task<FeaturedCategories> GetFeaturedCategoriesAsync(
            string countryCode = null, string language = null, CancellationToken token = default)
        {
            string queryParameters = string.IsNullOrWhiteSpace(countryCode)
                ? string.Empty
                : $"?cc={countryCode}";

            if (!string.IsNullOrWhiteSpace(language))
            {
                queryParameters += string.IsNullOrWhiteSpace(countryCode) ? "?" : "&";
                queryParameters += $"l={language.ToLower()}";
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl 
                          + "/api/featuredcategories" + queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JObject.Parse(result);
                if (jsonData["status"].ToString() != "1")
                {
                    throw new Exception("Bad request status.");
                }

                return jsonData.ToObject<FeaturedCategories>();
            }
        }

        /// <summary>
        /// Retrieves details for the specified package.
        /// </summary>  
        /// <param name="packageId">Steam package ID.</param>
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="language">
        /// Full name of the language in english used for string localization e.g. title, 
        /// description, release dates.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for a package in the steam store.</returns>
        public async Task<PackageInfo> GetPackageInfoAsync(int packageId, string countryCode = null,
            string language = null, CancellationToken token = default)
        {

            string queryParameters = $"?packageids={packageId}";
            queryParameters = string.IsNullOrWhiteSpace(countryCode)
                ? queryParameters
                : $"{queryParameters}&cc={countryCode}";

            queryParameters = string.IsNullOrWhiteSpace(language)
                ? queryParameters
                : $"{queryParameters}&l={language.ToLower()}";

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/packagedetails" +
                          queryParameters, token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it
                var jsonData = JToken.Parse(result).First.First;
                if (!bool.Parse(jsonData["success"].ToString()))
                {
                    throw new Exception("Bad request status.");
                }

                var package = jsonData["data"].ToObject<PackageInfo>();
                package.SteamPackageId = packageId;
                return package;
            }
        }

        #region IDisposable Implementation

        /// <summary>
        /// Clears managed resources. This class disposes only <see cref="HttpClient" />.
        /// </summary>
        public void Dispose()
        {
            if (!_disposedValue)
            {
                _disposedValue = true;

                _httpClient.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// Gets retry policy of client based on config.
        /// </summary>
        /// <returns>Created policy with specified in config parameters.</returns>
        private AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(
                r => r.StatusCode >= HttpStatusCode.InternalServerError
            )
            .WaitAndRetryAsync(
                _config.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(
                    Math.Pow(_config.RetryBackoffExponent, retryAttempt)
                )
            );
        }

        /// <summary>
        /// Creates base query with API key.
        /// </summary>
        /// <returns>Query string for request with optional key.</returns>
        private string GetBaseQuery()
        {
            return "?format=json" + (string.IsNullOrWhiteSpace(_config.ApiKey)
                ? string.Empty
                : "&key=" + _config.ApiKey
            );
        }
    }
}