using System.Collections.Generic;
using System.Linq;
using CountryData;

static class CountryInfoRowReader
{
    public static IEnumerable<CountryInfo> ReadRows(string path)
    {
        foreach (var split in RowReader.ReadRows(path))
        {
            var row = new CountryInfo
            {
                Iso = split[0]!,
                Iso3 = split[1]!,
                IsoNumeric = ushort.Parse(split[2]!),
                Fips = split[3]!,
                Name = split[4]!,
                Capital = split[5]!,
                //Population = uint.Parse(split[7]),
                Continent = split[8]!,
                TopLevelDomain = split[9]!,
                CurrencyCode = split[10]!,
                CurrencyName = split[11]!,
                PhonePrefix = split[12]!,
                PostCodeFormat = split[13]!,
                PostCodeRegex = split[14]!,
            };
            var languages = split[15];
            if (languages != null)
            {
                row.Languages = languages.Split(',').ToList();
            }
            var s = split[6]!;
            row.Area = double.Parse(s);

            yield return row;
        }
    }
}