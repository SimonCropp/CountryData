using System.Collections.Generic;

namespace CountryData
{
    public class Province
    {
        public string Name;
        public string Code;
        public List<Community> Communities  = new List<Community>();
    }
}