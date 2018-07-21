using System.IO;
using Newtonsoft.Json;

static class Serializer
{
    static JsonSerializer serializer;

    static Serializer()
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        settings.Converters.Add(new StateConverter());
        settings.Converters.Add(new PlaceConverter());
        settings.Converters.Add(new ProvinceConverter());
        settings.Converters.Add(new CommunityConverter());
        serializer = JsonSerializer.Create(settings);
    }

    public static T Deserialize<T>(Stream stream)
    {
        using (var streamReader = new StreamReader(stream))
        using (var jsonTextReader = new JsonTextReader(streamReader))
        {
            return serializer.Deserialize<T>(jsonTextReader);
        }
    }
}