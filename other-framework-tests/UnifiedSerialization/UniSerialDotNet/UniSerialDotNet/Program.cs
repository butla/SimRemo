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
            settings.Converters.Add(new JacksonConverter());

            object[] tablica = new object[] { 28, "jakis tekst", new DaneA(), new DaneB() };
            //List<object> tablica = new List<object> { 28, "jakis tekst", new DaneA(), new DaneB() };
            //Dictionary<int, string> tablica = new Dictionary<int, string> { { 1, "pierwszy" }, { 2, "drugi" }, { 3, "trzeci" } };

            string serialized = JsonConvert.SerializeObject(tablica, Formatting.Indented, settings);
            Console.WriteLine(serialized);

            // using <object> template is a trick. A converter is asked if it can deserialized, and my converter always says yes.
            object deserialized = JsonConvert.DeserializeObject<object>(serialized, settings);
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
