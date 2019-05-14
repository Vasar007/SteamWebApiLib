using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using SteamWebApiLib;
using SteamWebApiLib.Models.Requests;
using SteamWebApiLib.Models.Common;

namespace SteamWebApiLibTests
{
    public class SteamApiClientTests
    {
        private readonly SteamApiClient _steamApiClient;


        public SteamApiClientTests()
        {
            _steamApiClient = new SteamApiClient();
        }

        private static void CheckFeatureadAppsCollections(
            IReadOnlyList<SteamAppInfo> featuredCollection)
        {
            Assert.NotEmpty(featuredCollection);
            Assert.All(featuredCollection, featuredApp =>
            {
                Assert.NotNull(featuredApp);
                Assert.NotNull(featuredApp.Currency);
                Assert.NotNull(featuredApp.HeaderImage);
                Assert.True(featuredApp.Id > 0);
                Assert.NotNull(featuredApp.LargeCapsuleImage);
                Assert.NotNull(featuredApp.Name);
                Assert.NotNull(featuredApp.SmallCapsuleImage);
                Assert.True(featuredApp.Type >= 0);
            });
        }

        [Fact]
        public async Task TestGetAppList()
        {
            var appList = await _steamApiClient.GetAppListAsync();

            Assert.NotNull(appList);
            Assert.NotNull(appList.Apps);

            Assert.NotEmpty(appList.Apps);
            Assert.All(appList.Apps, app =>
            {
                Assert.NotNull(app);
                Assert.True(app.AppId > 0);
                Assert.NotNull(app.Name);
            });
        }

        [Fact]
        public async Task TestGetAppNews()
        {
            int mortalKombat11AppId = 976310;

            var appNews = await _steamApiClient.GetAppNewsAsync(
                new GetNewsRequest(mortalKombat11AppId)
            );

            Assert.NotNull(appNews);
            Assert.Equal(mortalKombat11AppId, appNews.AppId);
            Assert.NotNull(appNews.NewsItems);

            Assert.NotEmpty(appNews.NewsItems);
            Assert.All(appNews.NewsItems, newsItem =>
            {
                Assert.NotNull(newsItem);
                Assert.True(newsItem.AppId > 0);
                Assert.NotNull(newsItem.Author);
                Assert.NotNull(newsItem.Contents);
                Assert.NotNull(newsItem.FeedLabel);
                Assert.NotNull(newsItem.FeedName);
                Assert.NotNull(newsItem.Title);
                Assert.NotNull(newsItem.Url);
            });
        }

        [Fact]
        public async Task TestGetFeaturedApps()
        {
            var featuredApps = await _steamApiClient.GetFeaturedAppsAsync();

            Assert.NotNull(featuredApps);
            Assert.NotNull(featuredApps.FeaturedLinux);
            Assert.NotNull(featuredApps.FeaturedMac);
            Assert.NotNull(featuredApps.FeaturedWin);
            Assert.NotNull(featuredApps.LargeCapsules);
            Assert.NotNull(featuredApps.Layout);

            int featuredAppsCount = 10;
            Assert.Equal(featuredAppsCount, featuredApps.FeaturedLinux.Length);
            Assert.Equal(featuredAppsCount, featuredApps.FeaturedMac.Length);
            Assert.Equal(featuredAppsCount, featuredApps.FeaturedWin.Length);

            CheckFeatureadAppsCollections(featuredApps.FeaturedLinux);
            CheckFeatureadAppsCollections(featuredApps.FeaturedMac);
            CheckFeatureadAppsCollections(featuredApps.FeaturedWin);
        }

        [Fact]
        public async Task TestGetFeaturedCategories()
        {
            var featuredCategories = await _steamApiClient.GetFeaturedCategoriesAsync();

            Assert.NotNull(featuredCategories);
            Assert.NotNull(featuredCategories.ComingSoon);
            Assert.NotNull(featuredCategories.Genres);
            Assert.NotNull(featuredCategories.NewReleases);
            Assert.NotNull(featuredCategories.Specials);
            Assert.NotNull(featuredCategories.TopSellers);
            Assert.NotNull(featuredCategories.TrailerSlideshow);

            CheckFeatureadAppsCollections(featuredCategories.ComingSoon.Items);
            CheckFeatureadAppsCollections(featuredCategories.NewReleases.Items);
            CheckFeatureadAppsCollections(featuredCategories.Specials.Items);
            CheckFeatureadAppsCollections(featuredCategories.TopSellers.Items);
        }

        [Fact]
        public async Task TestGetPackageInfo()
        {
            int dontStarveTogetherPackageId = 68179;

            var packageInfo = await _steamApiClient.GetPackageInfoAsync(
                dontStarveTogetherPackageId, "US"
            );

            Assert.NotNull(packageInfo);
            Assert.NotNull(packageInfo.Apps);
            Assert.NotNull(packageInfo.Controller);
            Assert.NotNull(packageInfo.Name);
            Assert.NotNull(packageInfo.PageImage);
            Assert.NotNull(packageInfo.Platforms);
            Assert.NotNull(packageInfo.Price);
            Assert.NotNull(packageInfo.PurchaseText);
            Assert.NotNull(packageInfo.ReleaseDate);
            Assert.NotNull(packageInfo.SmallLogo);
            Assert.Equal(dontStarveTogetherPackageId, packageInfo.SteamPackageId);

            Assert.NotEmpty(packageInfo.Apps);
            Assert.All(packageInfo.Apps, app =>
            {
                Assert.NotNull(app);
                Assert.True(app.Id > 0);
                Assert.NotNull(app.Name);
            });
        }

        [Fact]
        public async Task TestGetReviewsInfo()
        {
            int teamFortress2AppId = 440;

            var reviewsResponse = await _steamApiClient.GetReviewsAsync(
                new GetReviewsRequest(teamFortress2AppId)
            );

            Assert.NotNull(reviewsResponse);
            Assert.NotNull(reviewsResponse.QuerySummary);
            Assert.NotNull(reviewsResponse.Reviews);

            Assert.NotEmpty(reviewsResponse.Reviews);
            Assert.All(reviewsResponse.Reviews, review =>
            {
                Assert.NotNull(review);
                Assert.NotNull(review.Author);
                Assert.NotNull(review.Comment);
                Assert.True(review.CommentCount >= 0);
                Assert.True(review.VotesFunny >= 0);
                Assert.True(review.VotesUp >= 0);
            });
        }

        [Fact]
        public async Task TestGetSteamApp()
        {
            int teamFortress2AppId = 440;

            var steamApp = await _steamApiClient.GetSteamAppAsync(teamFortress2AppId);

            Assert.NotNull(steamApp);
            Assert.NotNull(steamApp.AboutTheGame);
            Assert.NotNull(steamApp.Achievements);
            Assert.NotNull(steamApp.Background);
            Assert.NotNull(steamApp.Categories);
            Assert.NotEmpty(steamApp.Categories);
            Assert.NotNull(steamApp.DetailedDescription);
            Assert.NotNull(steamApp.Developers);
            Assert.NotEmpty(steamApp.Developers);
            Assert.NotNull(steamApp.DLC);
            Assert.NotNull(steamApp.Genres);
            Assert.NotEmpty(steamApp.Genres);
            Assert.NotNull(steamApp.HeaderImage);
            Assert.NotNull(steamApp.LinuxRequirements);
            Assert.NotNull(steamApp.MacRequirements);
            Assert.NotNull(steamApp.Movies);
            Assert.NotEmpty(steamApp.Movies);
            Assert.NotNull(steamApp.Name);
            Assert.NotNull(steamApp.PackageGroups);
            Assert.NotEmpty(steamApp.PackageGroups);
            Assert.NotNull(steamApp.Packages);
            Assert.NotEmpty(steamApp.Packages);
            Assert.NotNull(steamApp.PcRequirements);
            Assert.NotNull(steamApp.Platforms);
            Assert.NotNull(steamApp.Publishers);
            Assert.NotEmpty(steamApp.Publishers);
            Assert.NotNull(steamApp.Recommendations);
            Assert.NotNull(steamApp.ReleaseDate);
            Assert.True(steamApp.RequiredAge >= 0);
            Assert.NotNull(steamApp.Screenshots);
            Assert.NotEmpty(steamApp.Screenshots);
            Assert.NotNull(steamApp.ShortDescription);
            Assert.Equal(teamFortress2AppId, steamApp.SteamAppId);
            Assert.NotNull(steamApp.SupportedLanguages);
            Assert.NotNull(steamApp.SupportInfo);
            Assert.NotNull(steamApp.Type);
            Assert.NotNull(steamApp.Website);
        }
    }
}
