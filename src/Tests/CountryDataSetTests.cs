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
        var state = CountryDataSet.Australia().State();
        Assert.NotNull(state);
    }
}