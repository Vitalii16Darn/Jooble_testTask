using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoobleTest;
using WorkWithFiles;
using File = WorkWithFiles.File;

namespace JoobleTest
{
    internal class WordDivide
    { 
        /// <summary>
        /// list of all available words from dictionary
        /// </summary>
        public List<string> Words { get; private set; } = File.ReadFromFile("de-dictionary.tsv").Result;
        /// <summary>
        /// list of all our long words that we must check
        /// </summary>
        public List<string> Text { get; set; } = File.ReadFromFile("de-test-words.tsv").Result; 


        public void ResultIntoFile()
        {
            Dictionary<string, List<string>> result = TransformToDictionaryForm.ResultToDictionary(Alhorythm(),Text);
            File.WriteToFile("de-result.txt", result);
        }

        /// <summary>
        /// Firstly for each of input word it searches words from dictionary that are part of this input word
        /// If there aren't suitable words in dictionary then this word is indivisible (IT f.e)
        /// We find start part of input word in the dictionary and the rest part
        /// For searching the end of the word it uses counting of the letters
        /// We find new parts of the word till all letters of long word would be not used
        /// If there is long word in dictionary then it wouldn't be divided (even if it consists from smaller words from dict.)
        /// </summary>
        /// <returns>list of all suitable parts for each long word</returns>
        public List<List<string>> Alhorythm()
        { 
            List<List<string>> result = new List<List<string>>();
            foreach (var word in Text)
            {
                List<string> wordParts = new List<string>(); //підходящі частини для кожного слова записуватимуться сюди
                int lettersInvolved = 0; //підраховуємо кількість залучених букв великого слова
                //якщо в словнику не міститься ніякої частини слова, то воно - неподільне
                var suitableWords = Words.Any(w => word.Contains(w)) ? Words.Where(w => word.Contains(w)) : new List<string> { word }; 
                var startPart = suitableWords.Where(sw => sw.First() == word.First()).MaxBy(w => w.Length); //початкова частина слова (найдовше зі словника слово, яке міститься в тестовому слові і починається з тієї ж букви)
                if (startPart == null || startPart.Length<2)
                    startPart = word;
                StringBuilder rest=new StringBuilder(); //залишок слова (без початкової частини
                rest.Append(word).Remove(word.IndexOf(startPart), startPart.Length);
                lettersInvolved += startPart.Length;
                Console.WriteLine($"start= {startPart}");

                wordParts.Add(startPart);
                
                Console.WriteLine($"rest= {rest}");
                while (lettersInvolved < word.Length - 1)
                {
                    //шукаємо нові частини слова (має починатися з першої букви залишку)
                    //частина не повинна бути такою ж як початок слова
                    //var part = suitableWords.Where(sw => sw.StartsWith(rest.ToString().First())).Where(w => w != startPart).MaxBy(w => w.Length);
                    var part = suitableWords.Where(sw => sw.StartsWith(rest.ToString().First()) && sw!=startPart).MaxBy(w => w.Length);
                    if (part == null)
                    {
                        //якщо нової частини більше не знайдено, то це кінець
                        part = word;
                        wordParts.Clear(); 
                    }
                    wordParts.Add(part);
                    //зі знаходженням кожної нової частини слова не забуваємо зменшувати залишок
                    //та рахувати кількість використаних букв великого слова
                    rest.Clear();
                    rest.Append(word.Except(part));
                    lettersInvolved += part.Length;
                }
                //наповнюємо список, який містить в собі списки з підходящими частинами до кожного довгого слова
                result.Add(wordParts); 
                foreach (var s in suitableWords)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("\n");
            }
            return result;
        }
    }
}
