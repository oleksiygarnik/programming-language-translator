using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Lab9
{

    public static class TableOfIdentifiersLab9
    {
        public static List<(int, (string, int))> tableOfIdentifiers { get; set; } = new List<(int, (string, int))>();
        //int - код хеш функции, string - идетификатор, int поссилання таблицю констант

        public static Dictionary<char, int> alphabet = new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 2,
            ['c'] = 3,
            ['d'] = 4,
            ['e'] = 5,
            ['f'] = 6,
            ['g'] = 7,
            ['h'] = 8,
            ['i'] = 9,
            ['j'] = 10,
            ['k'] = 11,
            ['l'] = 12,
            ['m'] = 13,
            ['n'] = 14,
            ['o'] = 15,
            ['p'] = 16,
            ['q'] = 17,
            ['r'] = 18,
            ['s'] = 19,
            ['t'] = 20,
            ['u'] = 21,
            ['v'] = 22,
            ['w'] = 23,
            ['x'] = 24,
            ['y'] = 25,
            ['z'] = 26
        };

        public static List<int> TryCount { get; set; } = new List<int>();

        public static void RandomLoadTable(int countOfElement, int sizeOfTable, string method, string condition = "no add")
        {
            Random rnd = new Random();
            List<string> listOfRandomElements = new List<string>();
           
            string word = null;
            for(int i = 0; i < countOfElement; i++)
            {
                int countCharInWord = 10;//rnd.Next(10) + 1;
                for (int j = 0; j < countCharInWord; j++)
                {
                    //lol = alphabet.Where(w => w.Value == (rnd.Next(25) + 1)).First();
                    //word += lol.Key; // a - z
                    word += (char)(rnd.Next(97, 122) + 1);
                }
                if (listOfRandomElements.Contains(word) || tableOfIdentifiers.Select(ol=>ol.Item2.Item1).Contains(word))
                {
                    countOfElement++;
                    //i--;
                    word = null;
                }
                else
                {
                    listOfRandomElements.Add(word);
                    switch (method)
                    {
                        case "Square":
                            AddToTableOfIdentifierBySquareMethod(word, sizeOfTable, condition);
                            word = null;
                            break;
                        case "Random":
                            AddToTableOfIdentifierByRandomMethod(word, sizeOfTable, condition);
                            word = null;
                            break;
                    }

                    //AddToTableOfIdentifierByRandomMethod(word, sizeOfTable, condition);
                    //word = null;
                }
            }
        }

 
        public static void AddToTableOfIdentifierByLinearMethod(string word, int sizeOftable, string condition = "no add")
        {
           
            Random rnd = new Random();
            char[] wordByChar = word.ToCharArray();
            int num = Math.Abs(word.GetHashCode());

            foreach (char item in wordByChar)
                {
                //lol = alphabet.Where(w => w.Key == item).First();
                //num += lol.Value;
                }

            int tmp = 0;
            for (int i = 0; i < sizeOftable; i++)
            {
                tmp = num;
                num += i;
               num %= sizeOftable;
               
               if(tableOfIdentifiers.Select(a=>a.Item1).Contains(num))
               {
                    num = tmp;
                    continue;
               }
                else
               {
                    if (condition == "add")
                    {
                        tableOfIdentifiers.Add((num, (word, rnd.Next(100))));
                        //TryCount.Add(i + 1);

                    }
                    else
                    {
                        TryCount.Add(i + 1);
                    }
                    break;
               }
            }
        }

        public static void AddToTableOfIdentifierByRandomMethod(string word, int sizeOfTable, string condition = "no add")
        {
            int R = 1;
            int r = 0;
            Random rnd = new Random();
           
            int num = Math.Abs(word.GetHashCode());

          
            int tmp = 0;

            for (int i = 0; i < sizeOfTable; i++)
            {
                
                tmp = num;
                num += r;
                num %= sizeOfTable;
                if(i == sizeOfTable-1)
                {
                    i = 0;
                    R = 1;
                    r = 0;
                    num = tmp;
                }
                if (tableOfIdentifiers.Select(a => a.Item1).Contains(num))
                {
                    num = tmp;
                    R = R * 5;
                    R = R % (4 * sizeOfTable);
                    r = (int)(R / 4);
                    continue;
                }
                else
                {
                    if (condition == "add")
                    {
                        tableOfIdentifiers.Add((num, (word, rnd.Next(100))));
                        //TryCount.Add(i + 1);
                        break;

                    }
                    else
                    {
                        TryCount.Add(i + 1);
                        break;
                    }
                }
            }


        }


        public static void AddToTableOfIdentifierBySquareMethod(string word, int sizeOfTable, string condition = "no add")
        {
            Random rnd = new Random();
           
            int num = Math.Abs(word.GetHashCode());
            int additional = 0;
         
            //configuration constants
            int a = 1;
            int b = 1;
            int c = 0;


            int tmp = 0;
            for (int i = 0; i < sizeOfTable; i++)
            {
                tmp = num;
                additional = a * (int)Math.Pow(i, 2) + b * i + c;
                num += additional;
                //num += (a * (int)Math.Pow(i, 2) + b*i + c);
                num %= sizeOfTable;
                ////if(i == (sizeOfTable -1))
                ////{
                ////    i = 0;
                ////}
                if (tableOfIdentifiers.Select(el => el.Item1).Contains(num))
                {
                    num = tmp;
                    continue;
                }
                else
                {
                    if (condition == "add")
                    {
                        tableOfIdentifiers.Add((num, (word, rnd.Next(100))));
                        break;
                        //TryCount.Add(i + 1);

                    }
                    else
                    {
                        TryCount.Add(i + 1);
                        break;
                    }
                    //break;
                }
            }


        }

        public static float CalculateAverageValue()
        {
            float average = 0;
            foreach ( var oneTry in TryCount)
            {
                average += oneTry;
            }
            average /= TryCount.Count();
            return average;
        }
    }
}
