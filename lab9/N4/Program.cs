using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N4
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\ZAKHAR\MyTextFiles\";
            List<string> f = Directory.GetFiles(directoryPath, "*.txt").ToList();
            Func<string, IEnumerable<string>> tokenizer = TokenizeFile;
            Func<IEnumerable<string>, IDictionary<string, int>> wordCounter = CountWords;
            Action<IDictionary<string, int>> report = DisplayWordFrequency;
            foreach (string filePath in f)
            {
                string fileName = Path.GetFileName(filePath);
                Console.WriteLine($"Processing file: {fileName}");
                string fileContent = File.ReadAllText(filePath);
                IEnumerable<string> tokens = tokenizer(fileContent);
                IDictionary<string, int> wordFrequency = wordCounter(tokens);
                report(wordFrequency);
            }
            Console.ReadLine();
        }
        static IEnumerable<string> TokenizeFile(string content)
        {
            return content.Split(new char[] { ' ', '.', ',', ';', ':', '-', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
        static IDictionary<string, int> CountWords(IEnumerable<string> tokens)
        {
            Dictionary<string, int> w = new Dictionary<string, int>();
            foreach (string token in tokens)
            {
                if (w.ContainsKey(token))
                {
                    w[token]++;
                }
                else
                {
                    w[token] = 1;
                }
            }
            return w;
        }
        static void DisplayWordFrequency(IDictionary<string, int> wordFrequency)
        {
            foreach (KeyValuePair<string, int> word in wordFrequency.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{word.Key}: {word.Value}");
            }
            Console.WriteLine();
        }
    }
}