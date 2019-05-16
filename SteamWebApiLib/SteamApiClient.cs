using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using SteamWebApiLib.Exceptions;
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
    /// <para>Client which can make a requests to the Steam API.</para>
    /// Client uses Retry Policy and will try to get response several times (you can specify 
    /// attempts number in config). If no response was received or an error code was received, an 
    /// exception will be thrown.
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
            // Another option: https://partner.steamgames.com/doc/store/getreviews

            var queryParameters = new QueryParametersBuilder();
            queryParameters.AppendParameter("json", "1");
            queryParameters.AppendParameter("filter", request.Filter.ToString().ToLower());
            queryParameters.AppendParameter("language", request.Language);
            queryParameters.AppendParameter("review_type", request.ReviewType.ToString().ToLower());
            queryParameters.AppendParameter(
                "purchase_type", request.PurchaseType.ToString().ToLower()
            );
            if (request.DayRange.HasValue)
            {
                queryParameters.AppendParameter("day_range", request.DayRange.Value.ToString());
            }
            if (request.StartOffset.HasValue)
            {
                queryParameters.AppendParameter(
                    "start_offset", request.StartOffset.Value.ToString()
                );
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                       () => _httpClient.GetAsync(_config.SteamStoreBaseUrl +
                           $"/appreviews/{request.AppId}" + queryParameters.ToString(), token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JToken.Parse(result);
                if (jsonData["success"].ToString() != "1")
                {
                    throw new SteamApiBadRequestException(
                        "Bad request status. Check if you specified rigth parameters."
                    );
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
            var queryParameters = new QueryParametersBuilder();
            queryParameters.AppendParameter("format", "json");
            queryParameters.AppendParameter("key", _config.ApiKey);
            queryParameters.AppendParameter("appid", request.AppId.ToString());
            if (request.EndDate.HasValue)
            {
                queryParameters.AppendParameter(
                    "enddate", request.EndDate.Value.ToUnixTimeSeconds().ToString()
                );
            }
            if (request.Count.HasValue)
            {
                queryParameters.AppendParameter("count", request.Count.Value.ToString());
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                       () => _httpClient.GetAsync(_config.SteamPoweredBaseUrl +
                           "/ISteamNews/GetNewsForApp/v0002/" + queryParameters.ToString(), token)))
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
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for an application in the steam store.</returns>
        public async Task<SteamApp> GetSteamAppAsync(int appId, CancellationToken token = default)
        {
            return await GetSteamAppAsync(appId, CountryCode.Unknown, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves details for the specified application. Some of the data in response may vary 
        /// for a given country code.
        /// </summary>  
        /// <param name="appId">Steam App ID.</param>
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for an application in the steam store.</returns>
        public async Task<SteamApp> GetSteamAppAsync(int appId, CountryCode countryCode,
            CancellationToken token = default)
        {
            return await GetSteamAppAsync(appId, countryCode, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves details for the specified application. Some of the data in response may vary 
        /// for a given country code and language.
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
        public async Task<SteamApp> GetSteamAppAsync(int appId, CountryCode countryCode,
            Language language, CancellationToken token = default)
        {
            var queryParameters = new QueryParametersBuilder();
            queryParameters.AppendParameter("appids", appId.ToString());
            if (countryCode != CountryCode.Unknown)
            {
                queryParameters.AppendParameter(
                    "cc", CountryCodeConverter.GetCountryCodeStringValue(countryCode)
                );
            }
            if (language != Language.Unknown)
            {
                queryParameters.AppendParameter("l", language.ToString().ToLower());
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/appdetails" +
                          queryParameters.ToString(), token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it.
                var jsonData = JToken.Parse(result).First.First;
                if (!bool.Parse(jsonData["success"].ToString()))
                {
                    throw new SteamApiBadRequestException(
                        "Bad request status. Check if you specified right AppID."
                    );
                }

                return jsonData["data"].ToObject<SteamApp>();
            }
        }

        /// <summary>
        /// Retrieves a list of featured items.
        /// </summary>  
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items in the steam store.</returns>
        public async Task<FeaturedApps> GetFeaturedAppsAsync(CancellationToken token = default)
        {
            return await GetFeaturedAppsAsync(CountryCode.Unknown, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves a list of featured items. Some of the data in response may vary for a given 
        /// country code.
        /// </summary>  
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items in the steam store.</returns>
        public async Task<FeaturedApps> GetFeaturedAppsAsync(CountryCode countryCode,
            CancellationToken token = default)
        {
            return await GetFeaturedAppsAsync(countryCode, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves a list of featured items. Some of the data in response may vary for a given 
        /// country code and language.
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
        public async Task<FeaturedApps> GetFeaturedAppsAsync(CountryCode countryCode,
            Language language, CancellationToken token = default)
        {
            var queryParameters = new QueryParametersBuilder();
            if (countryCode != CountryCode.Unknown)
            {
                queryParameters.AppendParameter(
                    "cc", CountryCodeConverter.GetCountryCodeStringValue(countryCode)
                );
            }
            if (language != Language.Unknown)
            {
                queryParameters.AppendParameter("l", language.ToString().ToLower());
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/featured" +
                          queryParameters.ToString(), token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JToken.Parse(result);
                if (jsonData["status"].ToString() != "1")
                {
                    throw new SteamApiBadRequestException(
                        "Bad request status. Check if you specified right parameters."
                    );
                }

                return FeaturedApps.FromJson(result);
            }
        }

        /// <summary>
        /// Retrieves a list of featured items, grouped by category.
        /// </summary>  
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items, grouped by category, in the steam store.</returns>
        public async Task<FeaturedCategories> GetFeaturedCategoriesAsync(
            CancellationToken token = default)
        {
            return await GetFeaturedCategoriesAsync(CountryCode.Unknown, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves a list of featured items, grouped by category. Some of the data in response 
        /// may vary for a given country code.
        /// </summary>  
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items, grouped by category, in the steam store.</returns>
        public async Task<FeaturedCategories> GetFeaturedCategoriesAsync(CountryCode countryCode,
            CancellationToken token = default)
        {
            return await GetFeaturedCategoriesAsync(countryCode, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves a list of featured items, grouped by category. Some of the data in response 
        /// may vary for a given country code and language.
        /// </summary>  
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="language">
        /// Full name of the language in english used for string localization e.g. name, 
        /// description.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>A list of featured items, grouped by category, in the steam store.</returns>
        public async Task<FeaturedCategories> GetFeaturedCategoriesAsync(CountryCode countryCode,
            Language language, CancellationToken token = default)
        {
            var queryParameters = new QueryParametersBuilder();
            if (countryCode != CountryCode.Unknown)
            {
                queryParameters.AppendParameter(
                    "cc", CountryCodeConverter.GetCountryCodeStringValue(countryCode)
                );
            }
            if (language != Language.Unknown)
            {
                queryParameters.AppendParameter("l", language.ToString().ToLower());
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl 
                          + "/api/featuredcategories" + queryParameters.ToString(), token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var jsonData = JObject.Parse(result);
                if (jsonData["status"].ToString() != "1")
                {
                    throw new SteamApiBadRequestException(
                        "Bad request status. Check if you specified right parameters."
                    );
                }

                return jsonData.ToObject<FeaturedCategories>();
            }
        }

        /// <summary>
        /// Retrieves details for the specified package.
        /// </summary>  
        /// <param name="packageId">Steam package ID.</param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for a package in the steam store.</returns>
        public async Task<PackageInfo> GetPackageInfoAsync(int packageId,
            CancellationToken token = default)
        {
            return await GetPackageInfoAsync(packageId, CountryCode.Unknown, Language.Unknown,
                                             token);
        }

        /// <summary>
        /// Retrieves details for the specified package. Some of the data in response may vary for 
        /// a given country code.
        /// </summary>  
        /// <param name="packageId">Steam package ID.</param>
        /// <param name="countryCode">
        /// Two letter country code to customise currency and date values.
        /// </param>
        /// <param name="token">Propogates notification that operation should be cancelled.</param>
        /// <returns>Details for a package in the steam store.</returns>
        public async Task<PackageInfo> GetPackageInfoAsync(int packageId, CountryCode countryCode,
            CancellationToken token = default)
        {
            return await GetPackageInfoAsync(packageId, countryCode, Language.Unknown, token);
        }

        /// <summary>
        /// Retrieves details for the specified package. Some of the data in response may vary for 
        /// a given country code and language.
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
        public async Task<PackageInfo> GetPackageInfoAsync(int packageId, CountryCode countryCode,
            Language language, CancellationToken token = default)
        {
            var queryParameters = new QueryParametersBuilder();
            queryParameters.AppendParameter("packageids", packageId.ToString());
            if (countryCode != CountryCode.Unknown)
            {
                queryParameters.AppendParameter(
                    "cc", CountryCodeConverter.GetCountryCodeStringValue(countryCode)
                );
            }
            if (language != Language.Unknown)
            {
                queryParameters.AppendParameter("l", language.ToString().ToLower());
            }

            using (var response = await GetRetryPolicy().ExecuteAsync(
                      () => _httpClient.GetAsync(_config.SteamStoreBaseUrl + "/api/packagedetails" +
                          queryParameters.ToString(), token)))
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                // The actual payload is wrapped, drill down to the third level to get to it
                var jsonData = JToken.Parse(result).First.First;
                if (!bool.Parse(jsonData["success"].ToString()))
                {
                    throw new SteamApiBadRequestException(
                        "Bad request status. Check if you specified right PackageID."
                    );
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
    }
}