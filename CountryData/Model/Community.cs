using System.Collections.Generic;

namespace CountryData
{
    public class Community : ICommunity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IReadOnlyList<IPlace> Places { get; set; } = new List<IPlace>();
    }
}