using System.Collections.Generic;

namespace CountryData
{
    public class Community
    {
        public string Name;
        public string Code;
        public List<Place> Places = new List<Place>();
    }
}