using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientTests
{
    public interface TestService
    {
        string test();

        string testString(string arg);

        DaneA testA();

        DaneB testB();

        int[] testArray();

        int testInt();

        double testDouble();

        void testVoid();

        void testException();

        void testUndefinedMethod();

        string testInputA(DaneA arg);
    }

    public class DaneA : IEquatable<DaneA>
    {
        public int numberA = 13;
        public string stringA = "domyslny";

        public bool Equals(DaneA other)
        {
            return this.numberA == other.numberA && this.stringA.Equals(other.stringA);
        }
    }

    public class DaneB : DaneA, IEquatable<DaneB>
    {
        public double numberB = 5.25;

        public bool Equals(DaneB other)
        {
            return this.numberB == other.numberB && base.Equals(other);
        }
    }
}
