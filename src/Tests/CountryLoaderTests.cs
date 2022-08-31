using CountryData;
using Xunit.Abstractions;

[UsesVerify]
public class CountryLoaderTests
{
    ITestOutputHelper testOutputHelper;

    public CountryLoaderTests(ITestOutputHelper testOutputHelper) =>
        this.testOutputHelper = testOutputHelper;

    [Fact]
    public void WritePostCodeRegex()
    {
        foreach (var info in CountryLoader.CountryInfo)
        {
            if (info.PostCodeRegex != null)
            {
                testOutputHelper.WriteLine(info.Name + " " + info.PostCodeRegex);
            }
        }
    }

    [Fact]
    public Task LoadLocationData()
    {
        var country = CountryLoader.LoadLocationData("PW");
        Assert.Throws<ArgumentException>(() => CountryLoader.LoadLocationData("QQ"));

        return Verify(country);
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
        return Verify(country.PostCodes().First());
    }

    [Fact]
    public Task LoadSpecificLocationData()
    {
        var country = CountryLoader.LoadPalauLocationData();
        return Verify(country);
    }

    [Fact]
    public void LoadAll()
    {
        CountryLoader.LoadAll();
        Assert.NotEmpty(CountryLoader.LoadedLocationData);
    }
}