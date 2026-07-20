using Argon;

static class JsonSerializer
{
    static Argon.JsonSerializer jsonSerializer;

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
        File.Delete(path);
        using var fileStream = File.OpenWrite(path);
        using var textWriter = new StreamWriter(fileStream);
        using var jsonTextWriter = new JsonTextWriter(textWriter)
        {
            Formatting = Formatting.Indented
        };
        jsonSerializer.Serialize(jsonTextWriter, value);
    }
}