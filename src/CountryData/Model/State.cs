using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CountryData
{
    public interface IState
    {
        string Name { get; }
        string? Code { get; }
        string? PostCode { get; }
        IReadOnlyList<IProvince> Provinces { get; }
        ICountry Country { get; }
    }

    [DebuggerDisplay("Name={Name}, Code={Code}, PostCode={PostCode}")]
    class State :
        IState
    {
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public string? PostCode { get; set; }
        public IReadOnlyList<IProvince> Provinces { get; set; } = null!;
        [JsonIgnore][IgnoreDataMember]
        public ICountry Country { get; set; } = null!;
    }
}