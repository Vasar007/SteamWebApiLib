using System;
using System.Threading.Tasks;
using SteamWebApiLib;
using SteamWebApiLib.Models.AppDetails;
using SteamWebApiLib.Models.BriefInfo;
using SteamWebApiLib.Models.Featured;
using SteamWebApiLib.Models.FeaturedCategories;
using SteamWebApiLib.Models.PackageDetails;

namespace SteamWebApiLibConsoleApp
{
    internal static class Program
    {
        private static async Task Main()
        {
            try
            {
                await Examples();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static async Task Examples()
        {
            var steamApiClient = new SteamApiClient();

            // Get full list of SteamApps
            SteamAppBriefInfoList steamAppList = await steamApiClient.GetAppListAsync();

            // Get details for SteamApp with ID 443790
            SteamApp steamApp1 = await steamApiClient.GetSteamAppAsync(460810);

            // Get details for SteamApp with ID 443790 for region US
            SteamApp steamApp2 = await steamApiClient.GetSteamAppAsync(322330, "US");

            // Get details for Package with ID 68179 for region
            PackageInfo package1 = await steamApiClient.GetPackageInfoAsync(68179);

            // Get details for Package with ID 68179 for region JP
            PackageInfo package2 = await steamApiClient.GetPackageInfoAsync(68179, "JP");

            // Get a list of featured games
            FeaturedApps featured = await steamApiClient.GetFeaturedAppsAsync();

            // Get a list of featured games for region DE
            FeaturedApps featured2 = await steamApiClient.GetFeaturedAppsAsync("DE");

            // Get a list of featured games grouped by category
            FeaturedCategories featuredCategories = await steamApiClient.GetFeaturedCategoriesAsync();

            // Get a list of featured games grouped by category for region US
            FeaturedCategories featuredCategories2 = await steamApiClient.GetFeaturedCategoriesAsync("DE");
        }
    }
}
