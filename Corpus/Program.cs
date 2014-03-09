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
        public static Logger mLogger = new Logger();
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            while (true)
            {
                mLogger.WriteLine("Enter 1 to Generate Corporas From Text Files to a Choosen Folder");
                mLogger.WriteLine("Enter 2 to Analyze Some Sentences with Data in Corporas Files");
                mLogger.WriteLine("Enter 3 to Exit");
                mLogger.Write("Please Enter Your Chose: ");
                string inputed_command = Console.ReadLine();
                mLogger.WriteLine(inputed_command);
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
                    mLogger.WriteLine("Program Exits");
                    break;
                }
            }
            mLogger.Close();
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
                    mLogger.Write("Please input a sentence: ");
                    string input_line = Console.ReadLine();
                    mLogger.WriteLine(input_line);
                    mLogger.WriteLine("");
                    string lang = CorpAnalyzer.AnalyzeTextFromCorporasWithDemandedOutput(lst_cops, input_line);
                    mLogger.Write("It is " + lang + " continue?(y/n)");
                    string inputed_command = Console.ReadLine();
                    mLogger.WriteLine(inputed_command);
                    mLogger.WriteLine("");
                    if ("n" == inputed_command)
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
                mLogger.WriteLine(output);
                mLogger.Write("Continue?(y/n): ");
                string str = Console.ReadLine();
                mLogger.WriteLine(str);
                if ("y" == str)
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
