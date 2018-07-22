using System.IO;
using System.Runtime.Serialization.Json;

static class Serializer
{
    public static T Deserialize<T>(Stream stream)
    {
        var jsonSerializer = new DataContractJsonSerializer(typeof(T));
        return (T) jsonSerializer.ReadObject(stream);
    }
}