using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;

namespace JsonRpcClientDotNet
{
    class Program
    {
        private static long id = 1; // we can't use zero

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            TestService serv = JsonRpcClientProxy<TestService>.Create("localhost", 1666);
            serv.testVoid();
            
        }
    }
}
