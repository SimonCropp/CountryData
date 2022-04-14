namespace CountryData;

public static class CountryExtensions
{
    public static IEnumerable<IProvince> Provinces(this ICountry country) =>
        country.States
            .SelectMany(x => x.Provinces)
            .OrderBy(x => x.Name);

    public static IEnumerable<ICommunity> Communities(this ICountry country) =>
        country.States
            .SelectMany(x => x.Provinces)
            .SelectMany(x => x.Communities)
            .OrderBy(x => x.Name);

    public static IEnumerable<IPlace> Places(this ICountry country) =>
        country.States
            .SelectMany(x => x.Provinces)
            .SelectMany(x => x.Communities)
            .SelectMany(x => x.Places)
            .OrderBy(x => x.Name);

    public static IReadOnlyDictionary<string, IReadOnlyList<IPlace>> PostCodes(this ICountry country) =>
        country.States
            .SelectMany(x => x.Provinces)
            .SelectMany(x => x.Communities)
            .SelectMany(x => x.Places)
            .GroupBy(x => x.PostCode)
            .ToDictionary(x => x.Key, x => (IReadOnlyList<IPlace>)x.ToList());
}