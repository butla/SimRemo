using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonRpcClientDotNet
{
    public class JsonRpcException : Exception
    {
        public JsonRpcException()
        {
        }

        public JsonRpcException(string message) : base(message)
        {
        }
    }
}
