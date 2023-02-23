using CountryData.Bogus;

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
        var dataSet = new CountryDataSet();
        var state = dataSet.Australia().State();
        Assert.NotNull(state);
    }
}