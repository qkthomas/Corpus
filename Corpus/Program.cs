using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Corpus
{
    class Program
    {
        static void Main(string[] args)
        {
            CorporaIrrigationTest();
        }

        static void SerializerTest()
        {
            Corpora cops = new Corpora();
            cops.Init();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, cops);
            writer.Close();
        }

        static void SerializerTest(Corpora cops)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, cops);
            writer.Close();
        }

        static void DeserializerTest()
        {
            Corpora cops = new Corpora();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextReader reader = new StreamReader(filename);
            cops = ser.Deserialize(reader) as Corpora;
            reader.Close();
            int bp = 0;
        }

        static void RegexTest()
        {
            string pattern = @"[^a-z]";
            Regex defaultRegex = new Regex(pattern);
            bool goon = true;
            while (goon)
            {
                Console.Write("Type something: ");
                string input = Console.ReadLine();
                string output = "";
                string replacement = "";
                try
                {
                    output = defaultRegex.Replace(input, replacement);
                }
                catch (Exception)
                {

                    throw;
                }
                Console.WriteLine(output);
                Console.Write("Continue?(y/n): ");
                string c = Console.ReadLine();
                if ("y" == c)
                {
                    goon = true;
                }
                else
                {
                    goon = false;
                }
            }
        }

        static void CorporaIrrigationTest()
        {
            string filepath = @"E:\Dropbox\Codes Hub\C#\Corpus\text samples\english.txt";
            Corpora cops = CorpAnalyzer.ExtractCorporaFromFile(filepath);
            SerializerTest(cops);
        }
    }
}
