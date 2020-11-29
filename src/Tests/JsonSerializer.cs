using System.IO;
using Newtonsoft.Json;

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