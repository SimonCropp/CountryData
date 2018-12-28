using CountryData;
using ObjectApproval;
using Xunit;

public class CountryLoaderTests
{
    [Fact]
    public void LoadLocationData()
    {
        var country = CountryLoader.LoadLocationData("PW");
        ObjectApprover.VerifyWithJson(country);
    }

    [Fact]
    public void LoadSpecificLocationData()
    {
        var country = CountryLoader.LoadPalauLocationData();
        ObjectApprover.VerifyWithJson(country);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }
}