using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CountryData
{
    public interface ICommunity
    {
        string Name { get; }
        string? Code { get; }
        string? PostCode { get; }
        IReadOnlyList<IPlace> Places { get; }
        IProvince Province { get; }
    }

    [DebuggerDisplay("Name='{Name}', Code='{Code}'")]
    class Community :
        ICommunity
    {
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public IReadOnlyList<IPlace> Places { get; set; } = null!;
        [JsonIgnore][IgnoreDataMember]
        public IProvince Province { get; set; } = null!;
        public string? PostCode { get; set; }
    }
}