using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPackage.Net
{
    internal class SortedStringMapWriterConverter<T> : JsonConverter
    {
        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, T>);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, value);
                return;
            }
            SortedDictionary<string, T> ordered = new((Dictionary<string, T>)value);
            serializer.Serialize(writer, ordered);
        }
    }
}
