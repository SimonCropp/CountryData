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

    public static void Serialize(string directory,string country, object value)
    {
        using (var fileStream = File.OpenWrite(Path.Combine(directory, country+".json.txt")))
        using (var textWriter = new StreamWriter(fileStream))
        using (var jsonTextWriter = new JsonTextWriter(textWriter))
        {
            jsonTextWriter.IndentChar = ' ';
            jsonTextWriter.Indentation = 1;
            jsonSerializer.Serialize(jsonTextWriter, value);
        }
    }
}