using CountryData;
using ObjectApproval;
using Xunit;

public class CountryLoaderTests
{
    [Fact]
    public void LoadLocationData()
    {
        var states = CountryLoader.LoadLocationData("PW");
        ObjectApprover.VerifyWithJson(states);
    }

    [Fact]
    public void LoadSpecificLocationData()
    {
        var states = CountryLoader.LoadPalauLocationData();
        ObjectApprover.VerifyWithJson(states);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }
}