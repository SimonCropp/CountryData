﻿static class Serializer
{
    static JsonSerializerOptions options;
    static Serializer()
    {
        options = new();
        options.Converters.Add(new InterfaceConverter<Location, ILocation>());
        options.Converters.Add(new InterfaceConverter<Community, ICommunity>());
        options.Converters.Add(new InterfaceConverter<Country, ICountry>());
        options.Converters.Add(new InterfaceConverter<CountryInfo, ICountryInfo>());
        options.Converters.Add(new InterfaceConverter<Place, IPlace>());
        options.Converters.Add(new InterfaceConverter<Province, IProvince>());
        options.Converters.Add(new InterfaceConverter<State, IState>());
        options.Converters.Add(new JsonStringEnumConverter());
    }

    public static T Deserialize<T>(Stream stream) =>
        JsonSerializer.Deserialize<T>(ReadToEnd(stream), options)!;

    static string ReadToEnd(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}