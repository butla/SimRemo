using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using JsonRpcClientDotNet;
using System.Globalization;
using System.Threading;

namespace ClientTests
{
    [TestFixture]
    public class ResultDeserializerTest
    {
        public ResultDeserializerTest()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void TypeTest<T>(T value, string valueSerialized)
        {
            T toSerialization = value;
            JToken testString = JValue.Parse(valueSerialized);

            object deserialized = ResultDeserializer.Deserialize(testString, typeof(T));

            Assert.IsInstanceOf(typeof(T), deserialized);
            Assert.AreEqual(toSerialization, deserialized);
        }

        [Test]
        public void TestString()
        {
            string toSerialization = "jakis string";
            TypeTest<string>(toSerialization, "\"" + toSerialization + "\"");
        }

        [Test]
        public void TestObject()
        {
            TypeTest<DaneA>(new DaneA(), @"{""stringA"": ""domyslny"", ""numberA"":13}");
        }

        [Test]
        public void TestInteger()
        {
            TypeTest<int>(13, "13");
        }

        [Test]
        public void TestDouble()
        {
            TypeTest<double>(5.25, "5.25");
        }

        [Test]
        public void TestArray()
        {
            TypeTest<int[]>(new int[] { 1, 2, 3 }, "[1,2,3]");
        }

        [Test]
        public void TestNull()
        {
            JToken testNull = JValue.Parse("null");
            object deserialized = ResultDeserializer.Deserialize(testNull, typeof(void));
            Assert.IsNull(deserialized);
        }
    }
}
