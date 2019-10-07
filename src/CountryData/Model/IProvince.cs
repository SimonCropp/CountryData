using System.Collections.Generic;

namespace CountryData
{
    public interface IProvince
    {
        string Name { get; }
        string? Code { get; }
        string? PostCode { get; }
        IReadOnlyList<ICommunity> Communities { get; }
        IState State { get; }
    }
}