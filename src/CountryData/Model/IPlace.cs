namespace CountryData
{
    public interface IPlace
    {
        string Name { get; }
        string PostCode { get; }
        ILocation Location { get; }
    }
}