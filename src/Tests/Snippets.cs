using System.Linq;
using Bogus;
using CountryData;
using CountryData.Bogus;
using Xunit;

// ReSharper disable UnusedVariable

public class Snippets
{
    [Fact]
    public void Bogus()
    {
        #region bogususage

        var faker = new Faker<Target>()
            .RuleFor(u => u.RandomCountryName, (f, u) => f.Country().Name())
            .RuleFor(u => u.RandomCountryCurrency, (f, u) => f.Country().CurrencyCode())
            .RuleFor(u => u.AustralianCapital, (f, u) => f.Country().Australia().Capital)
            .RuleFor(u => u.RandomIrelandState, (f, u) => f.Country().Ireland().State().Name)
            .RuleFor(u => u.RandomIcelandPostCode, (f, u) => f.Country().Iceland().PostCode());
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
        var latLong = place.LatLong;

        #endregion
    }

    public class Target
    {
        public string AustralianCapital;
        public string RandomCountryCurrency;
        public string RandomCountryName;
        public string RandomIrelandState;
        public string RandomIcelandPostCode;
    }
}