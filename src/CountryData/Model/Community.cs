using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Community : ICommunity
    {
        [DataMember]
        public string Name { get; set; } = null!;
        [DataMember]
        public string? Code { get; set; }
        [DataMember]
        public List<Place> Places { get; set; } = new List<Place>();

        [IgnoreDataMember]
        public IProvince Province { get; set; } = null!;

        [DataMember]
        public string? PostCode { get; set; }

        [IgnoreDataMember]
        IReadOnlyList<IPlace> ICommunity.Places => Places;
    }
}