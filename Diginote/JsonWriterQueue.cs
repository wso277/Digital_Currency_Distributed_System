using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class QueueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsGenericType)
            {
                return false;
            }

            return objectType.GetGenericTypeDefinition() == typeof(ConcurrentQueue<>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objType = objectType.GetGenericArguments()[0];
            var listType = typeof(List<>).MakeGenericType(objType);
            var list = serializer.Deserialize(reader, listType);
            var bagType = typeof(ConcurrentQueue<>).MakeGenericType(objType);
            var instance = Activator.CreateInstance(bagType, list);
            return instance;
        }

    }
}
