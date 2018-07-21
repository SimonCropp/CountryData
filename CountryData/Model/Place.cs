namespace CountryData
{
    public class Place : IPlace
    {
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string LatLong { get; set; }
    }
}