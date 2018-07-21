using System.Collections.Generic;

namespace StateProvinceCommunity
{
    class Province
    {
        public string Name;
        public string Code;
        public List<Community> Communities  = new List<Community>();
    }
}