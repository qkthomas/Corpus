using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;


namespace XmlSerializer_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Corpora cops = new Corpora();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, cops);
        }
    }
}
