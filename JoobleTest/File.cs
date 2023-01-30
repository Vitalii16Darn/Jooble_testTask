using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoobleTest
{
    internal static class File
    {
        static public async Task<List<string>> ReadFromFile(string path)
        {
            string text = "";
            using(StreamReader sr = new StreamReader(path))
            {
                text=await sr.ReadToEndAsync();
            }
            List<string> words=text.ToLower().Split('\n').ToList();
            return words;
        }

        static public async void WriteToFile(string path, string text)
        {
            using(StreamWriter sw = new StreamWriter(path,false)) //перезаписуємо файл, не дописуючи зміни в кінець
            {
                await sw.WriteAsync(text);
            }
        }

        static public async void WriteToFile(string path, Dictionary<string, List<string>> dictionary)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                foreach (var d in dictionary.Keys)
                {
                    await sw.WriteAsync($"(in) {d}" + " -> (out) ");
                    foreach (var dv in dictionary[d])
                        await sw.WriteAsync($"{dv}, ");
                    await sw.WriteLineAsync();
                    await sw.WriteLineAsync();
                }
            }
        }
    }
}
