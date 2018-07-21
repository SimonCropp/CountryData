using System;
using Newtonsoft.Json;

namespace CountryData
{
    public class StateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(IState);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, typeof(State));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}