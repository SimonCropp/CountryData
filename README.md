# CountryLocationData

Provides a .net wrapper around the [GeoNames Postal Codes](https://www.geonames.org/postal-codes/). Also exposes the data as per country json files.


## Json Files

List of country codes: https://raw.githubusercontent.com/SimonCropp/CountryData/master/countries.txt

```
https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/[CountryCode].json.txt
```

For example the url for Australia (AU) is 

https://raw.githubusercontent.com/SimonCropp/CountryData/master/Data/PostCodes/au.json.txt


### Structure 

The GeoNames data is structured as

State > Province > Community > Place

However many countries do not have data for every level. To keep the data small only nodes that have data are represented in the json. This structure for each country is stored in a `Structure` property at the root.

So a country with all levels would have a `Structure` property of `State/Province/Community/Place`. While a country with no Community values would be `Structure` property of `State/Province/Place`.


## Icon

<a href="https://thenounproject.com/term/world/956116/" target="_blank">World</a> designed by <a href="https://thenounproject.com/pedrosantospt3" target="_blank">Pedro Santos</a> from The Noun Project