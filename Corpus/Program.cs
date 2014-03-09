using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Corpus
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter 1 to Generate Corporas From Text Files to a Choosen Folder");
                Console.WriteLine("Enter 2 to Analyze Some Sentences From Corporas Files");
                Console.WriteLine("Enter 3 to Exit");
                Console.Write("Please Enter Your Chose: ");
                string inputed_command = Console.ReadLine();
                if ("1" == inputed_command)
                {
                    FuncBuildCorporas();
                }
                else if ("2" == inputed_command)
                {
                    FuncAnalyzeSentence();
                }
                else if ("3" == inputed_command)
                {
                    break;
                }
            }
        }

        static void FuncAnalyzeSentence()
        {
            OpenFileDialog openCorporasFileDialog = new OpenFileDialog();
            openCorporasFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openCorporasFileDialog.RestoreDirectory = true;
            openCorporasFileDialog.Multiselect = true;
            openCorporasFileDialog.Title = "Please Select Corpora File(s)";
            List<string> lst_filepaths = new List<string>();
            if(openCorporasFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string str_path in openCorporasFileDialog.FileNames)
                {
                    lst_filepaths.Add(str_path);
                }
                List<Corpora> lst_cops = FuncDeserializeCorporaFiles(lst_filepaths);
                while (true)
                {
                    Console.Write("Please input a sentence: ");
                    string input_line = Console.ReadLine();
                    string lang = CorpAnalyzer.AnalyzeTextFromCorporas(lst_cops, input_line);
                    Console.Write("It is " + lang + " continue?(y/n)");
                    if ("n" == Console.ReadLine())
                    {
                        break;
                    }
                }
            }
        }

        static List<Corpora> FuncDeserializeCorporaFiles(List<string> lst_filepaths)
        {
            List<Corpora> lst_cops = new List<Corpora>();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            foreach (string filename in lst_filepaths)
            {
                TextReader reader = new StreamReader(filename);
                Corpora cops = new Corpora();
                cops = ser.Deserialize(reader) as Corpora;
                lst_cops.Add(cops);
                reader.Close();
            }
            return lst_cops;
        }

        static void FuncBuildCorporas()
        {
            OpenFileDialog openTextFileDialog = new OpenFileDialog();
            openTextFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openTextFileDialog.RestoreDirectory = true;
            openTextFileDialog.Multiselect = true;
            openTextFileDialog.Title = "Please Select Text File(s)";
            List<Corpora> lst_cops = new List<Corpora>();
            if (openTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string str_path in openTextFileDialog.FileNames)
                {
                    Corpora cops = CorpAnalyzer.ExtractCorporaFromFile(str_path);
                    lst_cops.Add(cops);
                }
                FuncSerializeCorporaFiles(lst_cops);
            }
        }

        static void FuncSerializeCorporaFiles(List<Corpora> lst_cops)
        {
            FolderBrowserDialog browseFolderToSaveCorporas = new FolderBrowserDialog();
            if (browseFolderToSaveCorporas.ShowDialog() == DialogResult.OK)
            {
                string folder_path = browseFolderToSaveCorporas.SelectedPath + @"\";
                foreach (Corpora cops in lst_cops)
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Corpora));
                    string filename = cops.Language + ".xml";
                    TextWriter writer = new StreamWriter(folder_path + filename);
                    ser.Serialize(writer, cops);
                    writer.Close();
                } 
            }
        }

        #region Test Codes
        static void SerializerTest()
        {
            string language = "test";
            Corpora cops = new Corpora();
            cops.Init(language);
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = "data.xml";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, cops);
            writer.Close();
        }

        static void SerializerTest(Corpora cops)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string filename = cops.Language + ".xml";
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
        }

        static List<Corpora> DeserializerTest(int any_number)
        {
            List<Corpora> lst_cops = new List<Corpora>();
            XmlSerializer ser = new XmlSerializer(typeof(Corpora));
            string[] arr_filenames = { @"english.xml", @"french.xml", @"italian.xml" };
            foreach (string filename in arr_filenames)
            {
                TextReader reader = new StreamReader(filename);
                Corpora cops = new Corpora();
                cops = ser.Deserialize(reader) as Corpora;
                lst_cops.Add(cops);
                reader.Close();
            }
            return lst_cops;
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
            string[] arr_filenames = { @"E:\Dropbox\Codes Hub\C#\Corpus\text samples\english.txt", @"E:\Dropbox\Codes Hub\C#\Corpus\text samples\french.txt", @"E:\Dropbox\Codes Hub\C#\Corpus\text samples\italian.txt" };

            foreach (string filename in arr_filenames)
            {
                Corpora cops = CorpAnalyzer.ExtractCorporaFromFile(filename);
                SerializerTest(cops);
            }
        } 
        #endregion
    }
}
