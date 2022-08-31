using Bogus;
// ReSharper disable IdentifierTypo

namespace CountryData.Bogus;

public partial class CountryDataSet : DataSet
{
    public IEnumerable<string> Names(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return Name();
        }
    }

    public string Name() =>
        CountryInfo().Name;

    public IEnumerable<string?> Capitals(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return Capital();
        }
    }

    public string? Capital() =>
        CountryInfo().Capital;

    public IEnumerable<string> Continents(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return Continent();
        }
    }

    public string Continent() =>
        CountryInfo().Continent;

    public IEnumerable<string?> PhonePrefixs(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return PhonePrefix();
        }
    }

    public string? PhonePrefix() =>
        CountryInfo().PhonePrefix;

    public IEnumerable<string?> PostCodeFormats(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return PostCodeFormat();
        }
    }

    public string? PostCodeFormat() =>
        CountryInfo().PostCodeFormat;

    public IEnumerable<string?> PostCodeRegexs(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return PostCodeRegex();
        }
    }

    public string? PostCodeRegex() =>
        CountryInfo().PostCodeRegex;

    public IEnumerable<string?> TopLevelDomains(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return TopLevelDomain();
        }
    }

    public string? TopLevelDomain() =>
        CountryInfo().TopLevelDomain;

    public IEnumerable<ushort> IsoNumerics(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return IsoNumeric();
        }
    }

    public ushort IsoNumeric() =>
        CountryInfo().IsoNumeric;

    public IEnumerable<ICountryInfo> CountryInfo(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return CountryInfo();
        }
    }

    public ICountryInfo CountryInfo()
    {
        var index = Random.Number(CountryLoader.CountryInfo.Count - 1);
        return CountryLoader.CountryInfo[index];
    }
}