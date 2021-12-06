using System.Globalization;
using System.IO.Compression;
using CountryData;

public class Sync
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task SyncCountryData()
    {
        var countriesPath = Path.GetFullPath(Path.Combine(DataLocations.DataPath, "countrycodes.txt"));
        var allZipPath = Path.Combine(DataLocations.TempPath, "allCountries.zip");

        var countryInfos = await SyncCountryInfo();

        await Downloader.DownloadFile(allZipPath, "http://download.geonames.org/export/zip/allCountries.zip");

        var allCountriesTxtPath = Path.Combine(DataLocations.TempPath, "allCountries.txt");
        File.Delete(allCountriesTxtPath);
        ZipFile.ExtractToDirectory(allZipPath, DataLocations.TempPath);

        var list = PostCodeRowReader.ReadRows(allCountriesTxtPath).ToList();
        var groupByCountry = list
            .GroupBy(x => x.CountryCode)
            .OrderBy(x => x.Key)
            .ToList();
        File.Delete(countriesPath);
        await File.WriteAllLinesAsync(countriesPath, groupByCountry.Select(x => x.Key.ToLower()));
        var countryLocationData = WriteRows(DataLocations.PostCodesPath, groupByCountry);

        var postcodeZip = Path.Combine(DataLocations.DataPath, "postcodes.zip");

        var postCodesPath = DataLocations.PostCodesPath;
        Zipper.ZipDir(postcodeZip, postCodesPath);

        WriteNamedCountryCs(countryInfos, countryLocationData);
    }

    static void WriteNamedCountryCs(List<CountryInfo> countryInfos, IDictionary<string, List<State>> countryLocationData)
    {
        Dictionary<string, string> keyToName = new();
        var cultureInfo = new CultureInfo("en-US", false).TextInfo;
        foreach (var locationKey in countryLocationData.Keys)
        {
            var name = countryInfos
                .Single(x => x.Iso == locationKey)
                .Name;

            name = cultureInfo.ToTitleCase(name);
            name = name
                .Replace(" ", "")
                .Replace(".", "");
            keyToName.Add(locationKey, name);
        }

        WriteCountryCode(countryInfos);
        WriteCountryLoader(keyToName);
    }

    static void WriteCountryLoader(Dictionary<string, string> keyToName)
    {
        var namedCountryData = Path.Combine(DataLocations.CountryDataProjectPath, "CountryLoader_named.cs");
        File.Delete(namedCountryData);

        using (var writer = File.CreateText(namedCountryData))
        {
            writer.WriteLine(@"
// ReSharper disable IdentifierTypo

namespace CountryData
{
    public static partial class CountryLoader
    {");
            foreach (var locationData in keyToName)
            {
                writer.WriteLine($@"
        public static ICountry Load{locationData.Value}LocationData()
        {{
            return LoadLocationData(""{locationData.Key}"");
        }}");
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        var namedBogusData = Path.Combine(DataLocations.BogusProjectPath, "CountryDataSet_named.cs");
        File.Delete(namedBogusData);
        using (var writer = File.CreateText(namedBogusData))
        {
            writer.WriteLine(@"using Bogus;

// ReSharper disable IdentifierTypo

namespace CountryData.Bogus
{
    public partial class CountryDataSet : DataSet
    {");
            foreach (var locationData in keyToName)
            {
                writer.WriteLine($@"
        public ICountry {locationData.Value}()
        {{
            return CountryLoader.Load{locationData.Value}LocationData();
        }}");
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }
    }

    static void WriteCountryCode(List<CountryInfo> countryInfos)
    {
        var iso = Path.Combine(DataLocations.CountryDataProjectPath, "Iso.cs");
        File.Delete(iso);
        var iso3 = Path.Combine(DataLocations.CountryDataProjectPath, "Iso3.cs");
        File.Delete(iso3);
        var fips = Path.Combine(DataLocations.CountryDataProjectPath, "Fips.cs");
        File.Delete(fips);
        var currencyCode = Path.Combine(DataLocations.CountryDataProjectPath, "CurrencyCode.cs");
        File.Delete(currencyCode);
        using var isoWriter = File.CreateText(iso);
        using var iso3Writer = File.CreateText(iso3);
        using var fipsWriter = File.CreateText(fips);
        using var currencyCodeWriter = File.CreateText(currencyCode);

        isoWriter.WriteLine(@"
namespace CountryData
{
    public enum Iso
    {");
        iso3Writer.WriteLine(@"
namespace CountryData
{
    public enum Iso3
    {");
        fipsWriter.WriteLine(@"
namespace CountryData
{
    public enum Fips
    {");
        currencyCodeWriter.WriteLine(@"
namespace CountryData
{
    public enum CurrencyCode
    {");

        foreach (var code in countryInfos
            .Select(x => x.CurrencyCode)
            .Where(x => x is not null)
            .Distinct())
        {
            currencyCodeWriter.WriteLine($"        {code},");
        }

        foreach (var countryInfo in countryInfos)
        {
            isoWriter.WriteLine($"        {countryInfo.Iso},");
            iso3Writer.WriteLine($"        {countryInfo.Iso3},");

            if (countryInfo.Fips is not null)
            {
                fipsWriter.WriteLine($"        {countryInfo.Fips},");
            }
        }

        isoWriter.WriteLine("    }");
        isoWriter.WriteLine("}");
        iso3Writer.WriteLine("    }");
        iso3Writer.WriteLine("}");
        fipsWriter.WriteLine("    }");
        fipsWriter.WriteLine("}");
        currencyCodeWriter.WriteLine("    }");
        currencyCodeWriter.WriteLine("}");
    }

    static async Task<List<CountryInfo>> SyncCountryInfo()
    {
        var countryInfoPath = Path.Combine(DataLocations.TempPath, "countryInfo.txt");
        await Downloader.DownloadFile(countryInfoPath, "http://download.geonames.org/export/dump/countryInfo.txt");

        var path = Path.Combine(DataLocations.DataPath, "countryInfo.json.txt");
        var value = CountryInfoRowReader.ReadRows(countryInfoPath)
            .OrderBy(x => x.CurrencyCode)
            .ToList();
        JsonSerializer.Serialize(value, path);
        return value;
    }

    static IDictionary<string, List<State>> WriteRows(string jsonPath, List<IGrouping<string, PostCodeRow>> groupByCountry)
    {
        IoHelpers.PurgeDirectory(jsonPath);
        SortedDictionary<string, List<State>> dictionary = new();
        foreach (var group in groupByCountry)
        {
            dictionary.Add(group.Key, ProcessCountry(group.Key, group.OrderBy(x => x.CountryCode).ToList(), jsonPath));
        }

        return dictionary;
    }

    static List<State> ProcessCountry(string country, List<PostCodeRow> rows, string directory)
    {
        return CountrySerializer.Serialize(country, rows, directory);
    }
}