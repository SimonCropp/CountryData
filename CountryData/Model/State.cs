using System.Collections.Generic;

namespace CountryData
{
    public class State
    {
        public string Name;
        public string Code;
        public List<Province> Provinces = new List<Province>();
    }
}