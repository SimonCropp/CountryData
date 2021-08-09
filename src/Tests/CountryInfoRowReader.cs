using System;
using System.Collections.Generic;
using System.Linq;
using CountryData;

static class CountryInfoRowReader
{
    public static IEnumerable<CountryInfo> ReadRows(string path)
    {
        foreach (var split in RowReader.ReadRows(path))
        {
            CountryInfo row = new()
            {
                Iso = split[0]!,
                Iso3 = split[1]!,
                IsoNumeric = ushort.Parse(split[2]!),
                Name = split[4]!,
                Capital = split[5]!,
                //Population = uint.Parse(split[7]),
                Continent = split[8]!,
                TopLevelDomain = split[9]!,
                CurrencyName = split[11]!,
                PhonePrefix = split[12]!,
                PostCodeFormat = split[13]!,
                PostCodeRegex = split[14]!,
            };

            var currencyCode = split[10];
            if (!string.IsNullOrWhiteSpace(currencyCode))
            {
                row.CurrencyCode = Enum.Parse<CurrencyCode>(currencyCode);
            }

            var fips = split[3];
            if (!string.IsNullOrWhiteSpace(fips))
            {
                row.Fips = Enum.Parse<Fips>(fips);
            }

            var languages = split[15];
            if (languages is not null)
            {
                row.Languages = languages.Split(',').ToList();
            }

            var s = split[6]!;
            row.Area = double.Parse(s);

            yield return row;
        }
    }
}