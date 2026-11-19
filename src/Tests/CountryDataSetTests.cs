public class CountryDataSetTests
{
    [Test]
    public async Task Simple()
    {
        var name = new CountryDataSet().Name();
        await Assert.That(name).IsNotNull();
    }

    [Test]
    public async Task SpecificCountry()
    {
        var state = CountryDataSet.Australia().State();
        await Assert.That(state).IsNotNull();
    }
}