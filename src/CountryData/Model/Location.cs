namespace CountryData
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }
    }

    class Location :
        ILocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}