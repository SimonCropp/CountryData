using System.Collections.Generic;

namespace CountryData
{
    public class Province : IProvince
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IReadOnlyList<ICommunity> Communities { get; set; } = new List<Community>();
    }
}