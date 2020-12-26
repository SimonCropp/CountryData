using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CountryData;

static class Serializer
{
    public static T Deserialize<T>(Stream stream)
    {
        JsonSerializerOptions options = new();
        options.Converters.Add(new InterfaceConverter<Location, ILocation>());
        options.Converters.Add(new InterfaceConverter<Community, ICommunity>());
        options.Converters.Add(new InterfaceConverter<Country, ICountry>());
        options.Converters.Add(new InterfaceConverter<CountryInfo, ICountryInfo>());
        options.Converters.Add(new InterfaceConverter<Place, IPlace>());
        options.Converters.Add(new InterfaceConverter<Province, IProvince>());
        options.Converters.Add(new InterfaceConverter<State, IState>());
        options.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<T>(ReadToEnd(stream), options)!;
    }

    static string ReadToEnd(Stream stream)
    {
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}