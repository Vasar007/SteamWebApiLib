# SteamWebApiLib

[![nuget](https://img.shields.io/nuget/v/SteamWebApiLib.svg)](https://www.nuget.org/packages/SteamWebApiLib)
[![License](https://img.shields.io/hexpm/l/plug.svg)](https://github.com/Vasar007/SteamWebApiLib/blob/master/LICENSE)
[![AppVeyor branch](https://img.shields.io/appveyor/ci/Vasar007/SteamWebApiLib/master.svg)](https://ci.appveyor.com/project/Vasar007/steamwebapiLib)

SteamWepApiLib is a .NET wrapper for the Steam API. It provides a set of methods to retrieve various data from the Steam API. Lib can interact with Steam Strorefront API and Steam Web API (each API provides different set of data).

The Steam API is not officially available or documented, all data in this library was either compiled by trial and error from the inofficial API documentation, and is therefore provided as-is.

## Installation

Install [NuGet package](https://www.nuget.org/packages/SteamWebApiLib).

## Usage examples

```cs
using System;
using System.Threading.Tasks;
using SteamWebApiLib;
using SteamWebApiLib.Models.AppDetails;
using SteamWebApiLib.Models.BriefInfo;
using SteamWebApiLib.Models.Featured;
using SteamWebApiLib.Models.FeaturedCategories;
using SteamWebApiLib.Models.PackageDetails;

private static async Task Examples()
{
    var steamApiClient = new SteamApiClient();

    // Get full list of SteamApps.
    SteamAppBriefInfoList steamAppList = await steamApiClient.GetAppListAsync();
    Console.WriteLine($"Got {steamAppList.Apps.Length} items.");

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
    Console.WriteLine($"Got response for {package1.Name}.");

    // Get a list of featured games.
    FeaturedApps featured1 = await steamApiClient.GetFeaturedAppsAsync();
    Console.WriteLine($"Got {featured1.FeaturedWin.Length} items for Winodws.");

    // Get a list of featured games for region DE.
    FeaturedApps featured2 = await steamApiClient.GetFeaturedAppsAsync(CountryCode.Germany);
    Console.WriteLine($"Got {featured2.FeaturedWin.Length} items for Winodws.");

    // Get a list of featured games grouped by category.
    FeaturedCategories featuredCategories1 = await steamApiClient.GetFeaturedCategoriesAsync();
    Console.WriteLine($"Got {featuredCategories1.TopSellers.Items.Length} top sellers items.");

    // Get a list of featured games grouped by category for region US.
    FeaturedCategories featuredCategories2 = await steamApiClient.GetFeaturedCategoriesAsync(CountryCode.USA);
    Console.WriteLine($"Got {featuredCategories2.TopSellers.Items.Length} top sellers items.");
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

### Third party software and libraries used

Newtonsoft.Json https://www.newtonsoft.com/json

Copyright (c) 2007 James Newton-King

License (MIT) https://raw.githubusercontent.com/JamesNK/Newtonsoft.Json/master/LICENSE.md
