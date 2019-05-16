# SteamWebApiLib

[![nuget](https://img.shields.io/nuget/v/SteamWebApiLib.svg)](https://www.nuget.org/packages/SteamWebApiLib)
[![License](https://img.shields.io/hexpm/l/plug.svg)](https://github.com/Vasar007/TIMLE/blob/master/LICENSE)

SteamWepApiLib is a .NET wrapper for the Steam API. It provides a set of methods to retrieve various data from the Steam API. Lib can interact with Steam Strorefront API and Steam Web API (each API provides different set of data).

The Steam API is not officially available or documented, all data in this library was either compiled by trial and error from the inofficial API documentation, and is therefore provided as-is.

## Usage examples

```cs
using SteamWebApiLib;
using SteamWebApiLib.Models.AppDetails;
using SteamWebApiLib.Models.BriefInfo;
using SteamWebApiLib.Models.Featured;
using SteamWebApiLib.Models.FeaturedCategories;
using SteamWebApiLib.Models.PackageDetails;

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

## Dependencies

Target .NET Standard 2.0 and version of C# is 7.1. Project uses further dependencies:

- Newtonsoft.Json v12.0.2;
- Polly v7.1.0.

You can install dependencies using NuGet package manager. Also all dependensies and this project have NuGet packages.

## Additional info

As a basis for project were taken [SteamStorefrontAPI](https://github.com/mmuffins/SteamStorefrontAPI) and [Narochno.Steam](https://github.com/Narochno/Narochno.Steam/).

## License information

This project is licensed under the terms of the [Apache License 2.0](LICENSE).
