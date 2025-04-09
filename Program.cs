using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace character_to_pinyin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;  // enable Chinese characters to be input on the console
            Console.OutputEncoding = Encoding.UTF8; // enable Chinese characters to be printed on the console

            IDictionary<string, WordInfo> word_dictionary = Program.Populate_dictionary();  // create and populate the dictionary using file data

            while (true)
            {
                Console.WriteLine("Enter a Chinese Word");  // have the user input a Chinese word
                string input_character = Console.ReadLine();              
                Console.WriteLine(word_dictionary[input_character].pinyin); // print the pinyin
            }
            

        }

        private static IDictionary<string, WordInfo> Populate_dictionary()
        {
            string path = "D:/docs/C# projects/character_to_pinyin/cedict_ts.u8";   // file with Chinese characters, pinyin, meanings

            string pinyin_pattern = @"\[(.*?)\]";  // regex for pinyin extraction
            string definition_pattern = @"(?<=/)(.*?)(?=/)";   // regex for definition extraction

            IDictionary<string, WordInfo> word_dictionary = new Dictionary<string, WordInfo>();   // dictionary to hold data from file

            foreach (string line in File.ReadLines(path, Encoding.UTF8))
            {
                if (!line.Contains('#')) // skip any comments
                {
                    string[] words_simplified = line.Split(new char[] { ' ' });
                    string simplified = words_simplified[1];    // extract simplified characters

                    Match regex_match = Regex.Match(line, pinyin_pattern);
                    string pinyin = regex_match.Groups[1].Value;    // extract pinyin

                    Match regex_match2 = Regex.Match(line, definition_pattern);
                    string definition_ = regex_match2.Groups[1].Value;    // extract definition

                    WordInfo wordInfo = new WordInfo    // put all values in class for each word
                    {
                        character = simplified,
                        pinyin = pinyin,
                        definition = definition_
                    };

                    word_dictionary[simplified] = wordInfo; // put word in the dictionary with pinyin and definition 


                }
            }

            return word_dictionary;
        }
    }

    class WordInfo
    {
        public string character { get; set; }
        public string pinyin { get; set; }
        public string definition { get; set; }
    }
}
