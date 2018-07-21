using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using StateProvinceCommunity;
using Xunit;

public class Sync
{
    [Fact]
    public async Task SyncCountryData()
    {
        var countriesPath = Path.GetFullPath(Path.Combine(DataLocations.SlnPath, "countries.txt"));
        var allCountriesZipPath = Path.Combine(DataLocations.TempPath, "allCountries.zip");
        var countryInfoPath = Path.Combine(DataLocations.TempPath, "countryInfo.txt");
        await Downloader.DownloadFile(allCountriesZipPath, "http://download.geonames.org/export/zip/allCountries.zip");
        await Downloader.DownloadFile(countryInfoPath, "http://download.geonames.org/export/dump/countryInfo.txt");

        var allCountriesTxtPath = Path.Combine(DataLocations.TempPath, "allCountries.txt");
        File.Delete(allCountriesTxtPath);
        ZipFile.ExtractToDirectory(allCountriesZipPath, DataLocations.TempPath);

        var list = PostCodeRowReader.ReadRows(allCountriesTxtPath).ToList();
        var groupByCountry = list.GroupBy(x => x.CountryCode).ToList();
        File.Delete(countriesPath);
        File.WriteAllLines(countriesPath, groupByCountry.Select(x => x.Key.ToLower()));
        WriteRows(DataLocations.PostCodesPath, groupByCountry);
    }


    void WriteRows(string jsonPath, List<IGrouping<string, PostCodeRow>> groupByCountry)
    {
        IoHelpers.PurgeDirectory(jsonPath);
        foreach (var group in groupByCountry)
        {
            ProcessCountry(@group.Key, @group.ToList(), jsonPath);
        }
    }

    void ProcessCountry(string country, List<PostCodeRow> rows, string directory)
    {
        StateProvinceCommunitySerializer.Serialize(country, rows, directory);
    }
}