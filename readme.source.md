# <img src="/src/icon.png" height="30px"> CountryData

[![Build status](https://ci.appveyor.com/api/projects/status/bb8461c5js69pn4x/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/australianelectorates)
[![NuGet Status](https://img.shields.io/nuget/v/CountryData.svg)](https://www.nuget.org/packages/CountryData/)

Provides a .net wrapper around the [GeoNames Data](https://www.geonames.org/). Also exposes the data as per country json files.

toc


## NuGet Packages

The NuGets contain a static copy of all data. This data is embedded as resources inside the assembly. No network calls are done by the assembly. To get the latests version of the data do a NuGet update. There are several options to help keep a NuGet update:

 * [Dependabot](https://dependabot.com/): creates pull requests to keep your dependencies secure and up-to-date.
 * [Using NuGet wildcards](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards).
 * [Libraries.io](https://libraries.io/) supports subscribing to NuGet package updates.

https://nuget.org/packages/CountryData/

https://nuget.org/packages/CountryData.Bogus/


## Usage

snippet: usage


## Bogus Usage

snippet: bogususage


## Json Files


### Country Codes

List of country codes: https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/countrycodes.txt

```
https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/[CountryCode].json.txt
```


### Country Information

https://github.com/SimonCropp/CountryData/blob/master/Data/countryInfo.json.txt


### Country Location Data

For example the url for Australia (AU) is:

https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/AU.json.txt


### Structure

The GeoNames data is structured as:

Country > State > Province > Community > Place

However many countries do not have data for every level.


## Release Notes

See [closed milestones](../../milestones?state=closed).


## Icon

[World](https://thenounproject.com/term/world/956116/) designed by [Pedro Santos](https://thenounproject.com/pedrosantospt3) from [The Noun Project](https://thenounproject.com/pedrosantospt3).