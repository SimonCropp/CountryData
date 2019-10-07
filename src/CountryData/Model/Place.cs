using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Place : IPlace
    {
        [DataMember]
        public string? Name { get; set; }
        [DataMember]
        public string PostCode { get; set; } = null!;
        [DataMember]
        public Location Location { get; set; } = null!;

        [IgnoreDataMember]
        public ICommunity Community { get; set; } = null!;

        [IgnoreDataMember]
        ILocation IPlace.Location => Location;
    }
}