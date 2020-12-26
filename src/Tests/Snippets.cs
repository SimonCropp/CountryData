using System.Linq;
using Bogus;
using CountryData;
using CountryData.Bogus;
using Xunit;
// ReSharper disable UnusedParameter.Local

// ReSharper disable UnusedVariable

public class Snippets
{
    [Fact]
    public void Bogus()
    {
        #region bogususage

        var faker = new Faker<Target>()
            .RuleFor(
                property: u => u.RandomCountryName,
                setter: (f, u) => f.Country().Name())
            //TODO
            //.RuleFor(
            //    property: u => u.RandomCountryCurrency,
            //    setter: (f, u) => f.Country().CurrencyCode())
            .RuleFor(
                property: u => u.AustralianCapital,
                setter: (f, u) => f.Country().Australia().Capital)
            .RuleFor(
                property: u => u.RandomIrelandState,
                setter: (f, u) => f.Country().Ireland().State().Name)
            .RuleFor(
                property: u => u.RandomIcelandPostCode,
                setter: (f, u) => f.Country().Iceland().PostCode());
        var targetInstance = faker.Generate();

        #endregion
    }

    [Fact]
    public void Normal()
    {
        #region usage

        // All country info. This is only the country metadata
        // and not all locationData.
        var allCountryInfo = CountryLoader.CountryInfo;
        var costaRicaInfo = allCountryInfo.Single(x => x.Iso == "CR");

        // Loads all location data for a specific country
        var australiaData = CountryLoader.LoadAustraliaLocationData();
        var name = australiaData.Name;
        var state = australiaData.States.First();
        var province = state.Provinces.First();
        var community = province.Communities.First();
        var place = community.Places.First();
        var postCode = place.PostCode;
        var placeName = place.Name;
        var latitude = place.Location.Latitude;
        var longitude = place.Location.Longitude;

        #endregion
    }

    public class Target
    {
        public string? AustralianCapital;
        public string? RandomCountryCurrency;
        public string? RandomCountryName;
        public string? RandomIrelandState;
        public string? RandomIcelandPostCode;
    }
}