using System.Collections.Generic;

namespace CountryData
{
    public interface ICommunity
    {
        string Name { get; }
        string Code { get; }
        string PostCode { get; }
        IReadOnlyList<IPlace> Places { get; }
        IProvince Province{ get; }
    }
}