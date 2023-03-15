using Bogus;
using Bogus.Premium;

namespace CountryData.Bogus;

public static class LocationDataExtensions
{
    public static CountryDataSet Country(this Faker faker) =>
        ContextHelper.GetOrSet(faker, () => new CountryDataSet(faker.Random));

    static Random random = new();

    public static IState State(this ICountry country) =>
        country.States.Random();

    public static IEnumerable<IState> States(this ICountry country, uint count) =>
        country.States.Random(count);

    public static IProvince Province(this ICountry country) =>
        AllProvinces(country).ToList().Random();

    public static IEnumerable<IProvince> Provinces(this ICountry country, uint count) =>
        AllProvinces(country).ToList().Random(count);

    public static ICommunity Community(this ICountry country) =>
        AllCommunities(country).ToList().Random();

    public static IEnumerable<ICommunity> Communities(this ICountry country, uint count) =>
        AllCommunities(country).ToList().Random(count);

    public static IPlace Place(this ICountry country) =>
        AllPlaces(country).ToList().Random();

    public static IEnumerable<IPlace> Places(this ICountry country, uint count) =>
        AllPlaces(country).ToList().Random(count);

    public static string PostCode(this ICountry country) =>
        Place(country).PostCode;

    public static IEnumerable<string> PostCode(this ICountry country, uint count) =>
        Places(country, count).Select(_ => _.PostCode);

    public static IEnumerable<ICommunity> AllCommunities(this ICountry country) =>
        AllProvinces(country).SelectMany(_ => _.Communities);

    public static IEnumerable<IPlace> AllPlaces(this ICountry country) =>
        AllCommunities(country).SelectMany(_ => _.Places);

    public static IEnumerable<IProvince> AllProvinces(this ICountry country) =>
        country.States.SelectMany(_ => _.Provinces);

    static T Random<T>(this IReadOnlyList<T> source)
    {
        var r = random.Next(source.Count);
        return source[r];
    }

    static IEnumerable<T> Random<T>(this IReadOnlyList<T> source, uint count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return source.Random();
        }
    }
}