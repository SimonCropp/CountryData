using System.IO;
using Newtonsoft.Json;

static class JsonSerializer
{
    static Newtonsoft.Json.JsonSerializer jsonSerializer;

    static JsonSerializer()
    {
        jsonSerializer = new Newtonsoft.Json.JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };
    }

    public static void Serialize(object value, string path)
    {
        using var fileStream = File.OpenWrite(path);
        using var textWriter = new StreamWriter(fileStream);
        using var jsonTextWriter = new JsonTextWriter(textWriter) {Indentation = 0};
        jsonSerializer.Serialize(jsonTextWriter, value);
    }
}