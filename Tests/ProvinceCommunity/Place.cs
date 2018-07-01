namespace ProvinceCommunity
{
    class Place
    {
        public string Name;
        public string PostCode;
        public string LatLong { get; set; }
        public ushort? Accuracy;
    }
}
