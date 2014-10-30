using ClientTests;
using JsonRpcClientDotNet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientFunctionalTests
{
    [TestFixture]
    public class ServiceCallingTest
    {
        private TestService service;

        [SetUp]
        public void Init()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            this.service = JsonRpcClientProxy<TestService>.Create("localhost", 1666);
        }

        [Test]
        public void Call()
        {
            DaneB returned = this.service.testB();
            Assert.IsTrue(new DaneB().Equals(returned));
        }

        [Test]
        [ExpectedException(typeof(JsonRpcException))]
        public void ServerSideException()
        {
            this.service.testException();
        }

        [Test]
        [ExpectedException(typeof(JsonRpcException))]
        public void MethodUnknownException()
        {
            this.service.testUndefinedMethod();
        }

        [Test]
        [ExpectedException(typeof(SocketException))]
        public void WrongPort()
        {
            this.service = JsonRpcClientProxy<TestService>.Create("localhost", 1333);
            this.service.test();
        }

        [Test]
        [ExpectedException(typeof(SocketException))]
        public void WrongAddress()
        {
            this.service = JsonRpcClientProxy<TestService>.Create("nosuchhost", 1666);
            this.service.test();
        }
    }
}
