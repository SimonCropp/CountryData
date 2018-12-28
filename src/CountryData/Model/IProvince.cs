using System.Collections.Generic;

namespace CountryData
{
    public interface IProvince
    {
        string Name { get; }
        string Code { get; }
        IReadOnlyList<ICommunity> Communities { get; }
    }
}