using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Community;
using Newtonsoft.Json;
using Place;
using Province;
using ProvinceCommunity;
using State;
using StateCommunity;
using StateProvince;
using StateProvinceCommunity;
using Xunit;

public class Sync
{
    [Fact]
    public async Task SyncCountryData()
    {
        var tempPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../../temp"));
        var jsonIndentedPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../../json_indented"));
        var jsonPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../../json"));
        var countriesPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../../countries.txt"));
        var allCountriesZipPath = Path.Combine(tempPath, "allCountries.zip");
        await Downloader.DownloadFile(allCountriesZipPath);
        var allCountriesTxtPath = Path.Combine(tempPath, "allCountries.txt");
        File.Delete(allCountriesTxtPath);
        ZipFile.ExtractToDirectory(allCountriesZipPath, tempPath);

        var list = ReadRows(allCountriesTxtPath).ToList();
        var jsonSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
        var groupByCountry = list.GroupBy(x => x.CountryCode).ToList();
        File.Delete(countriesPath);
        File.WriteAllLines(countriesPath, groupByCountry.Select(x => x.Key.ToLower()));
        WriteRows(jsonPath, jsonSerializer, groupByCountry);

        jsonSerializer.Formatting = Formatting.Indented;
        WriteRows(jsonIndentedPath, jsonSerializer, groupByCountry);
    }

    void WriteRows(string jsonPath, JsonSerializer jsonSerializer, List<IGrouping<string, Row>> groupByCountry)
    {
        IoHelpers.PurgeDirectory(jsonPath);
        foreach (var group in groupByCountry)
        {
            var countryJsonFilePath = Path.Combine(jsonPath, @group.Key.ToLower() + ".json.txt");
            using (var fileStream = File.OpenWrite(countryJsonFilePath))
            using (var textWriter = new StreamWriter(fileStream))
            using (var jsonTextWriter = new JsonTextWriter(textWriter))
            {
                jsonTextWriter.IndentChar = ' ';
                jsonTextWriter.Indentation = 1;
                ProcessCountry(@group.ToList(), o => { jsonSerializer.Serialize(jsonTextWriter, o); });
            }
        }
    }

    void ProcessCountry(List<Row> rows, Action<object> action)
    {
        var hasState= rows.Any(_=>_.State != null);
        var hasProvince = rows.Any(_=>_.Province != null);
        var hasCommunity = rows.Any(_=>_.Community != null);

        if (hasState && hasProvince && hasCommunity)
        {
            StateProvinceCommunitySerializer.Serialize(rows, action);
            return;
        }
        if (hasState && hasProvince)
        {
            StateProvinceSerializer.Serialize(rows, action);
            return;
        }
        if (hasState && hasCommunity)
        {
            StateCommunitySerializer.Serialize(rows, action);
            return;
        }
        if (hasProvince && hasCommunity)
        {
            ProvinceCommunitySerializer.Serialize(rows, action);
            return;
        }
        if (hasState)
        {
            StateSerializer.Serialize(rows, action);
            return;
        }
        if (hasProvince)
        {
            ProvinceSerializer.Serialize(rows, action);
            return;
        }
        if (hasCommunity)
        {
            CommunitySerializer.Serialize(rows, action);
            return;
        }
        PlaceSerializer.Serialize(rows, action);
    }

    static IEnumerable<Row> ReadRows(string allCountriesTxtPath)
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

                    split[index] = s.Trim();
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