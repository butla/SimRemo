using ClientTests;
using JsonRpcClientDotNet;
using Newtonsoft.Json.Utilities;
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

        public ServiceCallingTest()
        {
            var toJavaDict = new Dictionary<string, string>
            {
                {"ClientTests.DaneA, ClientTests", "com.example.jsonrpc4jtest.DaneA"},
                {"ClientTests.DaneB, ClientTests", "com.example.jsonrpc4jtest.DaneB"}
            };
            JavaTypeDictionary.SetTypeDictionary(toJavaDict);            
        }

        [SetUp]
        public void Init()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            this.service = JsonRpcClientProxy<TestService>.Create("localhost", 1666);
        }

        [Test]
        public void CallWithoutParams()
        {
            DaneB returned = this.service.testB();
            Assert.IsTrue(new DaneB().Equals(returned));
        }

        [Test]
        public void CallWithObjectParam()
        {
            string testString = "jakis testowy lala";
            DaneA testArg = new DaneA() { tekstA = testString };
            Assert.AreEqual(testString, this.service.testInputA(testArg));
        }

        [Test]
        public void CallPolymorphic()
        {
            Assert.AreEqual(
                "com.example.jsonrpc4jtest.DaneA",
                this.service.testPolymorphism(new DaneA()));
            Assert.AreEqual(
                "com.example.jsonrpc4jtest.DaneB",
                this.service.testPolymorphism(new DaneB()));
        }

        [Test]
        public void CallGeneric()
        {
            Assert.AreEqual(
                "java.lang.Boolean",
                this.service.testGeneric(true));
            Assert.AreEqual(
                "java.lang.Integer",
                this.service.testGeneric(156));            
            Assert.AreEqual(
                "java.lang.Double",
                this.service.testGeneric(2.36));
            Assert.AreEqual(
                "java.lang.String",
                this.service.testGeneric("jakis string testowy"));
            Assert.AreEqual(
                "com.example.jsonrpc4jtest.DaneA",
                this.service.testGeneric(new DaneA()));
            Assert.AreEqual(
                "com.example.jsonrpc4jtest.DaneB",
                this.service.testGeneric(new DaneB()));
        }

        [Test]
        public void CallWithStringParam()
        {
            string testString = "jakis testowy lala";
            Assert.AreEqual(
                testString + " działa!",
                this.service.testString(testString));
        }

        [Test]
        public void CallOverloads()
        {
            int testNum = 654;
            Assert.AreEqual("działa!", this.service.test());
            Assert.AreEqual("działa! a teraz cyferki: " + testNum.ToString(), this.service.test(testNum));
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
