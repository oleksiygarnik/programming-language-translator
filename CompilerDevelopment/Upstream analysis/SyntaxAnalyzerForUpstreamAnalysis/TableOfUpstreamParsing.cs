using CompilerDevelopment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis.SyntaxAnalyzerForUpstreamAnalysis
{
    static class TableOfUpstreamParsing
    {

        public static List<Raw> tableOfUpstreamParsing = new List<Raw>();

        public class Raw
        {
            public int Step { get; set; }

            public string TokenStack { get; set; }

            public string Sign { get; set; }

            public string TokenQueue { get; set; }

        }

        public static void Loading2()
        {
            Int32 step = 0;
            //закидываем в стек первый элемент
            Stack<string> tokenStack = new Stack<string>();

            Queue<string> tokenQueue = new Queue<string>();

            for (int i = 0; i < SourceTableOfTokens.SourceListOfTokens.Count; i++)
            {
                tokenQueue.Enqueue(SourceTableOfTokens.SourceListOfTokens[i].View);
            }

            tokenQueue.Enqueue("#");

            tokenStack.Push("#");


            //// проверяем, что за знак
            //string sign = CheckSign(tokenStack.Peek(), tokenQueue.Peek());

            //Raw raw1 = new Raw()
            //{
            //    Step = step++,
            //    TokenStack = string.Join(", ", tokenStack),
            //    Sign = sign,
            //    TokenQueue = string.Join(", ", tokenQueue)
            //};

            //tableOfUpstreamParsing.Add(raw1);
            //// следующая итерация, проверяем на знак, чтобы понять, какой будет дальше стек и очередь
            //if(sign == "<" || sign == "=")
            //{
            //    tokenStack.Push(tokenQueue.Dequeue());
            //}
            //else if(sign == ">")
            //{
            //    int count = tableOfUpstreamParsing.Count() - 1;

            //    //идем в обратном порядке и ищем где был знак меньше
            //    for(int i = count; i >=0; i--)
            //    {
            //        if(tableOfUpstreamParsing[i].Sign == "<")
            //        {
            //            string[] words = tableOfUpstreamParsing[i].TokenQueue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //            string basis = tableOfUpstreamParsing[i].TokenQueue;
            //        }
            //    }
            //    //тут будет щас логика
            //}

            //NEXT
            string sign;
            string basis = null;
            while (tokenQueue.Count != 1)
            {

                sign = CheckSign(tokenStack.Peek(), tokenQueue.Peek());
        

                Raw raw = new Raw()
                {
                    Step = step++,
                    TokenStack = string.Join(", ", tokenStack),
                    Sign = sign,
                    TokenQueue = string.Join(", ", tokenQueue)
                };

                tableOfUpstreamParsing.Add(raw);

                if (sign == "<" || sign == "=")
                {
                    tokenStack.Push(tokenQueue.Dequeue());
                }
                else if (sign == ">")
                {
                    Node node1 = new Node();
                    int count = tableOfUpstreamParsing.Count() - 1;

                    tokenStack.Reverse().ToArray();

                    for(int i = 0; i < tokenStack.Count(); i++)
                    {    
                        string sign1 = CheckSign(tokenStack.ToArray()[i+1], tokenStack.ToArray()[i]);
                        if (sign1 == "<")
                        {
                            while ((i+1)!=0)
                            {
                                Terminal elem = new Terminal(tokenStack.ToArray()[i]); //???????????????????????????????/
                                node1.elements.Add(elem);

                                basis += tokenStack.ToArray()[i] + " ";
                                i--;
                            }
                            break;
                        }
                    }

                    //////идем в обратном порядке и ищем где был знак меньше
                    ////for (int i = count; i >= 0; i--)
                    ////{
                    ////    if (tableOfUpstreamParsing[i].Sign == "<")
                    ////    {
                    ////        string[] words = tableOfUpstreamParsing[i].TokenQueue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    ////        for (int j = 0; j < words.Count(); j++)
                    ////        {
                    ////            words[j] = words[j].Trim();
                    ////            if (words[j] != tokenQueue.Peek())
                    ////            {
                    ////                basis += words[j] + " ";
                    ////                string lol = tokenQueue.Peek();
                    ////            }
                    ////            else
                    ////            {
                    ////                break;
                    ////            }
                    ////        }
                    ////        if (basis != null)
                    ////        {
                    ////            break;
                    ////        }
                    ////    }
                    ////}



                    basis = basis.Trim();
                    if(TableOfIdentifiers.TokenIsContained(basis))
                    {
                        basis = "idn";
                    }
                    for(int i = 0; i < node1.elements.Count; i++)
                    {
                        if(TableOfIdentifiers.TokenIsContained(node1.elements[i].Name))
                        {
                            node1.elements[i].Name = "idn";
                        }
                    }
                    string replaceElem = null;
                    //нужно заменить основу на нетерминал
                    foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
                    {
                        foreach (var node in KeyValue.Value)
                        {
                            //foreach (var elem in node.elements)
                            //{
                                if(node.Equals(node1))
                                //if (elem.Name == basis)
                                {
                                    replaceElem = KeyValue.Key.Name;
                                    break;
                                }
                                if (replaceElem != null)
                                {
                                    break;
                                }
                           // }
                        }
                        if (replaceElem != null)
                        {
                            break;
                        }
                    }

                    basis = null;
                    int counter = node1.elements.Count;
                    while(counter!=0)
                    {
                        tokenStack.Pop();
                        counter--;
                    }
                    //tokenStack.Pop();
                    tokenStack.Push(replaceElem);
                    replaceElem = null;



                        //string NewSign = CheckSign(tokenStack.Pop(), tokenStack.Peek());
                        //тут будет щас логика
                    }


            }

        }


        //public static void Loading()
        //{
        //    // начало счетчика
        //    int step = 0;
        //    //закидываем в стек первый элемент
        //    Stack<string> tokenStack = new Stack<string>();

        //    Queue<string> tokenQueue = new Queue<string>();

        //    for (int i = 0; i < SourceTableOfTokens.SourceListOfTokens.Count; i++)
        //    {
        //        tokenQueue.Enqueue(SourceTableOfTokens.SourceListOfTokens[i].View);
        //    }

        //    tokenQueue.Enqueue("#");

        //    tokenStack.Push("#");
        //    Raw raw1 = new Raw()
        //    {
        //        Step = step++,
        //        TokenStack = tokenStack,
        //        Sign = "<",
        //        TokenQueue = tokenQueue
        //    };


        //    tableOfUpstreamParsing.Add(raw1);

        //    tokenStack.Push(tokenQueue.Dequeue());


        //    while (tokenQueue.Count != 0)
        //    {
        //        string sign = CheckSign(tokenStack.Peek(), tokenQueue.Peek());

        //        if(sign == "<" || sign == "=")
        //        {
        //            tokenStack.Push(tokenQueue.Dequeue());
        //        }
        //        else if(sign == ">")
        //        {
        //            int i = tableOfUpstreamParsing.Count() - 1;

        //            tableOfUpstreamParsing.Where(r => r.Sign == "<");
        //            while (tableOfUpstreamParsing[i].Sign != "<")
        //            {
        //                i--;
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Ошибка синтаксического анализатора. Данного отношения не существует!");
        //        }
        //        Raw raw = new Raw()
        //        {
        //            Step = step++,
        //            TokenStack = tokenStack,
        //            Sign = sign,
        //            TokenQueue = tokenQueue
        //        };
        //        tableOfUpstreamParsing.Add(raw);
        //    }
        //}


        public static string CheckSign(string first, string second)
        {
            foreach(var a  in Grammar.MyGrammar)
            {

            }

            if(TableOfIdentifiers.TokenIsContained(first))
            {
                first = "idn";
            }
            if(TableOfIdentifiers.TokenIsContained(second))
            {
                second = "idn";
            }
            //нада добавить проверку на константы
            foreach(KeyValuePair< string, Dictionary<string, string>> KeyValue  in TableOfRelations.tableOfRelation)
            {
                if (KeyValue.Key == first)
                {
                    foreach (KeyValuePair<string, string> KeyValueNext in KeyValue.Value)
                    {
                        if (KeyValueNext.Key == second)
                        {
                            return KeyValueNext.Value;
                        }
                    }
                }
            }
            return null;
        }



    }
}
