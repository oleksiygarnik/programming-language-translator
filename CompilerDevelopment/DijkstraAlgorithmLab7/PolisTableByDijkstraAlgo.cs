using CompilerDevelopment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerDevelopment.DijkstraAlgorithmLab7
{

    public class RawDijkstra
    {
        public int Step { get; set; }

        public string Stack { get; set; }

        public string InputToken { get; set; }

        public List<string> Polis { get; set; }

        public int? CycleIndication { get; set; }

        public string CycleParameterVariable { get; set; }

    }

    public class TableOfWorkingCells
    {
        public static int CounterOfWorkingCells { get; set; } = 1;

        public static Dictionary<int, (string, float)> tableOfWorkingCells { get; set; } = new Dictionary<int, (string, float)>();

        //public static string GenerateNewCell()
        //{
        //    if (tableOfWorkingCells.Count == 0)
        //    {
        //        tableOfWorkingCells.Add(1, "r1");
        //    }
        //    else
        //    {
        //        int lastElementInTable = tableOfWorkingCells.Count;
        //        tableOfWorkingCells.Add(lastElementInTable + 1, "r" + (lastElementInTable + 1));
        //    }
        //    return tableOfWorkingCells.Last().Value; // возвращаю название метки
        //}

        public static string GenerateNewCell()
        {
            if (CounterOfWorkingCells == 0)
            {
                tableOfWorkingCells.Clear();
                CounterOfWorkingCells = 1;
            }
                if (tableOfWorkingCells.Count == 0)
                {
                    tableOfWorkingCells.Add(1, ("r1", 0));
                }
                else
                {
                        int lastElementInTable = tableOfWorkingCells.Count;
                        tableOfWorkingCells.Add(lastElementInTable + 1, ("r" + (lastElementInTable + 1), 0));
            }
            return tableOfWorkingCells.Last().Value.Item1; // возвращаю название метки
        }

        public static bool TokenIsContained(string token)
        {
            if (tableOfWorkingCells.Count > 0)
            {
                foreach (KeyValuePair<int, (string, float)> KeyValue in tableOfWorkingCells)
                {
                    if (KeyValue.Value.Item1 == token)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public static class PolisTableByDijkstraAlgo
    {
      

        public static int? CycleIndication { get; set; } = null;

        public static string CycleParameterVariable { get; set; } = null;

        public static List<RawDijkstra> polisTableByDijkstraAlgo = new List<RawDijkstra>();

        public static Dictionary<int, List<string>> operationsTable = new Dictionary<int, List<string>>();

        public static void LoadingOperationsTable()
        {
            XDocument xDocument = XDocument.Load("Operations.xml");
            XElement root = xDocument.Element("operationsTable");


            int prior = 0;
            List<string> operations = null;

         
            foreach (var link in root.Elements())
            {
                foreach (var side in link.Elements())
                {
                    if (side.Name == "prior")
                    {
                        prior = Convert.ToInt32(side.Value);
                    }
                    else
                    {
                        operations = new List<string>();

                        foreach (var li in side.Elements())
                        {
                            operations.Add(li.Value);
                        }
                    }
                }
                operationsTable[prior] = operations;
            }

        }

        public static void LoadingPolisTableByDijkstraAlgorithm()
        {
            //Configuration before Dijkstra Algorithm
            int step = 0;

            //закидываем в стек первый элемент
            Stack<string> tokenStack = new Stack<string>();

            string inputToken = null;

            List<string> polis = new List<string>();
            List<string> tempPolis = new List<string>(polis);
            RawDijkstra raw = new RawDijkstra()
            {
                Step = step++,
                Stack = string.Join(" ", tokenStack),
                InputToken = inputToken,
                //Polis = string.Join(" ", polis)
                Polis = tempPolis
            };

            polisTableByDijkstraAlgo.Add(raw);

            //NEXT ALGO for creating Polis by Dijkstra Algorithm

            for(int i = 0; i < SourceTableOfTokens.SourceListOfTokens.Count; i++)
            {

                inputToken = SourceTableOfTokens.SourceListOfTokens[i].View;


                CheckInputToken(inputToken, ref polis, ref tokenStack, i);

             

                List<string> tmpPolis = new List<string>(polis);

                RawDijkstra raw1 = new RawDijkstra()
                {
                    Step = step++,
                    Stack = string.Join(" ", tokenStack),
                    InputToken = inputToken,
                    Polis = tmpPolis,
                    CycleIndication = CycleIndication,
                    CycleParameterVariable = CycleParameterVariable
                };

                polisTableByDijkstraAlgo.Add(raw1);

            }

            while (tokenStack.Count != 0)
            {
                polis.Add(tokenStack.Pop());
            }

            List<string> tmpPolis1 = new List<string>(polis);

            RawDijkstra raw2 = new RawDijkstra()
            {
                Step = step++,
                Stack = string.Join(" ", tokenStack),
                InputToken = null,
                Polis = tmpPolis1,
                CycleIndication = CycleIndication,
                CycleParameterVariable = CycleParameterVariable
            };

            polisTableByDijkstraAlgo.Add(raw2);
        }
    
        public static void CheckInputToken(string inputToken, ref List<string> polis, ref Stack<string> tokenStack, int numberOfElement)
        {
            //нужно проверить на символ присвоения и добавлять значения
            if(inputToken == "=")
            {
                string value = polis[polis.Count() - 1];
                string idn = polis[polis.Count() - 2];
                for(int i = 0; i < TableOfIdentifiers.IdentifierListOfTokens.Count(); i++)
                {
                    if(idn == TableOfIdentifiers.IdentifierListOfTokens[i].View)
                    {
                        TableOfIdentifiers.IdentifierListOfTokens[i].Value = float.Parse(value);
                    }
                }
            }
            // если входящий символ идентификатор или константа, то идет на выход в полис
            //удаляем со входящей строки
            if(TableOfIdentifiers.TokenIsContained(inputToken) || TableOfConstants.TokenIsContained(inputToken))
            {
                polis.Add(inputToken);
            }
            //проверяем входящий элемент и проверяем является ли он арифметической операцией
            if(operationsTable.SelectMany(a=>a.Value).Contains(inputToken))
            {
                //если стек пустой(магазин), то заносим в стек 
                if (tokenStack.Count == 0)
                {
                    if(inputToken == ";")
                    {
                        CheckStack(inputToken, ref tokenStack, ref polis);
                    }
                    //проверка на унарный минус
                    if (inputToken == "-")
                    {
                        tokenStack.Push("@");
                    }
                    else
                    {
                        CheckStack(inputToken, ref tokenStack, ref polis);
                        //tokenStack.Push(inputToken);
                    }
                }
                else
                {
                    //если знак -, нужно проверить предыдущий символ, чтобы определить унарный или бинарный минус
                    if (inputToken == "-")
                    {
                        // проверка на унарный минус
                        if(TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[numberOfElement-1].View) 
                            || TableOfConstants.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[numberOfElement-1].View) 
                            || SourceTableOfTokens.SourceListOfTokens[numberOfElement-1].View == ")")
                        {
                            CheckStack(inputToken, ref tokenStack, ref polis);
                        }
                        else
                        {
                            CheckStack("@", ref tokenStack, ref polis); 
                        }
                    }
                    else
                    {
                        CheckStack(inputToken, ref tokenStack, ref polis);
                    }
                    //tokenStack.Push(inputToken);
                }
            }
            else
            {
                if ((inputToken == ";" || inputToken == "{") && tokenStack.Peek() == "int")
                {
                    tokenStack.Pop();
                    polis.Add("IVD");
                }
                else if ((inputToken == ";" || inputToken == "{") && tokenStack.Peek() == "float")
                {
                    tokenStack.Pop();
                    polis.Add("FVD");
                }


            }

        }
    
        public static int FindPrior(string inputOperation)
        {
            if (inputOperation.Contains("if"))
            {
                inputOperation = "if";
            }
            int priorOfInputToken = 0;
            foreach (KeyValuePair<int, List<string>> KeyValue in operationsTable)
            {
                foreach (var operation in KeyValue.Value)
                {
                    if(operation == inputOperation)
                    {
                        priorOfInputToken = KeyValue.Key;
                        return priorOfInputToken;
                    }
                }
            }
            return priorOfInputToken;
        }

        public static void CheckStack(string inputToken, ref Stack<string> tokenStack, ref List<string> polis)
        {
            bool finish = false;
            int priorCurrent = 0;
            int priorLastInStack = 0;
            if(inputToken == ";" && tokenStack.Count == 0)
            {
                finish = true;
            }
            if(inputToken == "(")
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "[")
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "if")
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "for")
            {
                //нужно будет перегрузить метод
                CycleIndication = 1;
                tokenStack.Push(inputToken + " "+ TableOfLabels.GenerateNewLabel() + " "+ TableOfLabels.GenerateNewLabel() + " " + TableOfLabels.GenerateNewLabel());
                finish = true;
            }
            else if(inputToken == "int") // Int Variable declaration
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "float") // Float Variable declaration
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "cout") // OUT
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            else if(inputToken == "cin") // IN
            {
                tokenStack.Push(inputToken);
                finish = true;
            }
            while (finish != true)
            {
                priorCurrent = FindPrior(inputToken);
                if (tokenStack.Count == 0)
                {
                    if(inputToken == ";")
                    {
                        finish = true;
                        break;
                    }
                    tokenStack.Push(inputToken);
                    finish = true;
                    break;
                }
                else
                {
                    priorLastInStack = FindPrior(tokenStack.Peek());
                }
                //проверка на скобки
                {
                    if (inputToken == ")" && tokenStack.Peek() == "(")
                    {
                        tokenStack.Pop();
                        break;
                    }
                    if (inputToken == "]" && tokenStack.Peek() == "[")
                    {
                        tokenStack.Pop();
                        break;
                    }

                    





                    //УСЛОВНЫЙ ОПЕРАТОР







                    if (inputToken == ";" && tokenStack.Peek() == "if")
                    {
                        //тут нужно сгенирировать метку и добавить ее до if
                        string tmp = tokenStack.Pop();
                        tokenStack.Push(tmp + " " +TableOfLabels.GenerateNewLabel());

                        string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();
                        // а в полиз нужно добавить название метки + УПХ. Например: m1 УПХ
                        polis.Add(words.First());
                        polis.Add("УПХ");
                        break;
                    }
                    if (inputToken == "else" && tokenStack.Peek().Contains("if"))
                    {
                 
                        //генерируется новая метка например было if m1, а стало if m1 m2
                        string tmp = tokenStack.Pop();
                        tokenStack.Push(tmp + " " +TableOfLabels.GenerateNewLabel());

                        string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();
                        //в полиз нужно добавить тогда m2 БП m1:
                        //polis.Add(words[1] + "БП" + words.First() + ":"); //пока что хз как оно будет работать, но гамнокодим
                        polis.Add(words[1]);
                        polis.Add("БП");
                        polis.Add(words.First() + ":");
                        TableOfLabels.UpdateTable(words.First(), polis);
                        break;
                    }
                    if(inputToken == "endif" && tokenStack.Peek().Contains("if"))
                    {

                        string tmpList = tokenStack.First();
                        string[] words = tmpList.Split(new char[] { ' ' });
                        polis.Add(words.Last() + ":");
                        TableOfLabels.UpdateTable(words.Last(), polis);
                        tokenStack.Pop();
                        break;
                    }




                    // ЦИКЛ з правильним порядком






                    if(inputToken == "=" && CycleIndication == 1)
                    {
                        CycleParameterVariable = polis[polis.Count - 1]; // записываем последний со стека в параметр цикла и дальше скидываем признак цикла до 0
                        CycleIndication = 0;
                    }
                    //if (inputToken == "step" && tokenStack.Peek().Contains("for"))
                    //{
                    //    string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                    //    polis.Add(TableOfWorkingCells.GenerateNewCell() + " 1 =" + words.First() + ": " + TableOfWorkingCells.GenerateNewCell());
                    //    break;
                    //}
                    //if (inputToken == "to" && tokenStack.Peek().Contains("for"))
                    //{
                    //    string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();
                    //    string secondElem = words[1];

                    //    polis.Add("= " + TableOfWorkingCells.tableOfWorkingCells.First().Value + " 0 = " + secondElem + " " + "УПХ" + CycleParameterVariable + CycleParameterVariable + TableOfWorkingCells.tableOfWorkingCells.Last().Value + "+ = "
                    //        + secondElem + ": " + TableOfWorkingCells.tableOfWorkingCells.First().Value + " 0 = " + CycleParameterVariable);
                    //    break;
                    //}
                    //if (inputToken == "do" && tokenStack.Peek().Contains("for"))
                    //{
                    //    TableOfWorkingCells.CounterOfWorkingCells = 0;
                    //    string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                    //    polis.Add("- " + TableOfWorkingCells.tableOfWorkingCells.Last().Value + "* 0 <= " + words[2] + " УПХ");
                    //    break;
                    //}
                    //if (inputToken == "next" && tokenStack.Peek().Contains("for"))
                    //{
                    //    string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                    //    polis.Add(words.First() + " БП " + words[2] + ":");
                    //    tokenStack.Pop();
                    //    break;
                    //}



                    //Цикл з неправильним порядком параметрів

                    if (inputToken == "to" && tokenStack.Peek().Contains("for"))
                    {
                        string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                        polis.Add(TableOfWorkingCells.GenerateNewCell());
                        polis.Add("1");
                        polis.Add("=");
                        polis.Add(words[0] + ":"); //mi
                        TableOfLabels.UpdateTable(words[0], polis);

                        TableOfWorkingCells.GenerateNewCell();
                        polis.Add(TableOfWorkingCells.GenerateNewCell());

                        break;
                    }
                    if (inputToken == "step" && tokenStack.Peek().Contains("for"))
                    {
                        polis.Add("=");
                        polis.Add(TableOfWorkingCells.tableOfWorkingCells[2].Item1);
                        break;
                    }

                    if (inputToken == "do" && tokenStack.Peek().Contains("for"))
                    {
                        string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                        polis.Add("=");
                        polis.Add(TableOfWorkingCells.tableOfWorkingCells.First().Value.Item1);
                        polis.Add("0");
                        polis.Add("==");
                        polis.Add(words[1]); //mi+1
                        polis.Add("УПХ");
                        polis.Add(CycleParameterVariable);
                        polis.Add(CycleParameterVariable);
                        polis.Add(TableOfWorkingCells.tableOfWorkingCells[2].Item1);
                        polis.Add("+");
                        polis.Add("=");
                        polis.Add(words[1] + ":"); //mi+1
                        TableOfLabels.UpdateTable(words[1], polis);

                        polis.Add(TableOfWorkingCells.tableOfWorkingCells.First().Value.Item1);
                        polis.Add("0");
                        polis.Add("=");
                        polis.Add(CycleParameterVariable);
                        polis.Add(TableOfWorkingCells.tableOfWorkingCells[3].Item1);
                        polis.Add("-");
                        polis.Add(TableOfWorkingCells.tableOfWorkingCells[2].Item1);
                        polis.Add("*");
                        polis.Add("0");
                        polis.Add("<=");
                        polis.Add(words[2]); //mi+2
                        polis.Add("УПХ");


                        TableOfWorkingCells.CounterOfWorkingCells = 0;
                        break;
                    }
                    if (inputToken == "next" && tokenStack.Peek().Contains("for"))
                    {
                        string[] words = tokenStack.Peek().Split(new char[] { ' ' }).Skip(1).ToArray();

                        //polis.Add(words.First() + " БП " + words[2] + ":");
                        polis.Add(words.First());
                        polis.Add("БП");
                        polis.Add(words[2] + ":");
                        TableOfLabels.UpdateTable(words[2], polis);

                        tokenStack.Pop();
                        break;
                    }








                    //ОБЬЯВЛЕНИЕ ПЕРЕМЕННЫХ 




                    if (inputToken == ",")
                    {
                        break;
                    }
            
                    if((inputToken == ";" || inputToken == "{") && tokenStack.Peek() == "int")
                    {
                        tokenStack.Pop();
                        polis.Add("IVD");
                        break;
                    }
                    if ((inputToken == ";" || inputToken == "{") && tokenStack.Peek() == "float")
                    {
                        tokenStack.Pop();
                        polis.Add("FVD");
                        break;
                    }


                   
                    // ОПЕРАТОР ВВОДА И ВЫВОДА



                    if(inputToken == "<<" || inputToken == ">>")
                    {
                        break;
                    }
                    if(inputToken == ";" && tokenStack.Peek() == "cin")
                    {
                        tokenStack.Pop();
                        polis.Add("IN");
                        break;
                    }
                    if(inputToken == ";" && tokenStack.Peek() == "cout")
                    {
                        tokenStack.Pop();
                        polis.Add("OUT");
                        break;
                    }

                }
                if (priorCurrent > priorLastInStack)
                {
                    if (inputToken == ";" )
                    {
                        finish = true;
                    }
                    else
                    {
                        tokenStack.Push(inputToken);
                        finish = true;
                    }
                }
                else
                {               
                     polis.Add(tokenStack.Pop());
                }
            } 
        }
    }
}
