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
                    // TODO wrap serializedResult string depending on their type, whether it's integer/double, string, object or array
                    var resultReader = new JsonTextReader(new StringReader("\"" + serializedResult.ToString() + "\""));
                    object result = this.serializer.Deserialize(resultReader, method.ReturnType);
                    return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
                }
                else if (response.TryGetValue("exception", out serializedResult))
                {
                    throw new Exception("Some JsonRpcException");
                }
                else
                {
                    throw new Exception("Received response doesn't have either result or error, something is wrong.");  // TODO what about void methods?
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
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
            JObject call = new JObject();
            Interlocked.Increment(ref nextId);
            
            call["id"] = nextId;
            call["jsonrpc"] = "2.0";
            call["method"] = methodName;
            call["params"] = new JArray(parameters);
            return call;
        }
    }
}
