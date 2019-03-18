using CompilerDevelopment.Entities;
using CompilerDevelopment.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis.SyntaxAnalyzerForUpstreamAnalysis
{
    public class MiniToken
    {
        public int Row { get; set; }
        public string Name { get; set; }

        public MiniToken(int Row, string Name)
        {
            this.Name = Name;
            this.Row = Row;
        }
    }

    public class Raw
    {
        public int Step { get; set; }

        public string TokenStack { get; set; }

        public string Sign { get; set; }

        public List<MiniToken> TokenQueue { get; set; }

        public List<string> Polis { get; set; }

    }

    public class RawForCalculate
    {
        public int Step { get; set; }

        public string Stack { get; set; }

        public string Polis { get; set; }
    }


    static class TableOfCalculationExpression
    {
        public static List<RawForCalculate> tableOfCalculationExpression = new List<RawForCalculate>();

        public static string CalculationOfExpression()
        {
            int step = 0;
            //закидываем в стек первый элемент
            Stack<string> tokenStack = new Stack<string>();

            Queue<string> tokenQueue = new Queue<string>();
            var list = TableOfUpstreamParsing.tableOfUpstreamParsing.Last().Polis;

            for (int i = 0; i < list.Count(); i++)
            {
                tokenQueue.Enqueue(list[i].ToString());
                //tokenQueue.Enqueue(SourceTableOfTokens.SourceListOfTokens[i].View);
            }
            RawForCalculate raw = new RawForCalculate()
            {
                Step = step++,
                Stack = string.Join(" ", tokenStack),
                Polis = string.Join(" ", tokenQueue)
            };

            tableOfCalculationExpression.Add(raw);
            //NEXT ALGO for solution

            double result = 0;
            List<double> listOfNumbers = new List<double>();
            while (tokenQueue.Count != 0)
            {

 
                string firstQueueElem = tokenQueue.Peek();
                if(TableOfConstants.TokenIsContained(firstQueueElem))
                {
                    tokenStack.Push(tokenQueue.Dequeue());
                    listOfNumbers.Add(Double.Parse(firstQueueElem));
                }
                else if (TableOfIdentifiers.TokenIsContained(firstQueueElem))
                {
                    foreach (KeyValuePair<string, string> KeyValue in Storage.valueForIdentifiers)
                    {
                        if(KeyValue.Key == firstQueueElem)
                        {
                            if (KeyValue.Value == null || KeyValue.Value == string.Empty)
                            {
                                return "Please, enter value";
                            }
                            listOfNumbers.Add(Double.Parse(KeyValue.Value));
                        }
                    }
                    tokenStack.Push(tokenQueue.Dequeue());
                }
                else if(firstQueueElem=="@")
                {
                    //if(TableOfConstants.TokenIsContained(tokenStack.Peek()) || TableOfIdentifiers.TokenIsContained(tokenStack.Peek()))
                    //{

                    //}
                    double lastElem = listOfNumbers.Last() - 2 * listOfNumbers.Last();
                    listOfNumbers[listOfNumbers.Count - 1] = lastElem;
                
                    string tmp = tokenStack.Pop();
                    tokenStack.Push("(-" + tmp + ")");
                    tokenQueue.Dequeue();
                }
                else
                {
                    string tmp1 = tokenStack.Pop();
                    string tmp2 = tokenStack.Pop();
                    tokenStack.Push("(" + tmp2 + firstQueueElem + tmp1 + ")");
                    tokenQueue.Dequeue();
                }

                switch (firstQueueElem)
                {
                    case "-":
                        listOfNumbers[listOfNumbers.Count - 2] = listOfNumbers[listOfNumbers.Count - 2] - listOfNumbers[listOfNumbers.Count - 1];
                        listOfNumbers.RemoveAt(listOfNumbers.Count - 1);
                        break;
                    case "+":
                        listOfNumbers[listOfNumbers.Count - 2] = listOfNumbers[listOfNumbers.Count - 2] + listOfNumbers[listOfNumbers.Count - 1];
                        listOfNumbers.RemoveAt(listOfNumbers.Count - 1);
                        break;
                    case "*":
                        listOfNumbers[listOfNumbers.Count - 2] = listOfNumbers[listOfNumbers.Count - 2] * listOfNumbers[listOfNumbers.Count - 1];
                        listOfNumbers.RemoveAt(listOfNumbers.Count - 1);
                        break;
                    case "/":
                        if(listOfNumbers[listOfNumbers.Count-1]== 0)
                        {
                            return "Ошибка, деление на 0";
                        }
                        listOfNumbers[listOfNumbers.Count - 2] = listOfNumbers[listOfNumbers.Count - 2] / listOfNumbers[listOfNumbers.Count - 1];
                        listOfNumbers.RemoveAt(listOfNumbers.Count - 1);
                        break;
                }

                //else if(firstQueueElem == "-")
                //{
                //    listOfNumbers[listOfNumbers.Count - 1] = listOfNumbers[listOfNumbers.Count - 2] - listOfNumbers[listOfNumbers.Count - 1];
                //}
                if (listOfNumbers.Count > 0)
                {
                    result = listOfNumbers.First();
                }

                RawForCalculate raw1 = new RawForCalculate()
                {
                    Step = step++,
                    Stack = string.Join(" ", tokenStack),
                    Polis = string.Join(" ", tokenQueue)
                };

                tableOfCalculationExpression.Add(raw1);

            }
            return result.ToString();
        }
    }

    static class TableOfUpstreamParsing
    {

        public static List<Raw> tableOfUpstreamParsing = new List<Raw>();

     
     

        //public static string Loading2()
        //{
        //    int step = 0;
        //    //закидываем в стек первый элемент
        //    Stack<string> tokenStack = new Stack<string>();

        //    Queue<MiniToken> tokenQueue = new Queue<MiniToken>();

        //    for (int i = 0; i < SourceTableOfTokens.SourceListOfTokens.Count; i++)
        //    {
        //        MiniToken miniToken = new MiniToken(SourceTableOfTokens.SourceListOfTokens[i].Row, SourceTableOfTokens.SourceListOfTokens[i].View);
        //        tokenQueue.Enqueue(miniToken);
        //        //tokenQueue.Enqueue(SourceTableOfTokens.SourceListOfTokens[i].View);
        //    }
        //    MiniToken miniTokenLast = new MiniToken(0, "#");
        //    tokenQueue.Enqueue(miniTokenLast);
        //    //tokenQueue.Enqueue("#");

        //    tokenStack.Push("#");


        //    //NEXT
        //    string sign;
        //    string basis = null;
        //    while (tokenQueue.Count != 0)
        //    {

        //         sign = CheckSign(tokenStack.Peek(), tokenQueue.Peek().Name);

        //        if (sign == null)
        //        {
        //            int row = tokenQueue.Peek().Row + 1;
        //            return "Найдена ошибка в " + tokenQueue.Peek().Row + "-" + row + "рядке!";
        //        }

        //        Raw raw = new Raw()
        //        {
        //            Step = step++, 
        //            TokenStack = string.Join(" ", tokenStack),
        //            Sign = sign,
        //            TokenQueue = tokenQueue.ToList()
        //        };

        //        tableOfUpstreamParsing.Add(raw);

        //        if (sign == "<" || sign == "=")
        //        {
        //            tokenStack.Push(tokenQueue.Dequeue().Name);
        //        }
        //        else if (sign == ">")
        //        {
        //            PushElem(ref tokenStack, ref step, ref tokenQueue, ref sign);
        //        }

        //        if (tokenStack.Contains("programm"))
        //        {
        //            Raw raw1 = new Raw()
        //            {
        //                Step = step++,
        //                TokenStack = string.Join(" ", tokenStack),
        //                Sign = sign,
        //                TokenQueue = tokenQueue.ToList()
        //            };
        //            tableOfUpstreamParsing.Add(raw1);
        //            return "Успешно пройден синтаксический анализатор";
        //            break;
        //        }

        //    }
        //    return null;

        //}

        public static string LoadingForLab6()
        {
            int step = 0;
            //закидываем в стек первый элемент
            Stack<string> tokenStack = new Stack<string>();

            Queue<MiniToken> tokenQueue = new Queue<MiniToken>();

            for (int i = 0; i < SourceTableOfTokens.SourceListOfTokens.Count; i++)
            {
                MiniToken miniToken = new MiniToken(SourceTableOfTokens.SourceListOfTokens[i].Row, SourceTableOfTokens.SourceListOfTokens[i].View);
                tokenQueue.Enqueue(miniToken);
                //tokenQueue.Enqueue(SourceTableOfTokens.SourceListOfTokens[i].View);
            }
            MiniToken miniTokenLast = new MiniToken(0, "#");
            tokenQueue.Enqueue(miniTokenLast);
            //tokenQueue.Enqueue("#");

            tokenStack.Push("#");


            //NEXT
            string sign;
            string basis = null;
            List<string> polis = new List<string>();
            while (tokenQueue.Count != 0)
            {

                sign = CheckSign(tokenStack.Peek(), tokenQueue.Peek().Name);

                if (sign == null)
                {
                    int row = tokenQueue.Peek().Row + 1;
                    return "Найдена ошибка в " + tokenQueue.Peek().Row + "-" + row + "рядке!";
                }
                List<string> polisTmp = new List<string>(polis);
                //for(int i = 0; i < polis.Count; i++)
                //{
                //    polisTmp[i] = polis[i];
                //}
                Raw raw = new Raw()
                {
                    Step = step++,
                    TokenStack = string.Join(" ", tokenStack),
                    Sign = sign,
                    TokenQueue = tokenQueue.ToList(),
                    Polis = polisTmp
                };
              
                tableOfUpstreamParsing.Add(raw);

                //polis = new List<string>();

                if (sign == "<" || sign == "=")
                {
                    tokenStack.Push(tokenQueue.Dequeue().Name);
                }
                else if (sign == ">")
                {
                    PushElem(ref tokenStack, ref step, ref tokenQueue, ref sign, ref polis);
                }

                // if (tokenStack.Contains("programm"))

                if (tokenStack.Contains("E1") && tokenStack.Count == 2)
                {
                    if(tokenQueue.Count>1)
                    {
                        int row1 = tokenQueue.Peek().Row + 1;
                        return "Найдена ошибка в " + tokenQueue.Peek().Row + "-" + row1 + "рядке!";
                    }
                    Raw raw1 = new Raw()
                    {
                        Step = step++,
                        TokenStack = string.Join(" ", tokenStack),
                        Sign = sign,
                        TokenQueue = tokenQueue.ToList(), 
                        Polis = polis
                    };
                    tableOfUpstreamParsing.Add(raw1);
                    return "Успешно пройден синтаксический анализатор";
                    break;
                }

            }
            return null;

        }

        public static void PushElem(ref Stack<string> tokenStack, ref int step, ref Queue<MiniToken> tokenQueue, ref string sign, ref List<string> polis)
        {
            Node node1 = new Node();
            int count = tableOfUpstreamParsing.Count() - 1;

            //tokenStack.Reverse().ToArray();

            for (int i = 0; i < tokenStack.Count(); i++)
            {
                string sign1 = CheckSign(tokenStack.ToArray()[i + 1], tokenStack.ToArray()[i]);
                if (sign1 == "<")
                {
                    while ((i + 1) != 0)
                    {
                        Terminal elem = new Terminal(tokenStack.ToArray()[i]); //???????????????????????????????/
                        node1.elements.Add(elem);
                        i--;
                    }
                    break;
                }

            }

            string tmp_name = null;
            for (int i = 0; i < node1.elements.Count; i++)
            {
                if (TableOfIdentifiers.TokenIsContained(node1.elements[i].Name))
                {
                    tmp_name = node1.elements[i].Name;
                    node1.elements[i].Name = "idn";
                }
                if(TableOfConstants.TokenIsContained(node1.elements[i].Name))
                {
                    tmp_name = node1.elements[i].Name;
                    node1.elements[i].Name = "con";
                }
            }
            string replaceElem = null;
            //нужно заменить основу на нетерминал
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in Grammar.MyGrammar)
            {
                foreach (var node in KeyValue.Value)
                {
                    if (node.Equals(node1))
                    {
                        if(node1.elements[0].Name == "idn" || node1.elements[0].Name == "con")
                        {
                            polis.Add(tmp_name);
                        }
                        else if(node.SemanticSubProgramm!="none")
                        {
                            polis.Add(node.SemanticSubProgramm);
                        }
                        replaceElem = KeyValue.Key.Name;
                        break;
                    }
                    if (replaceElem != null)
                    {
                        break;
                    }
                }
                if (replaceElem != null)
                {
                    break;
                }
            }

            int counter = node1.elements.Count;
            while (counter != 0)
            {
                tokenStack.Pop();
                counter--;
            }
            string signBeforePush = CheckSign(tokenStack.Peek(), replaceElem);

            if (signBeforePush == ">")
            {
                PushElem(ref tokenStack, ref step, ref tokenQueue, ref sign, ref polis);
            }
            //tokenStack.Pop();
             tokenStack.Push(replaceElem);

            replaceElem = null;


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
            if(TableOfIdentifiers.TokenIsContained(first))
            {
                first = "idn";
            }
            if(TableOfIdentifiers.TokenIsContained(second))
            {
                second = "idn";
            }
            if(TableOfConstants.TokenIsContained(first))
            {
                first = "con";
            }
            if(TableOfConstants.TokenIsContained(second))
            {
                second = "con";
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

//basis = basis.Trim();
//if (TableOfIdentifiers.TokenIsContained(basis))
//{
//    basis = "idn";
//}




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