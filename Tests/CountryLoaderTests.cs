using CountryData;
using ObjectApproval;
using Xunit;

public class CountryLoaderTests
{
    [Fact]
    public void Run()
    {
        var states = CountryLoader.Load("PW");
        ObjectApprover.VerifyWithJson(states);
    }
}