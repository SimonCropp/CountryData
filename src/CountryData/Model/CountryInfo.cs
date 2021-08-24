using System.Collections.Generic;
using System.Diagnostics;

namespace CountryData
{
    public interface ICountryInfo
    {
        string Name { get; }
        string Iso { get; }
        string Iso3 { get; }
        ushort IsoNumeric { get; }
        Fips? Fips { get; }
        string Capital { get; }
        double? Area { get; }
        uint Population { get; }
        string Continent { get; }
        string TopLevelDomain { get; }
        CurrencyCode? CurrencyCode { get; }
        string? CurrencyName { get; }
        string PhonePrefix { get; }
        string PostCodeFormat { get; }
        string PostCodeRegex { get; }
        IReadOnlyList<string> Languages { get; }
    }

    [DebuggerDisplay("Name={Iso}, Code={Iso}")]
    class CountryInfo :
        ICountryInfo
    {
        public string Name { get; set; } = null!;
        public string Iso { get; set; } = null!;
        public string Iso3 { get; set; } = null!;
        public ushort IsoNumeric { get; set; }
        public Fips? Fips { get; set; }
        public string Capital { get; set; } = null!;
        public double? Area { get; set; }
        public uint Population { get; set; }
        public string Continent { get; set; } = null!;
        public string TopLevelDomain { get; set; } = null!;
        public CurrencyCode? CurrencyCode { get; set; }
        public string? CurrencyName { get; set; }
        public string PhonePrefix { get; set; } = null!;
        public string PostCodeFormat { get; set; } = null!;
        public string PostCodeRegex { get; set; } = null!;
        public IReadOnlyList<string> Languages { get; set; } = null!;
    }
}