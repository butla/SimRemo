using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace JsonRpcClientDotNet
{
    class Program
    {
        private static long id = 1; // we can't use zero

        static void Main(string[] args)
        {
            TestService serv = JsonRpcClientProxy<TestService>.Create("localhost", 1666);
            string answer = serv.testString("parametryk");
            Console.WriteLine(answer);

            //JObject call = new JObject();
            //call["id"] = Program.id;
            //id++;
            //call["jsonrpc"] = "2.0";
            //call["method"] = "testString";
            //call["params"] = new JArray("wejscie");
            
            //Console.WriteLine(call.ToString());

            //using (Socket socket = new Socket(
            //        AddressFamily.InterNetwork,
            //        SocketType.Stream,
            //        ProtocolType.Tcp))
            //{
            //    socket.Connect("localhost", 1666);

            //    var stream = new NetworkStream(socket);
            //    StreamWriter writer = new StreamWriter(stream);
            //    StreamReader reader = new StreamReader(stream);
            //    var jsonReader = new JsonTextReader(reader);

            //    writer.Write(call.ToString());
            //    writer.Flush();

            //    JsonSerializer serializer = new JsonSerializer();
            //    JObject returned = serializer.Deserialize<JObject>(jsonReader);

            //    // TODO get error or value
            //    Console.WriteLine(returned.ToString());
            //}
        }
    }
}
