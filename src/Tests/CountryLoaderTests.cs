using System.Linq;
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
    public void ReversePropsNotNull()
    {
        var country = CountryLoader.LoadAustraliaLocationData();
        foreach (var state in country.States)
        {
            Assert.NotNull(state.Country);
            foreach (var province in state.Provinces)
            {
                Assert.NotNull(province.State);
                foreach (var community in province.Communities)
                {
                    Assert.NotNull(community.Province);
                    foreach (var place in community.Places)
                    {
                        Assert.NotNull(place.Community);
                    }
                }
            }
        }
    }

    [Fact]
    public void PostCodes()
    {
        var country = CountryLoader.LoadAustraliaLocationData();
        ObjectApprover.Verify(country.PostCodes().First());
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