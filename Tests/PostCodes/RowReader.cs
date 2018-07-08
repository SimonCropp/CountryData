using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

static class RowReader
{
    public static IEnumerable<Row> ReadRows(string allCountriesTxtPath)
    {
        var tab = Convert.ToChar(9);
        using (var csv = File.OpenText(allCountriesTxtPath))
        {
            while (true)
            {
                var line = csv.ReadLine();
                if (line == null)
                {
                    break;
                }

                var split = line.Split(tab);
                for (var index = 0; index < split.Length; index++)
                {
                    var s = split[index];
                    if (s.Length == 0)
                    {
                        split[index] = null;
                        continue;
                    }

                    split[index] = s.Trim().Trim('_').Replace('_',' ');
                }

                var row = new Row
                {
                    CountryCode = split[0],
                    PostalCode = split[1],
                    PlaceName = split[2],
                    State = split[3],
                    StateCode = split[4],
                    Province = split[5],
                    ProvinceCode = split[6],
                    Community = split[7],
                    CommunityCode = split[8],
                    Latitude = double.Parse(split[9]),
                    Longitude = double.Parse(split[10]),
                };
                Assert.NotEmpty(row.PostalCode);
                var accuracy = split[11];
                if (accuracy != null)
                {
                    row.Accuracy = ushort.Parse(accuracy);
                }

                yield return row;
            }
        }
    }
}