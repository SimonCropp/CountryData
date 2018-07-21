using CountryData;
using ObjectApproval;
using Xunit;

public class CountryLoaderTests
{
    [Fact]
    public void Run()
    {
        var states = CountryLoader.LoadLocationData("PW");
        ObjectApprover.VerifyWithJson(states);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }
}