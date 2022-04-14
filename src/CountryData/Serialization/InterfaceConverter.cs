using System.Text.Json;
using System.Text.Json.Serialization;

class InterfaceConverter<TClass, TInterface> :
    JsonConverter<TInterface>
    where TClass:TInterface
{
    public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<TClass>(ref reader, options)!;

    public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}