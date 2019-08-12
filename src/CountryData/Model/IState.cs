using System.Collections.Generic;

namespace CountryData
{
    public interface IState
    {
        string Name { get; }
        string Code { get; }
        string PostCode { get; }
        IReadOnlyList<IProvince> Provinces { get; }
        ICountry Country{ get; }
    }
}