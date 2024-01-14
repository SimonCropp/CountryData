using System.Runtime.Serialization;

namespace CountryData;

public interface IPlace
{
    string? Name { get; }
    string PostCode { get; }
    ILocation Location { get; }
    ICommunity Community { get; }
}

[DebuggerDisplay("Name={Name}, PostCode={PostCode}")]
class Place :
    IPlace
{
    public string? Name { get; set; }
    public string PostCode { get; set; } = null!;
    public ILocation Location { get; set; } = null!;
    [JsonIgnore][IgnoreDataMember]
    public ICommunity Community { get; set; } = null!;
}