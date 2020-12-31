using System;
using System.Linq;
using System.Threading.Tasks;
using CountryData;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class CountryLoaderTests
{
    [Fact]
    public Task LoadLocationData()
    {
        var country = CountryLoader.LoadLocationData("PW");
        Assert.Throws<ArgumentException>(() => CountryLoader.LoadLocationData("QQ"));

        return Verifier.Verify(country);
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
    public Task PostCodes()
    {
        var country = CountryLoader.LoadAustraliaLocationData();
        return Verifier.Verify(country.PostCodes().First());
    }

    [Fact]
    public Task LoadSpecificLocationData()
    {
        var country = CountryLoader.LoadPalauLocationData();
        return Verifier.Verify(country);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }
}