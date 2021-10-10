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
        using StreamWriter textWriter = new(fileStream);
        using JsonTextWriter jsonTextWriter = new(textWriter)
        {
            Indentation = 0
        };
        jsonSerializer.Serialize(jsonTextWriter, value);
    }
}