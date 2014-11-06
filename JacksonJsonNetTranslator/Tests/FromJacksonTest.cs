using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class FromJacksonTest
    {
        [Test]
        public void TestObject()
        {
            //JToken testString = JValue.Parse(valueSerialized);
            //{"id":"4653019993893876656","jsonrpc":"2.0","method":"testDataA","params":[{"@class":"jsonrpc4jtestse.TestDataB","numberA":13,"stringA":"domyslny","numberB":5.25}]}
            //{"jsonrpc":"2.0","id":"4653019993893876656","result":{"@class":"jsonrpc4jtestse.TestDataB","numberA":14,"stringA":"domyslny dodałem coś!","numberB":5.25}}
        }
    }
}
