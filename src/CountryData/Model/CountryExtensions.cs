namespace CountryData;

public static class CountryExtensions
{
    public static IEnumerable<IProvince> Provinces(this ICountry country) =>
        country.States
            .SelectMany(_ => _.Provinces)
            .OrderBy(_ => _.Name);

    public static IEnumerable<ICommunity> Communities(this ICountry country) =>
        country.States
            .SelectMany(_ => _.Provinces)
            .SelectMany(_ => _.Communities)
            .OrderBy(_ => _.Name);

    public static IEnumerable<IPlace> Places(this ICountry country) =>
        country.States
            .SelectMany(_ => _.Provinces)
            .SelectMany(_ => _.Communities)
            .SelectMany(_ => _.Places)
            .OrderBy(_ => _.Name);

    public static IReadOnlyDictionary<string, IReadOnlyList<IPlace>> PostCodes(this ICountry country) =>
        country.States
            .SelectMany(_ => _.Provinces)
            .SelectMany(_ => _.Communities)
            .SelectMany(_ => _.Places)
            .GroupBy(_ => _.PostCode)
            .ToDictionary(_ => _.Key, _ => (IReadOnlyList<IPlace>)_.ToList());
}