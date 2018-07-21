using System;
using Newtonsoft.Json;

namespace CountryData
{
    public class PlaceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(IPlace);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, typeof(Place));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}