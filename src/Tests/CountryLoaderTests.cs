using CountryData;
using Xunit;
using Xunit.Abstractions;

public class CountryLoaderTests :
    XunitLoggingBase
{
    [Fact]
    public void LoadLocationData()
    {
        var country = CountryLoader.LoadLocationData("PW");
        ObjectApprover.Verify(country);
    }

    [Fact]
    public void LoadSpecificLocationData()
    {
        var country = CountryLoader.LoadPalauLocationData();
        ObjectApprover.Verify(country);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }

    public CountryLoaderTests(ITestOutputHelper output) :
        base(output)
    {
    }
}