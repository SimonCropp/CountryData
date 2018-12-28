﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class State : IState
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public List<Province> Provinces { get; set; } = new List<Province>();

        [IgnoreDataMember]
        IReadOnlyList<IProvince> IState.Provinces => Provinces;
    }
}