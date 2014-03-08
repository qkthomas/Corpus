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

            #region Serializer Test
            Corpora cops = new Corpora();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, cops); 
            #endregion

            #region regex test
            /*
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
             * */
            #endregion
        }
    }
}
