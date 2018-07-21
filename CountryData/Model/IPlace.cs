namespace CountryData
{
    public interface IPlace
    {
        string Name { get; }
        string PostCode { get; }
        string LatLong { get; }
    }
}