using Newtonsoft.Json;
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

        public static object Deserialize(JsonToken serializedResult, Type resultType)
        {
            var wrappedResult = new StringBuilder(serializedResult.ToString());
            // TODO wrapping logic
            wrappedResult.Insert(0, "\"");
            wrappedResult.Append("\"");

            var resultReader = new JsonTextReader(new StringReader(wrappedResult.ToString()));
            return ResultDeserializer.serializer.Deserialize(resultReader, resultType);
        }
        
    }
}
