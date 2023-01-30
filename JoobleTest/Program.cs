using JoobleTest;
using System.Text;

WordDivide wordDivide = new WordDivide();
//List<string> inputText = wordDivide.Dictionary();
List<string> inputText = wordDivide.Words;
Dictionary<string,List<string>> result = wordDivide.Alhorythm();
/*foreach (var r in result)
{
    foreach(var r2 in r)
        Console.WriteLine(r2);
    Console.WriteLine();
}*/
wordDivide.ResultIntoFile();

Console.ReadLine();
