using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientTests
{
    [TestFixture]
    public class DataClassesTest
    {
        [Test]
        public void DaneAEquality()
        {
            DaneA a, b;
            a = new DaneA();
            b = new DaneA();
            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        public void DaneAInequality()
        {
            DaneA a, b;
            a = new DaneA();
            b = new DaneA();
            b.numberA = 131313;
            Assert.IsFalse(a.Equals(b));
        }

        [Test]
        public void DaneBEquality()
        {
            var a = new DaneA();
            var b = new DaneA();
            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        public void DaneBInequality()
        {
            var a = new DaneB();
            var b = new DaneB();
            b.numberB = 8.8888;
            Assert.IsFalse(a.Equals(b));
        }
    }
}
