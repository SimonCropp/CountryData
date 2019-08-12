using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Place : IPlace
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PostCode { get; set; }
        [DataMember]
        public Location Location { get; set; }

        [IgnoreDataMember]
        public ICommunity Community { get; set; }

        [IgnoreDataMember]
        ILocation IPlace.Location => Location;
    }
}