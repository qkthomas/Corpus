using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Corpus
{
    class CorpAnalyzer
    {
        public static Corpora ExtractCorporaFromFile(string filename)
        {
            Corpora cops = new Corpora();
            cops.Init();
            StreamReader file = new StreamReader(filename);
            string line;
            while((line = file.ReadLine()) != null)
            {
                string str_in_process = TraimNoneAlphabetChars(line);
                if (2 <= str_in_process.Length)
                {
                    List<Tuple<char, char>> bigrams = BuildBigramsFromString(str_in_process);
                    IrrigateCorporaWithBigrams(cops, bigrams); 
                }
            }
            file.Close();
            return cops;
        }

        private static string TraimNoneAlphabetChars(string input_line)
        {
            input_line.ToLower();   //convert all characters in input_line to lower case.
            string output_line;
            string pattern = @"[^a-z]";
            string replacement = "";
            Regex defaultRegex = new Regex(pattern);
            output_line = defaultRegex.Replace(input_line, replacement);
            return output_line;
        }

        //BuildBigramsFromString can only be used when input_line.Length > 2
        private static List<Tuple<char, char>> BuildBigramsFromString(string input_line)
        {
            List<Tuple<char, char>> output_bigrams = new List<Tuple<char,char>>();
            int first_char_index = 0;
            int second_char_index = 1;
            while (second_char_index < input_line.Length)
            {
                Tuple<char, char> a_bigram = Tuple.Create<char, char>(input_line[first_char_index], input_line[second_char_index]);
                output_bigrams.Add(a_bigram);
                first_char_index++;
                second_char_index++;
            }
            return output_bigrams;
        }

        private static void IrrigateCorporaWithBigrams(Corpora cops, List<Tuple<char, char>> bigrams)
        {
            foreach(Tuple<char, char> tuple_char_char in bigrams)
            {
                char first_char = tuple_char_char.Item1;
                char second_char = tuple_char_char.Item2;
                cops.IrrigateBigram(first_char, second_char);
            }
        }
    }
}
