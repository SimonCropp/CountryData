using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class CountryInfo : ICountryInfo
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Iso { get; set; }
        [DataMember]
        public string Iso3 { get; set; }
        [DataMember]
        public ushort IsoNumeric { get; set; }
        [DataMember]
        public string Fips { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public double Area { get; set; }
        [DataMember]
        public uint Population { get; set; }
        [DataMember]
        public string Continent { get; set; }
        [DataMember]
        public string TopLevelDomain { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public string PhonePrefix { get; set; }
        [DataMember]
        public string PostCodeFormat { get; set; }
        [DataMember]
        public string PostCodeRegex { get; set; }

        [DataMember]
        public List<string> Languages { get; set; }

        [IgnoreDataMember]
        IReadOnlyList<string> ICountryInfo.Languages => Languages;
    }
}