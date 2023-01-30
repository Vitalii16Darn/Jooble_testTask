using JoobleTest;
using System.Text;

WordDivide wordDivide = new WordDivide();
List<string> inputText = wordDivide.Words;
List<List<string>> result = wordDivide.Alhorythm();
wordDivide.ResultIntoFile();

Console.ReadLine();
