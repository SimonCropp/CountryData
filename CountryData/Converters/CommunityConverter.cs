using System;
using CountryData;
using Newtonsoft.Json;

class CommunityConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(ICommunity);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize(reader, typeof(Community));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
    }
}