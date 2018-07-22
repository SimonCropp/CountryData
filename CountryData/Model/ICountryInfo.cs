using System.Collections.Generic;

namespace CountryData
{
    public interface ICountryInfo
    {
        string Name { get; }
        string Iso { get; }
        string Iso3 { get; }
        ushort IsoNumeric { get; }
        string Fips { get; }
        string Capital { get; }
        double Area { get; }
        uint Population { get; }
        string Continent { get; }
        string TopLevelDomain { get; }
        string CurrencyCode { get; }
        string CurrencyName { get; }
        string PhonePrefix { get; }
        string PostCodeFormat { get; }
        string PostCodeRegex { get; }
        IReadOnlyList<string> Languages { get; }
    }
}