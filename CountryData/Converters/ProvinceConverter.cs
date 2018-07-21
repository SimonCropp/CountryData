using System;
using CountryData;
using Newtonsoft.Json;

class ProvinceConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(IProvince);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize(reader, typeof(Province));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
    }
}