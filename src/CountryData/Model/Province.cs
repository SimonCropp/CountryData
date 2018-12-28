using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Province : IProvince
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public List<Community> Communities { get; set; } = new List<Community>();

        [IgnoreDataMember]
        IReadOnlyList<ICommunity> IProvince.Communities => Communities;
    }
}