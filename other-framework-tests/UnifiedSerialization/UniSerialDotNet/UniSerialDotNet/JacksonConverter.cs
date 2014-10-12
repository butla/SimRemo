using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace UniSerialDotNet
{
    class JacksonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (typeof(IEnumerable).IsAssignableFrom(objectType) &&
                !(typeof(string).IsAssignableFrom(objectType)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // [ to czytasz typ i jedziesz dalej 
            // { to normalnie

            reader.Skip();
            return 42;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {            
            if(value is IEnumerable)
            {
                writer.WriteStartArray();
                writer.WriteValue("jakiś typ blabla");
                writer.WriteStartArray();
                foreach(object listElement in (IEnumerable)value)
                {
                    // defaultowy serializer używaj
                    serializer.Serialize(writer, listElement);
                }
                writer.WriteEndArray();
                writer.WriteEndArray();
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
