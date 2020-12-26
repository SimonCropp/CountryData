using CountryData.Bogus;
using Xunit;

public class CountryDataSetTests
{
    [Fact]
    public void Simple()
    {
        var name = new CountryDataSet().Name();
        Assert.NotNull(name);
    }

    [Fact]
    public void SpecificCountry()
    {
        CountryDataSet dataSet = new();
        var state = dataSet.Australia().State();
        Assert.NotNull(state);
    }
}