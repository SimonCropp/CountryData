using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class CountryInfo : ICountryInfo
    {
        [DataMember]
        public string Name { get; set; } = null!;
        [DataMember]
        public string Iso { get; set; } = null!;
        [DataMember]
        public string Iso3 { get; set; } = null!;
        [DataMember]
        public ushort IsoNumeric { get; set; }
        [DataMember]
        public string Fips { get; set; } = null!;
        [DataMember]
        public string Capital { get; set; } = null!;
        [DataMember]
        public double Area { get; set; }
        [DataMember]
        public uint Population { get; set; }
        [DataMember]
        public string Continent { get; set; } = null!;
        [DataMember]
        public string TopLevelDomain { get; set; } = null!;
        [DataMember]
        public string CurrencyCode { get; set; } = null!;
        [DataMember]
        public string CurrencyName { get; set; } = null!;
        [DataMember]
        public string PhonePrefix { get; set; } = null!;
        [DataMember]
        public string PostCodeFormat { get; set; } = null!;
        [DataMember]
        public string PostCodeRegex { get; set; } = null!;

        [DataMember]
        public List<string> Languages { get; set; } = null!;

        [IgnoreDataMember]
        IReadOnlyList<string> ICountryInfo.Languages => Languages;
    }
}