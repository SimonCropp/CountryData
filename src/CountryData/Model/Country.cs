using System.Collections.Generic;
using System.Diagnostics;

namespace CountryData
{
    public interface ICountry
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
        IReadOnlyList<IState> States { get; }
    }

    [DebuggerDisplay("Name='{Iso}', Code='{Iso}'")]
    class Country :
        ICountry
    {
        public string Name { get; set; } = null!;
        public string Iso { get; set; } = null!;
        public string Iso3 { get; set; } = null!;
        public ushort IsoNumeric { get; set; }
        public string Fips { get; set; } = null!;
        public string Capital { get; set; } = null!;
        public double Area { get; set; }
        public uint Population { get; set; }
        public string Continent { get; set; } = null!;
        public string TopLevelDomain { get; set; } = null!;
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public string PhonePrefix { get; set; } = null!;
        public string PostCodeFormat { get; set; } = null!;
        public string PostCodeRegex { get; set; } = null!;
        public IReadOnlyList<string> Languages { get; set; } = null!;
        public IReadOnlyList<IState> States { get; set; } = null!;
    }
}