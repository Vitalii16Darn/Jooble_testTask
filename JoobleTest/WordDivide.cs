using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoobleTest
{
    internal class WordDivide
    { 
        public List<string> Words { get; private set; } = File.ReadFromFile("de-dictionary.tsv").Result;
        public List<string> Text { get; private set; } = File.ReadFromFile("de-test-words.tsv").Result;


        public void ResultIntoFile()
        {
            Dictionary<string,List<string>> result = Alhorythm(); 
            File.WriteToFile("de-result.txt", result);
        }

        public IEnumerable<string> SuitableWords(List<string> inputText, List<string> dictionary)
        {
            IEnumerable<string> result=new List<string>();
            foreach (var word in inputText)
            {
                result = dictionary.Any(d => word.Contains(d)) ? dictionary.Where(d => word.Contains(d)) : new List<string> { word };
            }
            return result;
        }

        /// <summary>
        /// Divides input text into words.
        /// Firstly for each of this input word it searches words from dictionary that are part of this input word
        /// If there aren't suitable words in dictionary then this word is indivisible (IT f.e)
        /// We find start part of input word in the dictionary and the rest part
        /// For searching the end of the word it uses counting of the letters
        /// We find new parts of the word 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,List<string>> Alhorythm()
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            //List<string> words = Dictionary();
            //List<string> text = InputText();
            List<List<string>> result = new List<List<string>>();
            foreach (var word in Text)
            {
                List<string> wordParts = new List<string>();
                int lettersInvolved = 0; //підраховуємо кількість залучених букв слова
                var suitableWords = Words.Any(w => word.Contains(w)) ? Words.Where(w => word.Contains(w)) : new List<string> { word }; 
                var startPart = suitableWords.Where(sw => sw.First() == word.First()).MaxBy(w => w.Length);
                if (startPart == null || startPart.Length<2)
                    startPart = word;
                StringBuilder rest=new StringBuilder();
                rest.Append(word).Remove(word.IndexOf(startPart), startPart.Length);
                lettersInvolved += startPart.Length;
                Console.WriteLine($"start= {startPart}");

                wordParts.Add(startPart);
                
                Console.WriteLine($"rest= {rest}");
                while(lettersInvolved<word.Length-1)
                {
                    //var part = suitableWords.Where(sw => sw.First() == rest.ToString().First()).Max(w=>w); 
                    var part = suitableWords.Where(sw => sw.First() == rest.ToString().First()).Where(w=>w!=startPart).MaxBy(w=>w.Length);
                    if (part is null)
                    {
                        part = word;
                        wordParts.Clear();
                        //wordParts.Remove(startPart);
                    }
                    wordParts.Add(part);
                    rest.Clear();
                    rest.Append(word.Except(part));
                    lettersInvolved += part.Length;
                }
                result.Add(wordParts);
                dictionary.Add(word, wordParts);
                /*foreach (var wp in wordParts)
                    Console.WriteLine(wp);*/
                foreach (var s in suitableWords)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("\n");
            }
            return dictionary; 
        }
    }
}
