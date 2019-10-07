using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Province : IProvince
    {
        [DataMember]
        public string Name { get; set; } = null!;
        [DataMember]
        public string? Code { get; set; }

        [DataMember]
        public string? PostCode { get; set; }

        [DataMember]
        public List<Community> Communities { get; set; } = new List<Community>();

        [IgnoreDataMember]
        public IState State { get; set; } = null!;

        [IgnoreDataMember]
        IReadOnlyList<ICommunity> IProvince.Communities => Communities;
    }
}