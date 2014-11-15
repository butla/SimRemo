using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading;

namespace JsonRpcClientDotNet
{
    // based on http://stackoverflow.com/questions/15733900/dynamically-creating-a-proxy-class
    // TODO is it neccesary to point out Creative Commons
    public class JsonRpcClientProxy<T> : RealProxy
    {
        private const string ResultProperty = "result";
        private const string ErrorProperty = "error";

        private static long nextId; // starting with zero, but will be incremented before first use

        private string endpointAddress;
        private int port;

        private JsonSerializer serializer = new JsonSerializer();

        private JsonRpcClientProxy(string address, int port)
            : base(typeof(T))
        {
            this.endpointAddress = address;
            this.port = port;
        }

        public static T Create(string address, int port)
        {
            return (T)new JsonRpcClientProxy<T>(address, port).GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;

            try
            {
                JObject response = this.MakeCall(methodCall.MethodName, methodCall.InArgs);

                JToken serializedResult;
                if(response.TryGetValue(ResultProperty, out serializedResult))
                {
                    object result = ResultDeserializer.Deserialize(serializedResult, method.ReturnType);
                    return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
                }
                else if (response.TryGetValue(ErrorProperty, out serializedResult))
                {
                    throw new JsonRpcException(serializedResult.ToString());
                }
                else
                {
                    throw new Exception("Received response doesn't have either result or error, something is wrong.");
                }
            }
            catch (Exception e)
            {
                if (e is TargetInvocationException && e.InnerException != null)
                {
                    return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
                }

                return new ReturnMessage(e, msg as IMethodCallMessage);
            }
        }
                

        private JObject MakeCall(string methodName, object[] parameters)
        {
            using (Socket socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp))
            {
                socket.Connect(this.endpointAddress, this.port);

                var stream = new NetworkStream(socket);
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                var jsonReader = new JsonTextReader(reader);

                writer.Write(PrepareCall(methodName, parameters));
                writer.Flush();

                return this.serializer.Deserialize<JObject>(jsonReader);
            }
        }

        private JObject PrepareCall(string methodName, object[] parameters)
        {
            //TODO moze po prostu tworzyć cały obiekt Call i go serializować?

            JObject call = new JObject();
            Interlocked.Increment(ref nextId);
            
            call["id"] = nextId;
            call["jsonrpc"] = "2.0";
            call["method"] = methodName;
            call["params"] = PrepareAttributes(parameters);
            return call;
        }

        private JArray PrepareAttributes(object[] parameters)
        {
            var serializationSettings = new JsonSerializerSettings();
            serializationSettings.TypeNameHandling = TypeNameHandling.All;

            JArray paramsJson = new JArray();
            
            foreach(object parameter in parameters)
            {
                paramsJson.Add(JToken.Parse(JsonConvert.SerializeObject(parameter, serializationSettings)));          
            }

            return paramsJson;
        }
    }
}
