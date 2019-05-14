# SteamWebApiLib
[![nuget](https://img.shields.io/nuget/v/SteamWebApiLib.svg)](https://www.nuget.org/packages/SteamWebApiLib)

SteamWepApiLib is a .NET wrapper for the Steam API. It provides a set of methods to retrieve various data from the Steam API. Lib can interact with Steam Strorefront API and Steam Web API (each API provides different set of data).

The Steam API is not officially available or documented, all data in this library was either compiled by trial and error from the inofficial API documentation, and is therefore provided as-is.

## Usage examples

```cs
using SteamWebApiLib;

public static async Task Examples()
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
    FeaturedCategories featuredCategories2 = await FeaturedCategories.GetFeaturedCategoriesAsync("DE");
}
```
