using System;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ImiImport
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ImiImport.exe favorites.imixch > favorites.tsv");
                return;
            }

            dynamic obj;
            try
            {
                obj = JObject.Parse(System.IO.File.ReadAllText(args[0]));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to parse file [{0}]. Error: [{1}]", args[0], ex.Message);
                return;
            }

            var items = obj?.lists?[0]?.list?.items;

            Console.OutputEncoding = Encoding.UTF8;
            foreach (var item in items)
            {
                if (item.type == 0)
                {
                    Console.Write(string.Join(", ", item.kanji.Count > 0 ? item.kanji : item.reading) + "\t");
                    Console.Write(string.Join(", ", item.meaning?.eng) + "\t");
                    Console.Write(string.Join(", ", item.reading) + "\n");
                }
                else if (item.type == 4)
                {
                    Console.Write(item.example.jap + "\t");
                    Console.Write(item.example.eng + "\t\n");
                }
            }
        }
    }
}
