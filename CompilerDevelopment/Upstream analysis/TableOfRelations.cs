using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis
{
    static class TableOfRelations
    {
        public static Dictionary<string, Dictionary<string, string>> tableOfRelation = new Dictionary<string, Dictionary<string, string>>();

        //Таблица с First, First+, Last, Last+
        public static Dictionary<string, Dictionary<string, List<Element>>> supportTable = new Dictionary<string, Dictionary<string, List<Element>>>();

        public static void LoadFields()
        {
            List<string> all = new List<string>();
            all = Grammar.GetAllElementsStartWithNonTerminal();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string value in all)
            {
                foreach (string valueNext in all)
                {

                    dict.Add(valueNext, null);
                    //dict[valueNext] = ">";

                }
                dict.Add("#", ">");
                tableOfRelation[value] = dict;
                dict = new Dictionary<string, string>();
            }

            foreach (string value in all)
            {

                dict.Add(value, "<");
                //dict[valueNext] = ">";

            }
            dict.Add("#", null);
            tableOfRelation["#"] = dict;

            //dict.Add("#", ">");

        }


        public static void LoadEquels() // в этом методе мы определяем знаки =
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            List<Node> nodes = new List<Node>();
            int tmpCount = 0;
            string equel = null;
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar) // проганяю по левой части грамматики
            {
                foreach (var node in KeyValue.Value) // проганяю каждую правую часть по 1й левой
                {
                    if (node.elements.Count > 1) // если справа в узле больше чем одного элемента, значит можна найти знак ==========
                    {
                        tmpCount = 0;
                        while (tmpCount != node.elements.Count - 1)
                        {
                            tableOfRelation.TryGetValue(node.elements[tmpCount].Name, out dict);
                            {
                                dict.TryGetValue(node.elements[tmpCount + 1].Name, out equel);
                                dict[node.elements[tmpCount + 1].Name] = "=";
                            }
                            tmpCount++;
                        }
                    }
                }
            }

        }
        public static void LoadMoreSign()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, string> dict2 = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> Save = new Dictionary<string, Dictionary<string, string>>();


            Dictionary<string, List<Element>> dict1 = new Dictionary<string, List<Element>>();

            List<Element> elements = new List<Element>();

            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
             string sign = null;
            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in tableOfRelation)
            {
                if (all.Contains(KeyValue.Key))
                {
                    foreach (KeyValuePair<string, string> KeyValueNext in KeyValue.Value)
                    {
                        if (KeyValueNext.Value == "=")
                        {
                            
                            supportTable.TryGetValue("LastPlus", out dict1);
                            
                                dict1.TryGetValue(KeyValue.Key, out elements); // получаем все элементы которые будут >
                            Dictionary<string, string> Save1;




                                    foreach (var item in elements)
                                    {
                                      Save1 = new Dictionary<string, string>();
                                      Save1[KeyValueNext.Key] = ">";

                                        if (Save.TryGetValue(item.Name, out dict))
                                        {
                                            dict[KeyValueNext.Key] = ">";
                                        }
                                        else
                                        {
                                            Save[item.Name] = Save1;
                                        }
                                string lol;
                                Save1.TryGetValue("-",out lol);
                                if(lol==">")
                                {
                                    int a;
                                }
                                    
                                    }
                                
       
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in Save)
            {
                tableOfRelation.TryGetValue(KeyValue.Key, out dict);
                {
                    foreach (KeyValuePair<string, string> KeyValue1 in KeyValue.Value)
                    {
                        dict.TryGetValue(KeyValue1.Key, out sign);
                        dict[KeyValue1.Key] += ">";
                    }

                }
            }

        }
        public static void LoadLessSign() // нужно проверить есть если одинаковые ключи
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> Save = new Dictionary<string, Dictionary<string, string>>();


            Dictionary<string, List<Element>> dict1 = new Dictionary<string, List<Element>>();

            List<Element> elements = new List<Element>();

            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            string sign = null;
            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in tableOfRelation)
            {
                foreach (KeyValuePair<string, string> KeyValueNext in KeyValue.Value)
                {
                    if (all.Contains(KeyValueNext.Key) && KeyValueNext.Value == "=")
                    {
                        Dictionary<string, string> Save1 = new Dictionary<string, string>();
                        supportTable.TryGetValue("FirstPlus", out dict1); // достаем словарь для FirstPLus для всех нетерминалов
                        {
                            dict1.TryGetValue(KeyValueNext.Key, out elements); // достаем по необходимому нетерминалу(Который второй попался)
                            tableOfRelation.TryGetValue(KeyValue.Key, out dict); // достаем для update
                            {
                                foreach (var item in elements)
                                {
                                    if (dict.TryGetValue(item.Name, out sign))
                                    {
                                        if (sign == "=" || sign == ">")
                                        {
                                            Save1[item.Name] = "!!!";
                                        }
                                        else
                                        {
                                            Save1[item.Name] = "<";
                                        }

                                    }

                                }
                                Save[KeyValue.Key] = Save1;

                            }
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in Save)
            {
                tableOfRelation.TryGetValue(KeyValue.Key, out dict);
                {
                    foreach (KeyValuePair<string, string> KeyValue1 in KeyValue.Value)
                    {
                        dict.TryGetValue(KeyValue1.Key, out sign);
                        dict[KeyValue1.Key] += "<";
                        //if (sign == "=" || sign == ">")
                        //{
                        //    dict[KeyValue1.Key] = "<";
                        //}
                        //else
                        //{
                        //    dict[KeyValue1.Key] = "!!!";
                        //}
                        //dict[KeyValue1.Key] = "<";
                    }

                }
            }

        }

        public static void TwoNonTernminal()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> Save = new Dictionary<string, Dictionary<string, string>>();


            Dictionary<string, List<Element>> dict1 = new Dictionary<string, List<Element>>();
            Dictionary<string, List<Element>> dict2 = new Dictionary<string, List<Element>>();

            List<Element> elements1 = new List<Element>();
            List<Element> elements2 = new List<Element>();

            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            //string sign = null;
            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in tableOfRelation)
            {
                if (all.Contains(KeyValue.Key))
                {
                    foreach (KeyValuePair<string, string> KeyValueNext in KeyValue.Value)
                    {
                        if (all.Contains(KeyValueNext.Key) && KeyValueNext.Value == "=")
                        {
                            Dictionary<string, string> Save1 = new Dictionary<string, string>();
                            supportTable.TryGetValue("LastPlus", out dict1); // достаем словарь для LastPLus для всех нетерминалов
                            {
                                dict1.TryGetValue(KeyValue.Key, out elements1); // достаем по необходимому нетерминалу(Который второй попался)
                            }
                            supportTable.TryGetValue("FirstPlus", out dict2);
                            {
                                dict2.TryGetValue(KeyValueNext.Key, out elements2);
                            }
                            foreach (var item in elements1)
                            {
                                foreach (var item1 in elements2)
                                {
                                    if (tableOfRelation[item.Name][item1.Name] != ">")
                                    {
                                        tableOfRelation[item.Name][item1.Name] += ">";
                                    }


                                }

                            }


                        }
                    }
                }
            }
        }

        public static void LoadSupportTable()
        {
            LoadSupportTableFisrt();
            LoadSupportTableFisrtPlus();
            LoadSupportTableLast();
            LoadSupportTableLastPlus();
        }

        public static void LoadSupportTableFisrt()
        {
            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            Dictionary<string, List<Element>> dict = new Dictionary<string, List<Element>>();
            foreach (string value in all)
            {

                List<Node> nodes = new List<Node>();
                List<Element> nodesForSupporTableFirst = new List<Element>();
                NonTerminal ex = new NonTerminal(value);
                foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
                {
                    if (KeyValue.Key.Name == value)
                    {
                        nodes = KeyValue.Value;
                    }
                }
                foreach (var node in nodes)
                {
                    nodesForSupporTableFirst.Add(node.elements[0]); // заполняем First
                }
                dict[value] = nodesForSupporTableFirst;
            }
            supportTable["First"] = dict;

        }

        public static void LoadSupportTableLast()
        {
            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            Dictionary<string, List<Element>> dict = new Dictionary<string, List<Element>>();
            foreach (string value in all)
            {

                List<Node> nodes = new List<Node>();
                List<Element> nodesForSupporTableLast = new List<Element>();
                NonTerminal ex = new NonTerminal(value);
                foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
                {
                    if (KeyValue.Key.Name == value)
                    {
                        nodes = KeyValue.Value;
                    }
                }
                foreach (var node in nodes)
                {
                    int lastElem = node.elements.Count - 1;
                    nodesForSupporTableLast.Add(node.elements[lastElem]); // заполняем First
                }
                dict[value] = nodesForSupporTableLast;
            }
            supportTable["Last"] = dict;

        }

        public static void LoadSupportTableLastPlus()
        {
            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            Dictionary<string, List<Element>> dict = new Dictionary<string, List<Element>>();
            foreach (string value in all)
            {

                List<Node> nodes = new List<Node>();
                List<Element> nodesForSupporTableLastPlus = new List<Element>();
                NonTerminal ex = new NonTerminal(value);
                foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
                {
                    if (KeyValue.Key.Name == value) // нахожу нетерминал в грамматике и получаю все правила
                    {
                        nodes = KeyValue.Value;
                    }
                }


                foreach (var node in nodes) // проганяю все правила
                {
                    
                    int lastElem = node.elements.Count - 1;
                    nodesForSupporTableLastPlus.Add(node.elements[lastElem]); // заполняем First
                    
                    if (node.elements[lastElem] is NonTerminal)
                    {
                        //var listsB = nodesForSupporTableFirstPlus.SelectMany(x => x.Li);
                        nodesForSupporTableLastPlus.AddRange(GetElementByLastPLus(node.elements[lastElem].Name));
                    }

                }
                dict[value] = nodesForSupporTableLastPlus;
            }
            supportTable["LastPlus"] = dict;
        }
        public static List<Element> GetElementByLastPLus(string name)
        {
            List<Element> elem = new List<Element>();
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
            {
                if (KeyValue.Key.Name == name)
                {
                    foreach (var node in KeyValue.Value)
                    {
                        int lastElem = node.elements.Count - 1;
                        elem.Add(node.elements[lastElem]);
                        if (node.elements[lastElem] is NonTerminal && node.elements[lastElem].Name != name)
                        //if (node.elements[lastElem] is Terminal)
                        {
                            elem.AddRange(GetElementByLastPLus(node.elements[lastElem].Name));
                        }
                    }
                }
            }
            return elem;
        }

        public static void LoadSupportTableFisrtPlus()
        {
            List<string> all = new List<string>();
            all = Grammar.GetAllNonTerminalFirslty();
            Dictionary<string, List<Element>> dict = new Dictionary<string, List<Element>>();
            foreach (string value in all)
            {

                List<Node> nodes = new List<Node>();
                List<Element> nodesForSupporTableFirstPlus = new List<Element>();
                NonTerminal ex = new NonTerminal(value);
                foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
                {
                    if (KeyValue.Key.Name == value) // нахожу нетерминал в грамматике и получаю все правила
                    {
                        nodes = KeyValue.Value;
                    }
                }


                foreach (var node in nodes) // проганяю все правила
                {
                    nodesForSupporTableFirstPlus.Add(node.elements[0]); // заполняем First
                    if (node.elements[0] is NonTerminal)
                    {
                        //var listsB = nodesForSupporTableFirstPlus.SelectMany(x => x.Li);
                        nodesForSupporTableFirstPlus.AddRange(GetElementByFirstPLus(node.elements[0].Name));
                    }

                }
                dict[value] = nodesForSupporTableFirstPlus;
            }
            supportTable["FirstPlus"] = dict;
        }
        public static List<Element> GetElementByFirstPLus(string name)
        {
            List<Element> elem = new List<Element>();
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
            {
                if (KeyValue.Key.Name == name)
                {
                    foreach (var node in KeyValue.Value)
                    {
                        elem.Add(node.elements[0]);
                        if (node.elements[0] is NonTerminal && node.elements[0].Name != name)
                        //if (node.elements[0] is Terminal)
                        {
                            elem.AddRange(GetElementByFirstPLus(node.elements[0].Name));
                        }
                    }
                }
            }
            return elem;
        }

        public static void Show()
        {
            Console.WriteLine("Table of Relations");
            Console.WriteLine("------------------------");
            Console.Write("   ");
            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in tableOfRelation)
            {
                Console.Write("{0,2} ", KeyValue.Key);
            }
            Console.WriteLine();
            foreach (KeyValuePair<string, Dictionary<string, string>> KeyValue in tableOfRelation)
            {
                Console.Write("{0,2} ", KeyValue.Key);
                foreach (KeyValuePair<string, string> KeyValue1 in KeyValue.Value)
                {
                    foreach (var item in KeyValue1.Value)
                    {
                        Console.Write("{0,2} ", item);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------");
        }
    }
}
