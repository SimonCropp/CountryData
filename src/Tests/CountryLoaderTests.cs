public class CountryLoaderTests
{
    [Test]
    public void WritePostCodeRegex()
    {
        foreach (var info in CountryLoader.CountryInfo)
        {
            if (info.PostCodeRegex != null)
            {
                Console.WriteLine(info.Name + " " + info.PostCodeRegex);
            }
        }
    }

    [Test]
    public async Task LoadLocationData()
    {
        var country = CountryLoader.LoadLocationData("PW");
        await Assert.That(() => CountryLoader.LoadLocationData("QQ")).Throws<ArgumentException>();

        await Verify(country);
    }

    [Test]
    public async Task ReversePropsNotNull()
    {
        var country = CountryLoader.LoadAustraliaLocationData();
        foreach (var state in country.States)
        {
            await Assert.That(state.Country).IsNotNull();
            foreach (var province in state.Provinces)
            {
                await Assert.That(province.State).IsNotNull();
                foreach (var community in province.Communities)
                {
                    await Assert.That(community.Province).IsNotNull();
                    foreach (var place in community.Places)
                    {
                        await Assert.That(place.Community).IsNotNull();
                    }
                }
            }
        }
    }

    [Test]
    public Task PostCodes()
    {
        var country = CountryLoader.LoadAustraliaLocationData();
        return Verify(country.PostCodes().First());
    }

    [Test]
    public Task LoadSpecificLocationData()
    {
        var country = CountryLoader.LoadPalauLocationData();
        return Verify(country);
    }

    [Test]
    public async Task LoadAll()
    {
        CountryLoader.LoadAll();
        await Assert.That(CountryLoader.LoadedLocationData).IsNotEmpty();
    }
}