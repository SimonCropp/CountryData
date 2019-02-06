# CountryLocationData

Provides a .net wrapper around the [GeoNames Data](https://www.geonames.org/). Also exposes the data as per country json files.s


## NuGets

The NuGets contain a static copy of all data. This data is embedded as resources inside the assembly. No network calls are done by the assembly. To get the latests version of the data do a NuGet update. There are several options to help keep a NuGet update:

 * [Dependabot](https://dependabot.com/): creates pull requests to keep your dependencies secure and up-to-date.
 * [Using NuGet wildcards](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards).
 * [Libraries.io](https://libraries.io/) supports subscribing to NuGet package updates.

https://nuget.org/packages/CountryData/ [![NuGet Status](http://img.shields.io/nuget/v/CountryData.svg?longCache=true&style=flat)](https://www.nuget.org/packages/CountryData/)

    PM> Install-Package CountryData


https://nuget.org/packages/CountryData.Bogus/ [![NuGet Status](http://img.shields.io/nuget/v/CountryData.Bogus.svg?longCache=true&style=flat)](https://www.nuget.org/packages/CountryData.Bogus/)

    PM> Install-Package CountryData.Bogus


## Usage

snippet: usage


## Bogus Usage

snippet: bogususage


## Json Files


### Country Codes

List of country codes: https://raw.githubusercontent.com/SimonCropp/CountryData/master/data/countrycodes.txt

```
https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/[CountryCode].json.txt
```


### Country Information

https://github.com/SimonCropp/CountryData/blob/master/Data/countryInfo.json.txt


### Country Location Data

For example the url for Australia (AU) is:

https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/au.json.txt


### Structure

The GeoNames data is structured as:

State > Province > Community > Place

However many countries do not have data for every level.


## Icon

<a href="https://thenounproject.com/term/world/956116/" target="_blank">World</a> designed by <a href="https://thenounproject.com/pedrosantospt3" target="_blank">Pedro Santos</a> from The Noun Project