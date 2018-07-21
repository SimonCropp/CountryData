using System.Collections.Generic;

namespace CountryData
{
    public class State : IState
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IReadOnlyList<IProvince> Provinces { get; set; } = new List<IProvince>();
    }
}