using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonRpcClientDotNet
{
    public static class ResultDeserializer
    {
        private static JsonSerializer serializer = new JsonSerializer();

        public static object Deserialize(JToken serializedResult, Type resultType)  // or JsonToken?
        {
            var wrappedResult = new StringBuilder(serializedResult.ToString());
            switch(serializedResult.Type)
            {
                case JTokenType.String:
                    wrappedResult.Insert(0, "\"");
                    wrappedResult.Append("\"");
                    break;
                case JTokenType.Null:
                    return null;
                default:
                    break;
            }            

            var resultReader = new JsonTextReader(new StringReader(wrappedResult.ToString()));
            return ResultDeserializer.serializer.Deserialize(resultReader, resultType);
        }        
    }
}
