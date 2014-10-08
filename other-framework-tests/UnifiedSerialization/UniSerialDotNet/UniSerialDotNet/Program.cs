using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniSerialDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            object[] tablica = new object[] { 28, "jakis tekst", new DaneA(), new DaneB() };

            string serialized = JsonConvert.SerializeObject(tablica, Formatting.Indented, settings);
            Console.WriteLine(serialized);

            object deserialized = JsonConvert.DeserializeObject(serialized, settings);
            Console.WriteLine(deserialized.GetType().Name);
        }
    }

    class DaneA
    {
        public int liczbaA = 13;
        public string tekstA = "domyslny";
    }

    class DaneB : DaneA
    {
        public double liczbaB = 5.25;
    }
}
