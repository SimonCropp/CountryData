using System.Runtime.Serialization;

namespace CountryData
{
    [DataContract]
    public class Location : ILocation
    {
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}