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
        private static async Task<int> Main()
        {
            try
            {
                Console.WriteLine("Console application started.");
                
                await Examples();

                return 0;
            }
            catch (Exception ex)
            {
                string exceptionMessage = $"Exception occurred in {nameof(Main)} method. " +
                                          $"{Environment.NewLine}{ex.ToString()}";
                Console.WriteLine(exceptionMessage);

                return -1;
            }
            finally
            {
                Console.WriteLine("Console application stopped.");
                Console.WriteLine("Press any key to close this window...");
                Console.ReadKey();
            }
        }

        private static async Task Examples()
        {
            var steamApiClient = new SteamApiClient();

            // Get full list of SteamApps.
            SteamAppBriefInfoList steamAppList = await steamApiClient.GetAppListAsync();
            Console.WriteLine($"Got {steamAppList.Apps.Length.ToString()} items.");

            // Get details for SteamApp with ID 292030 (The Witcher 3: Wild Hunt).
            SteamApp steamApp1 = await steamApiClient.GetSteamAppAsync(292030);
            Console.WriteLine($"Got response for {steamApp1.Name}.");

            // Get details for SteamApp with same ID for region US.
            SteamApp steamApp2 = await steamApiClient.GetSteamAppAsync(292030, CountryCode.USA);
            Console.WriteLine($"Got response for {steamApp2.Name}.");

            // Get details for Package with ID 68179 (Don't Starve Together).
            PackageInfo package1 = await steamApiClient.GetPackageInfoAsync(68179);
            Console.WriteLine($"Got response for {package1.Name}.");

            // Get details for Package with same ID for region JP.
            PackageInfo package2 = await steamApiClient.GetPackageInfoAsync(68179, CountryCode.Japan);
            Console.WriteLine($"Got response for {package2.Name}.");

            // Get a list of featured games.
            FeaturedApps featured1 = await steamApiClient.GetFeaturedAppsAsync();
            Console.WriteLine($"Got {featured1.FeaturedWin.Length.ToString()} items for Windows.");

            // Get a list of featured games for region DE.
            FeaturedApps featured2 = await steamApiClient.GetFeaturedAppsAsync(CountryCode.Germany);
            Console.WriteLine($"Got {featured2.FeaturedWin.Length.ToString()} items for Windows.");

            // Get a list of featured games grouped by category.
            FeaturedCategories featuredCategories1 = await steamApiClient.GetFeaturedCategoriesAsync();
            Console.WriteLine($"Got {featuredCategories1.TopSellers.Items.Length.ToString()} top sellers items.");

            // Get a list of featured games grouped by category for region US.
            FeaturedCategories featuredCategories2 = await steamApiClient.GetFeaturedCategoriesAsync(CountryCode.USA);
            Console.WriteLine($"Got {featuredCategories2.TopSellers.Items.Length.ToString()} top sellers items.");
        }
    }
}
