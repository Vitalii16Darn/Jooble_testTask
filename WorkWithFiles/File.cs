using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    public static class File
    {
        static public async Task<List<string>> ReadFromFile(string path)
        {
            string text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                text = await sr.ReadToEndAsync();
            }
            List<string> words = text.ToLower().Split('\n').ToList();
            return words;
        }

        /// <summary>
        /// Writes our long words to check into a file as a key
        /// Writes only suitable parts of long word to a file as a values
        /// Shows if long word is divisible (it is divisible if this key has more than 1 value)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dictionary"></param>
        static public async void WriteToFile(string path, Dictionary<string, List<string>> dictionary)
        {
            using (StreamWriter sw = new StreamWriter(path, false)) //перезаписуємо файл, не дописуючи зміни в кінець
            {
                await sw.WriteLineAsync("В консолі працюючого додатку для кожного тестового слова можна знайти список слів зі словника, які підходять як потенційні частини цього довгого слова");
                foreach (var d in dictionary.Keys)
                {
                    await sw.WriteAsync($"(in) {d}" + " -> (out) ");
                    var isDivisible = dictionary[d].Count > 1 ? "//слово яке змогли розбити" : "//слово, яке неможливо розбити";
                    foreach (var dv in dictionary[d])
                        await sw.WriteAsync($"{dv}, ");
                    await sw.WriteAsync(isDivisible);
                    await sw.WriteLineAsync();
                    await sw.WriteLineAsync();
                }
            }
        }
    }
}
