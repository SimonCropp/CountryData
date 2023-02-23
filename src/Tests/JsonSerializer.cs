using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

static class JsonSerializer
{
    static Newtonsoft.Json.JsonSerializer jsonSerializer;

    static JsonSerializer()
    {
        jsonSerializer = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };
        jsonSerializer.Converters.Add(new StringEnumConverter());
    }

    public static void Serialize(object value, string path)
    {
        using var fileStream = File.OpenWrite(path);
        using var textWriter = new StreamWriter(fileStream);
        using var jsonTextWriter = new JsonTextWriter(textWriter)
        {
            Indentation = 0
        };
        jsonSerializer.Serialize(jsonTextWriter, value);
    }
}