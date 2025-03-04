# <img src="/src/icon.png" height="30px"> CountryData

[![Build status](https://ci.appveyor.com/api/projects/status/bb8461c5js69pn4x/branch/main?svg=true)](https://ci.appveyor.com/project/SimonCropp/countrydata)
[![NuGet Status](https://img.shields.io/nuget/v/CountryData.svg?label=CountryData)](https://www.nuget.org/packages/CountryData/)
[![NuGet Status](https://img.shields.io/nuget/v/CountryData.Bogus.svg?label=CountryData.Bogus)](https://www.nuget.org/packages/CountryData.Bogus/)

Provides a .net wrapper around the [GeoNames Data](https://www.geonames.org/). Also exposes the data as per country json files.

**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet packages

 * https://nuget.org/packages/CountryData/
 * https://nuget.org/packages/CountryData.Bogus/

The NuGets contain a static copy of all data. This data is embedded as resources inside the assembly. No network calls are done by the assembly. To get the latests version of the data do a NuGet update. There are several options to help keep a NuGet update:

 * [Dependabot](https://dependabot.com/): creates pull requests to keep dependencies secure and up-to-date.
 * [Using NuGet wildcards](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards).
 * [Libraries.io](https://libraries.io/) supports subscribing to NuGet package updates.


## Usage

<!-- snippet: usage -->
<a id='snippet-usage'></a>
```cs
// All country info. This is only the country metadata
// and not all locationData.
var allCountryInfo = CountryLoader.CountryInfo;
var costaRicaInfo = allCountryInfo.Single(_ => _.Iso == "CR");

// Loads all location data for a specific country
var australiaData = CountryLoader.LoadAustraliaLocationData();
var name = australiaData.Name;
var state = australiaData.States[0];
var province = state.Provinces[0];
var community = province.Communities[0];
var place = community.Places[0];
var postCode = place.PostCode;
var placeName = place.Name;
var latitude = place.Location.Latitude;
var longitude = place.Location.Longitude;
```
<sup><a href='/src/Tests/Snippets.cs#L33-L52' title='Snippet source file'>snippet source</a> | <a href='#snippet-usage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Bogus Usage

<!-- snippet: bogususage -->
<a id='snippet-bogususage'></a>
```cs
var faker = new Faker<Target>()
    .RuleFor(
        property: u => u.RandomCountryName,
        setter: (f, u) => f.Country().Name())
    .RuleFor(
        property: u => u.AustralianCapital,
        setter: (f, u) => CountryDataSet.Australia().Capital)
    .RuleFor(
        property: u => u.RandomIrelandState,
        setter: (f, u) => CountryDataSet.Ireland().State().Name)
    .RuleFor(
        property: u => u.RandomIcelandPostCode,
        setter: (f, u) => CountryDataSet.Iceland().PostCode());
var targetInstance = faker.Generate();
```
<sup><a href='/src/Tests/Snippets.cs#L10-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-bogususage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


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


## Icon

[World](https://thenounproject.com/term/world/956116/) designed by [Pedro Santos](https://thenounproject.com/pedrosantospt3) from [The Noun Project](https://thenounproject.com/pedrosantospt3).
