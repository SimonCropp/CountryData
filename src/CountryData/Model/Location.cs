using System.Diagnostics;

namespace CountryData
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }
    }

    [DebuggerDisplay("Lat='{Latitude}', Long='{Longitude}'")]
    class Location :
        ILocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}