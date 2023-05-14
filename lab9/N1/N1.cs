using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N1
{
    class N1
    {
        static void Main(string[] args)
        {
            string f = @"transactions.csv";
            string d = "yyyy-MM-dd";
            int s = 10;
            int c = 0;
            var t = new Dictionary<DateTime, double>();
            Func<string, DateTime> getDate = (string input) => DateTime.ParseExact(input, d, null);
            Func<string, double> getAmount = (string input) => double.Parse(input);
            using (var reader = new StreamReader(f))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var date = getDate(values[0]);
                    var amount = getAmount(values[1]);
                    if (t.ContainsKey(date))
                    {
                        t[date] += amount;
                    }
                    else
                    {
                        t[date] = amount;
                    }
                    c++;
                    if (c == s)
                    {
                        WriteBatchToCsv(f, t);
                        t.Clear();
                        c = 0;
                    }
                }
            }
            if (t.Any())
            {
                WriteBatchToCsv(f, t);
            }
        }
        static void WriteBatchToCsv(string filePath, Dictionary<DateTime, double> t)
        {
            var lines = t.Select(kv => $"{kv.Key:yyyy-MM-dd},{kv.Value:F2}");
            using (var writer = new StreamWriter(filePath, true))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
            var totalAmount = t.Sum(kv => kv.Value);
        }
    }
}
