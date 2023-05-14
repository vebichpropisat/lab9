using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace N2
{
    class Program
    {
        static void Main()
        {
            string d = "C:\Users\ZAKHAR\products";
            Predicate<Product> filter = product => product.Category == "Food" && product.Price < 10; // критерії фільтрації

            foreach (int fileNumber in Enumerable.Range(1, 10))
            {
                string filePath = Path.Combine(d, $"{fileNumber}.json");
                if (!File.Exists(filePath))
                    continue;

                using (FileStream fs = File.OpenRead(filePath))
                {
                    var products = JsonSerializer.DeserializeAsync<Product[]>(fs).Result;
                    foreach (var product in products)
                    {
                        if (filter(product))
                            Console.WriteLine($"{product.Name} ({product.Category}): {product.Price}");
                    }
                }
            }
        }
    }
