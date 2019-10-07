using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CountryData
{
    public interface IProvince
    {
        string Name { get; }
        string? Code { get; }
        string? PostCode { get; }
        IReadOnlyList<ICommunity> Communities { get; }
        IState State { get; }
    }

    class Province :
        IProvince
    {
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public string? PostCode { get; set; }
        public IReadOnlyList<ICommunity> Communities { get; set; } = null!;
        [JsonIgnore][IgnoreDataMember]
        public IState State { get; set; } = null!;
    }
}