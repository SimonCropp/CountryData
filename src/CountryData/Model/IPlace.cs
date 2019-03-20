namespace CountryData
{
    public interface IPlace
    {
        string Name { get; }
        string PostCode { get; }
        double Latitude { get; }
        double Longitude { get; }
    }
}