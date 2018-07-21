using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Community;
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
        var countriesPath = Path.GetFullPath(Path.Combine(DataLocations.SlnPath, "countries.txt"));
        var allCountriesZipPath = Path.Combine(DataLocations.TempPath, "allCountries.zip");
        await Downloader.DownloadFile(allCountriesZipPath, "http://download.geonames.org/export/zip/allCountries.zip");
        var allCountriesTxtPath = Path.Combine(DataLocations.TempPath, "allCountries.txt");
        File.Delete(allCountriesTxtPath);
        ZipFile.ExtractToDirectory(allCountriesZipPath, DataLocations.TempPath);

        var list = RowReader.ReadRows(allCountriesTxtPath).ToList();
        var groupByCountry = list.GroupBy(x => x.CountryCode).ToList();
        File.Delete(countriesPath);
        File.WriteAllLines(countriesPath, groupByCountry.Select(x => x.Key.ToLower()));
        WriteRows(DataLocations.PostCodesPath, groupByCountry);
    }


    void WriteRows(string jsonPath, List<IGrouping<string, Row>> groupByCountry)
    {
        IoHelpers.PurgeDirectory(jsonPath);
        foreach (var group in groupByCountry)
        {
            ProcessCountry(@group.Key, @group.ToList(), jsonPath);
        }
    }

    void ProcessCountry(string country, List<Row> rows, string directory)
    {
        var hasState= rows.Any(_=>_.State != null);
        var hasProvince = rows.Any(_=>_.Province != null);
        var hasCommunity = rows.Any(_=>_.Community != null);

        if (hasState && hasProvince && hasCommunity)
        {
            StateProvinceCommunitySerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasState && hasProvince)
        {
            StateProvinceSerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasState && hasCommunity)
        {
            StateCommunitySerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasProvince && hasCommunity)
        {
            ProvinceCommunitySerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasState)
        {
            StateSerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasProvince)
        {
            ProvinceSerializer.Serialize(country, rows, directory);
            return;
        }
        if (hasCommunity)
        {
            CommunitySerializer.Serialize(country, rows, directory);
            return;
        }
        PlaceSerializer.Serialize(country, rows, directory);
    }
}