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
            string language = Path.GetFileName(filename).Replace(".txt", "");
            Corpora cops = new Corpora();
            cops.Init(language);
            StreamReader file = new StreamReader(filename);
            string line;
            while((line = file.ReadLine()) != null)
            {
                string str_in_process = TrimNoneAlphabetChars(line);
                if (2 <= str_in_process.Length)
                {
                    List<Tuple<char, char>> bigrams = BuildBigramsFromString(str_in_process);
                    IrrigateCorporaWithBigrams(cops, bigrams); 
                }
            }
            file.Close();
            return cops;
        }

        private static string TrimNoneAlphabetChars(string input_line)
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

        public static string AnalyzeTextFromCorporas(List<Corpora> lst_cops, string inputed_text)
        {
            string processed_input_line = TrimNoneAlphabetChars(inputed_text);
            List<Tuple<char, char>> bigrams_to_be_analyzed = BuildBigramsFromString(processed_input_line);
            Tuple<string, double> pair_highest_language_score = Tuple.Create<string, double>("", double.MinValue);
            foreach(Corpora cops in lst_cops)
            {
                double score = 0;
                foreach (Tuple<char, char> bigram in bigrams_to_be_analyzed)
                {
                    char first_char = bigram.Item1;
                    char second_char = bigram.Item2;
                    double prob = cops.ProbabilityOfBigram(first_char, second_char);
                    //do some output here
                    Console.Write(cops.Language + ": Probability of Bigram " + bigram + " = " + prob + " ==> ");
                    score += Math.Log10(prob);
                    Console.WriteLine("log prob of sequence so far: " + score);
                }
                if (score >= pair_highest_language_score.Item2)
                {
                    pair_highest_language_score = Tuple.Create<string, double>(cops.Language, score);
                }
            }
            return pair_highest_language_score.Item1;
        }

        public static string AnalyzeTextFromCorporasWithDemandedOutput(List<Corpora> lst_cops, string inputed_text)
        {
            string processed_input_line = TrimNoneAlphabetChars(inputed_text);
            List<Tuple<char, char>> bigrams_to_be_analyzed = BuildBigramsFromString(processed_input_line);
            Dictionary<Corpora, double> dict_scores_of_cops = new Dictionary<Corpora, double>();
            foreach (Corpora cops in lst_cops)
            {
                dict_scores_of_cops.Add(cops, 0);
            }
            foreach (Tuple<char, char> bigram in bigrams_to_be_analyzed)
            {
                Program.mLogger.WriteLine("Bigram: " + bigram);
                foreach (Corpora cops in lst_cops)
                {
                    char first_char = bigram.Item1;
                    char second_char = bigram.Item2;
                    double prob = cops.ProbabilityOfBigram(first_char, second_char);
                    double score_of_this_prob = Math.Log10(prob);
                    dict_scores_of_cops[cops] = dict_scores_of_cops[cops] + score_of_this_prob;
                    Program.mLogger.WriteLine(cops.Language + ": P" + bigram + " = " + prob + " ==> log prob of sequence so far: " + dict_scores_of_cops[cops]);
                }
                Program.mLogger.WriteLine("");
            }

            Tuple<string, double> pair_highest_language_score = Tuple.Create<string, double>("", double.MinValue);
            foreach (var item in dict_scores_of_cops)
            {
                if (pair_highest_language_score.Item2 <= item.Value)
                {
                    pair_highest_language_score = Tuple.Create<string, double>(item.Key.Language, item.Value);
                }
            }

            return pair_highest_language_score.Item1;
        }
    }
}
