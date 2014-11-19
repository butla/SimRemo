using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientTests
{
    public interface TestService
    {
        string test();

        string test(int arg);

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

        string testPolymorphism(DaneA arg);

        string testGeneric(object arg);
    }

    public class DaneA : IEquatable<DaneA>
    {
        public int liczbaA = 13;
        public string tekstA = "domyslny";

        public bool Equals(DaneA other)
        {
            return this.liczbaA == other.liczbaA && this.tekstA.Equals(other.tekstA);
        }
    }

    public class DaneB : DaneA, IEquatable<DaneB>
    {
        public double liczbaB = 5.25;

        public bool Equals(DaneB other)
        {
            return this.liczbaB == other.liczbaB && base.Equals(other);
        }
    }
}
