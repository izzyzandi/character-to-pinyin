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
                Console.WriteLine(Format_pinyin(word_dictionary[input_character].pinyin)); // format and print the pinyin
            }
            

        }

        private static String Format_pinyin(String pinyin_) // take each formatted syllable and combine to one word
        {
            String formatted_pinyin = "";

            string[] split_pinyin = pinyin_.Split(new char[] { ' ' });
            
            foreach (string syllable in split_pinyin)
            {
                String current_formatted_pinyin = " ";

                if (syllable.EndsWith('1'))
                {
                    current_formatted_pinyin = pinyin_format_tone1(syllable);
                }
                else if (syllable.EndsWith('2'))
                {
                    current_formatted_pinyin = pinyin_format_tone2(syllable);
                }
                else if (syllable.EndsWith('3'))
                {
                    current_formatted_pinyin = pinyin_format_tone3(syllable);
                }
                else if (syllable.EndsWith('4'))
                {
                    current_formatted_pinyin = pinyin_format_tone4(syllable);
                }
                else
                    current_formatted_pinyin = syllable.Replace('5',' ');

                formatted_pinyin += current_formatted_pinyin;
            }

            return formatted_pinyin;
        }

        private static string pinyin_format_tone1(String syllable)  // format tone 1 syllables
        {
            string x = syllable.Replace('1', ' ');  // remove the number at the end of the syllable
            string formatted_syllable = x.Replace('a', 'ā'); // no matter the vowel combination, 'a' has priority for the tone mark

            if (!syllable.Contains('a'))    // if no 'a', 'o' and 'e' are of equal priority and do not feature as a tone pairing so can be checked together
            {
                formatted_syllable = formatted_syllable.Replace('o', 'ō');
                formatted_syllable = formatted_syllable.Replace('e', 'ē');

                if (!syllable.Contains('o') && !syllable.Contains('e')) // if no 'a','e','o', then 'i' and 'u' get the tone mark depending on which is last
                {
                    if (syllable.Contains('i') && syllable.Contains('u'))
                    {
                        var i_index = formatted_syllable.IndexOf('i');
                        var u_index = formatted_syllable.IndexOf('u');

                        if (i_index < u_index)
                        {
                            formatted_syllable = formatted_syllable.Replace('u', 'ū');
                        }
                        else
                        {
                            formatted_syllable = formatted_syllable.Replace('i', 'ī');
                        }


                    }
                    else    // if no 'i','u' tone pairing, then accent the vowel in the syllable
                    {
                        formatted_syllable = formatted_syllable.Replace('i', 'ī');
                        formatted_syllable = formatted_syllable.Replace('u', 'ū');
                        formatted_syllable = formatted_syllable.Replace('v', 'ü');
                    }

                }

            }

            return formatted_syllable;
        }


        private static string pinyin_format_tone2(String syllable)  // format tone 2 syllables
        {
            string x = syllable.Replace('2', ' ');  // remove the number at the end of the syllable
            string formatted_syllable = x.Replace('a', 'á'); // no matter the vowel combination, 'a' has priority for the tone mark

            if (!syllable.Contains('a'))    // if no 'a', 'o' and 'e' are of equal priority and do not feature as a tone pairing so can be checked together
            {
                formatted_syllable = formatted_syllable.Replace('o', 'ó');
                formatted_syllable = formatted_syllable.Replace('e', 'é');

                if (!syllable.Contains('o') && !syllable.Contains('e')) // if no 'a','e','o', then 'i' and 'u' get the tone mark depending on which is last
                {
                    if (syllable.Contains('i') && syllable.Contains('u'))
                    {
                        var i_index = formatted_syllable.IndexOf('i');
                        var u_index = formatted_syllable.IndexOf('u');

                        if (i_index < u_index)
                        {
                            formatted_syllable = formatted_syllable.Replace('u', 'ú');
                        }
                        else
                        {
                            formatted_syllable = formatted_syllable.Replace('i', 'í');
                        }


                    }
                    else    // if no 'i','u' tone pairing, then accent the vowel in the syllable
                    {
                        formatted_syllable = formatted_syllable.Replace('i', 'í');
                        formatted_syllable = formatted_syllable.Replace('u', 'ú');
                        formatted_syllable = formatted_syllable.Replace('v', 'ü');
                    }

                }

            }

            return formatted_syllable;
        }

        private static string pinyin_format_tone3(String syllable)  // format tone 3 syllables
        {
            string x = syllable.Replace('3', ' ');  // remove the number at the end of the syllable
            string formatted_syllable = x.Replace('a', 'ǎ'); // no matter the vowel combination, 'a' has priority for the tone mark

            if (!syllable.Contains('a'))    // if no 'a', 'o' and 'e' are of equal priority and do not feature as a tone pairing so can be checked together
            {
                formatted_syllable = formatted_syllable.Replace('o', 'ǒ');
                formatted_syllable = formatted_syllable.Replace('e', 'ě');

                if (!syllable.Contains('o') && !syllable.Contains('e')) // if no 'a','e','o', then 'i' and 'u' get the tone mark depending on which is last
                {
                    if (syllable.Contains('i') && syllable.Contains('u'))
                    {
                        var i_index = formatted_syllable.IndexOf('i');
                        var u_index = formatted_syllable.IndexOf('u');

                        if (i_index < u_index)
                        {
                            formatted_syllable = formatted_syllable.Replace('u', 'ǔ');
                        }
                        else
                        {
                            formatted_syllable = formatted_syllable.Replace('i', 'ǐ');
                        }


                    }
                    else    // if no 'i','u' tone pairing, then accent the vowel in the syllable
                    {
                        formatted_syllable = formatted_syllable.Replace('i', 'ǐ');
                        formatted_syllable = formatted_syllable.Replace('u', 'ǔ');
                        formatted_syllable = formatted_syllable.Replace('v', 'ǚ');
                    }

                }

            }

            return formatted_syllable;
        }

        private static string pinyin_format_tone4(String syllable)  // format tone 4 syllables
        {
            string x = syllable.Replace('4', ' ');  // remove the number at the end of the syllable
            string formatted_syllable = x.Replace('a', 'à'); // no matter the vowel combination, 'a' has priority for the tone mark

            if (!syllable.Contains('a'))    // if no 'a', 'o' and 'e' are of equal priority and do not feature as a tone pairing so can be checked together
            {
                formatted_syllable = formatted_syllable.Replace('o', 'ò');
                formatted_syllable = formatted_syllable.Replace('e', 'è');

                if (!syllable.Contains('o') && !syllable.Contains('e')) // if no 'a','e','o', then 'i' and 'u' get the tone mark depending on which is last
                {
                    if (syllable.Contains('i') && syllable.Contains('u'))
                    {
                        var i_index = formatted_syllable.IndexOf('i');
                        var u_index = formatted_syllable.IndexOf('u');

                        if (i_index < u_index)
                        {
                            formatted_syllable = formatted_syllable.Replace('u', 'ù');
                        }
                        else
                        {
                            formatted_syllable = formatted_syllable.Replace('i', 'ì');
                        }


                    }
                    else    // if no 'i','u' tone pairing, then accent the vowel in the syllable
                    {
                        formatted_syllable = formatted_syllable.Replace('i', 'ì');
                        formatted_syllable = formatted_syllable.Replace('u', 'ù');
                        formatted_syllable = formatted_syllable.Replace('v', 'ǚ');
                    }

                }

            }

            return formatted_syllable;
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
