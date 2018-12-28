using System.Collections.Generic;

namespace CountryData
{
    public interface ICommunity
    {
        string Name { get; }
        string Code { get; }
        IReadOnlyList<IPlace> Places { get; }
    }
}