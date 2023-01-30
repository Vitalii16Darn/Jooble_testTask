using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    /// <summary>
    /// To record result in format: (in) -> (outs)..
    /// </summary>
    public class TransformToDictionaryForm
    {
        /// <summary>
        /// Generates the key-value pairs of: input word, received parts of this word
        /// </summary>
        /// <returns>key-values pairs</returns>
        static public Dictionary<string,List<string>> ResultToDictionary(List<List<string>> wordParts, List<string> text)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach(var word in text)
            {
                result.Add(word, wordParts.ElementAt(text.IndexOf(word)));
            }
            
            return result;
        }
    }
}
