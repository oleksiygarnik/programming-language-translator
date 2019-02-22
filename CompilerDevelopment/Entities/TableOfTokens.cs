using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class TableOfTokens
    {
        public static Dictionary<int, string> Tokens = new Dictionary<int, string> { };

        //функция считывания таблицы лексем с файла формата " key - value"
        public static void ReadFromFile(string path = @"D:\ASM\testLabCompiler.txt")
        {
            Tokens = new Dictionary<int, string>();
            using (StreamReader sr = new StreamReader(path))
            {
                int key = 0;
                string value = "";
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {

                    string[] parse = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parse.Count() > 1)
                    {
                        for (int i = 0; i < parse.Count(); i++)
                        {
                            parse[i] = parse[i].Trim();
                            parse[i] = parse[i].Replace('|', ' ');
                            parse[i] = parse[i].Replace(" ", string.Empty);
                        }
                        key = Convert.ToInt32(parse[0]);
                        value = parse[1];
                        Tokens.Add(key, value);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public static void ViewTableOfTokens()
        {
            Console.WriteLine("   Table of Tokens");
            Console.WriteLine("------------------------");
            Console.WriteLine("|   Code  |    View    |");
            foreach (KeyValuePair<int, string> KeyValue in Tokens)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("|{0, 5}    |{1, 7}     |", KeyValue.Key,
                                                                       KeyValue.Value);
            }
            Console.WriteLine("------------------------");
        }
    }
}
