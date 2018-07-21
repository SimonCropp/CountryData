using System.Collections.Generic;

namespace CountryData
{
    public interface IState
    {
        string Name { get; }
        string Code { get; }
        IReadOnlyList<IProvince> Provinces { get; }
    }
}