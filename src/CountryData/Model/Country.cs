using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    public class Country : ICountry
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
        public IReadOnlyList<State> States { get; set; } = null!;
        [IgnoreDataMember]
        IReadOnlyList<IState> ICountry.States => States;
    }
}