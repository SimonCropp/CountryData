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
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}