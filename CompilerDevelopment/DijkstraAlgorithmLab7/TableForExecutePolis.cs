using CompilerDevelopment.Entities;
using CompilerDevelopment.GUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerDevelopment.DijkstraAlgorithmLab7
{
    public class Raw
    {
        public int Step { get; set; }

        public string Command { get; set; }

        public string Action { get; set; }

        public List<string> Polis { get; set; }
    }

    public static class TableForExecutePolis
    {
        public static List<Raw> tableForExecutePolis { get; set; } = new List<Raw>();

        public static string ExecutePolis()
        {
            int step = 0;
            string displayResult = null;
            ProgrammResult programmResult = null;

            Queue<string> tokenQueue = new Queue<string>();
            //var list = TableOfUpstreamParsing.tableOfUpstreamParsing.Last().Polis;

            List<string> polis = PolisTableByDijkstraAlgo.polisTableByDijkstraAlgo.Last().Polis;

            for (int i = 0; i < polis.Count(); i++)
            {
                tokenQueue.Enqueue(polis[i].ToString());
            }

            List<string> tmpPolis = new List<string>(polis);

            Raw raw = new Raw()
            {
                Step = step++,
                Command = null,
                Action = null,
                Polis = tmpPolis
            };

            tableForExecutePolis.Add(raw);

            Stack<string> tokenStack = new Stack<string>(); //на примере арифметического выражения стек
            //List<string> tmpElem = new List<string>();
            //List<double> listOfNumbers = new List<double>();
            double result = 0;
            float numeric;

            //Next ALGO for solutin
            for (int i = 0; i < polis.Count(); i++)
            {
                string inputElem = polis[i];

                if(inputElem == "r1" || inputElem == "r2" || inputElem == "r3")
                {

                }
                foreach (KeyValuePair<int, (string, float)> KeyValue in TableOfWorkingCells.tableOfWorkingCells)
                {

                }
                if (TableOfIdentifiers.TokenIsContained(inputElem))
                {
                    //for(int j = 0; j < TableOfIdentifiers.IdentifierListOfTokens.Count(); j++)
                    //{
                    //    if(inputElem == TableOfIdentifiers.IdentifierListOfTokens[j].View)
                    //    {
                    //        tokenStack.Push(inputElem);
                    //        listOfNumbers.Add(Double.Parse(inputElem));
                    //    }
                    //}
                    tokenStack.Push(inputElem);
                }
                else if(TableOfConstants.TokenIsContained(inputElem))
                {
                    tokenStack.Push(inputElem);
                    continue;
                }
                else if(TableOfLabels.TokenIsContained(inputElem))
                {
                    tokenStack.Push(inputElem);
                }
               // else if(Regex.IsMatch(inputElem, @"\d"))
               else if(float.TryParse(inputElem, out numeric))
                {
                        tokenStack.Push(inputElem);
                        continue;
 
                }
                else if(TableOfWorkingCells.TokenIsContained(inputElem))
                {
                    tokenStack.Push(inputElem);
                }
                else if(inputElem == "УПХ")
                {
                    string label = tokenStack.Pop(); // вытягиваем со стека метку
                    if (bool.Parse(tokenStack.Pop()))
                    {
                        continue;
                    }
                    else
                    {
                        if (TableOfLabels.TokenIsContained(label))
                        {
                            foreach (KeyValuePair<int, (string, int)> KeyValue in TableOfLabels.tableOfLabels)
                            {
                                if(KeyValue.Value.Item1 == label)
                                {
                                    i = KeyValue.Value.Item2;
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if(inputElem == "БП")
                {
                    string label = tokenStack.Pop();
                    if (TableOfLabels.TokenIsContained(label))
                    {
                        foreach (KeyValuePair<int, (string, int)> KeyValue in TableOfLabels.tableOfLabels)
                        {
                            if (KeyValue.Value.Item1 == label)
                            {
                                i = KeyValue.Value.Item2;
                                i--;
                                break;
                            }
                        }
                    }
                }
                else if(inputElem == "OUT")
                {                
                    while(tokenStack.Count!=0)
                    {
                        for (int h = 0; h < TableOfIdentifiers.IdentifierListOfTokens.Count; h++)
                        {
                            if (tokenStack.Peek() == TableOfIdentifiers.IdentifierListOfTokens[h].View)
                            {
                                string text = TableOfIdentifiers.IdentifierListOfTokens[h].Value.ToString();
                                text = text.Replace(",", ".");
                                displayResult += text + "\n";
                            }
                        }
                        tokenStack.Pop();
                    }
                }
                else if (inputElem == "IN")
                {
                    while (tokenStack.Count != 0)
                    {
                        for (int h = 0; h < TableOfIdentifiers.IdentifierListOfTokens.Count; h++)
                        {
                            if (tokenStack.Peek() == TableOfIdentifiers.IdentifierListOfTokens[h].View)
                            {
                               
                                Storage.valueForIdentifiers[tokenStack.Peek()] = null; //displayResult += TableOfIdentifiers.IdentifierListOfTokens[h].Value + "\n";
                                
                            }
                        }
                        tokenStack.Pop();
                    }
                    programmResult = new ProgrammResult(displayResult);
                    if (programmResult.ShowDialog() == true)
                    {
                        displayResult = Storage.StringTemplate;
                        Storage.valueForIdentifiers.Clear();
                        Storage.End = false;
                        continue;
                    }
                    else
                    {
                        Storage.End = true;
                        programmResult.Close();
                        //return "Неправильно заполненные данные. Ошибка в присвоении";
                    }
                    //return displayResult;
                }

                else if (inputElem == "IVD")
                {
                    while(tokenStack.Count!=0)
                    { 
                        for(int h = 0; h < TableOfIdentifiers.IdentifierListOfTokens.Count; h++)
                        {
                            if(tokenStack.Peek() == TableOfIdentifiers.IdentifierListOfTokens[h].View)
                            {
                                TableOfIdentifiers.IdentifierListOfTokens[h].Type = "int";
                            }
                        }
                        tokenStack.Pop();
                    }
                }
                else if(inputElem == "FVD")
                {
                    while (tokenStack.Count != 0)
                    {
                        for (int h = 0; h < TableOfIdentifiers.IdentifierListOfTokens.Count; h++)
                        {
                            if (tokenStack.Peek() == TableOfIdentifiers.IdentifierListOfTokens[h].View)
                            {
                                TableOfIdentifiers.IdentifierListOfTokens[h].Type = "float";
                                
                            }
                        }
                        tokenStack.Pop();
                    }
                }
                else if (inputElem == "@")
                {
                    string lastStackElemIdentifier = tokenStack.Peek();
                    //если это идентификатор, достаем его с таблицы(его значение) и меняем на противоположное
                    if(TableOfIdentifiers.TokenIsContained(lastStackElemIdentifier))
                    {
                        for (int j = 0; j < TableOfIdentifiers.IdentifierListOfTokens.Count(); j++)
                        {
                            if (lastStackElemIdentifier == TableOfIdentifiers.IdentifierListOfTokens[j].View)
                            {
                                tokenStack.Pop();
                                tokenStack.Push((TableOfIdentifiers.IdentifierListOfTokens[j].Value - 2 * TableOfIdentifiers.IdentifierListOfTokens[j].Value).ToString());
                                //listOfNumbers.Add(Double.Parse(inputElem));
                            }
                        }
                    }
                    else // можна сделать проверку на наличие в таблице констант
                    {
                        string lastStackElemConstant = tokenStack.Pop();
                        //если элемент константа, то просто добавляем противоположное значение
                        float tmp = float.Parse(lastStackElemConstant);
                        tmp = tmp - 2 * tmp;

                        tokenStack.Push(tmp.ToString());

                    }
     
                }
                else if(inputElem == "true" || inputElem == "false" || inputElem == "<" || inputElem == "<=" || inputElem == ">"
                    || inputElem == ">=" || inputElem == "==" || inputElem == "and" || inputElem == "or" || inputElem == "not")
                {
                    switch (inputElem)
                    {
                        case "true":
                            tokenStack.Push(inputElem);
                            break;
                        case "false":
                            tokenStack.Push(inputElem);
                            break;
                        case ">":
                            ExecuteBinaryOperator(out float leftSide, out float rightSide, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();
                            if (leftSide > rightSide)
                                tokenStack.Push("true");

                            else
                                tokenStack.Push("false");
                            break;
                        case ">=":
                            ExecuteBinaryOperator(out float leftSide1, out float rightSide1, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();

                            if (leftSide1 >= rightSide1)
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "<":
                            ExecuteBinaryOperator(out float leftSide2, out float rightSide2, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();

                            if (leftSide2 < rightSide2)
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "<=":
                            ExecuteBinaryOperator(out float leftSide3, out float rightSide3, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();

                            if (leftSide3 <= rightSide3)
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "==":
                            ExecuteBinaryOperator(out float leftSide4, out float rightSide4, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();

                            if (leftSide4 == rightSide4)
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "!=":
                            ExecuteBinaryOperator(out float leftSide5, out float rightSide5, tokenStack);

                            tokenStack.Pop();
                            tokenStack.Pop();

                            if (leftSide5 != rightSide5)
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "or":
                            if (bool.Parse(tokenStack.Pop()) || bool.Parse(tokenStack.Pop()))
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;

                        case "and":
                            if (bool.Parse(tokenStack.Pop()) && bool.Parse(tokenStack.Pop()))
                                tokenStack.Push("true");
                            else
                                tokenStack.Push("false");
                            break;
                        case "not":
                            if (bool.Parse(tokenStack.Pop()))
                                tokenStack.Push("false");
                            else
                                tokenStack.Push("true");
                                
                            break;

                    }
                }
                else if(inputElem == "-" || inputElem == "+" || inputElem == "/" || inputElem == "*" || inputElem == "=")
                {
                    switch (inputElem)
                    {
                        case "-":

                            ExecuteBinaryOperator(out float leftSide, out float rightSide, tokenStack);
                      

                            leftSide = leftSide - rightSide;
                            tokenStack.Pop();
                            tokenStack.Pop();
                            tokenStack.Push(leftSide.ToString());
                            //listOfNumbers[listOfNumbers.Count - 2] = listOfNumbers[listOfNumbers.Count - 2] - listOfNumbers[listOfNumbers.Count - 1];
                            //listOfNumbers.RemoveAt(listOfNumbers.Count - 1);
                            break;
                        case "+":
                            ExecuteBinaryOperator(out float leftSide1, out float rightSide1, tokenStack);


                            leftSide1 = leftSide1 + rightSide1;
                            tokenStack.Pop();
                            tokenStack.Pop();
                            tokenStack.Push(leftSide1.ToString());
                            break;
                        case "*":
                            ExecuteBinaryOperator(out float leftSide2, out float rightSide2, tokenStack);


                            leftSide2 = leftSide2 * rightSide2;
                            tokenStack.Pop();
                            tokenStack.Pop();
                            tokenStack.Push(leftSide2.ToString());
                            break;
                        case "/":
                          
                            ExecuteBinaryOperator(out float leftSide3, out float rightSide3, tokenStack);

                            if (rightSide3 == 0)
                            {
                                return "Ошибка, деление на 0";
                            }
                            leftSide3 = leftSide3 / rightSide3;
                            tokenStack.Pop();
                            tokenStack.Pop();
                            tokenStack.Push(leftSide3.ToString());
                            break;
                        case "=":
                            float rightSide4 = 0;
                            if (TableOfIdentifiers.TokenIsContained(tokenStack.Peek()))
                            {
                                rightSide4 = FindValueOfIdentifier(tokenStack.Peek());
                            }
                            else if (TableOfWorkingCells.TokenIsContained(tokenStack.Peek()))
                            {
                                foreach (KeyValuePair<int, (string, float)> KeyValue in TableOfWorkingCells.tableOfWorkingCells)
                                {
                                    if (KeyValue.Value.Item1 == tokenStack.Peek())
                                    {
                                        rightSide4 = KeyValue.Value.Item2;
                                    }
                                }
                            }
                            else
                            {
                                rightSide4 = float.Parse(tokenStack.Peek(), CultureInfo.InvariantCulture);
                            }
                            tokenStack.Pop();


                            int num = 0;
                            string nameOfCell = null;

                            if (TableOfIdentifiers.TokenIsContained(tokenStack.Peek()))
                            {
                                for(int j = 0; j < TableOfIdentifiers.IdentifierListOfTokens.Count; j++)
                                {
                                    if(tokenStack.Peek() == TableOfIdentifiers.IdentifierListOfTokens[j].View)
                                    {
                                        if (TableOfIdentifiers.IdentifierListOfTokens[j].Type == "int")
                                        {
                                            TableOfIdentifiers.IdentifierListOfTokens[j].Value = (int)rightSide4;
                                        }
                                        else if(TableOfIdentifiers.IdentifierListOfTokens[j].Type == "float")
                                        {
                                            TableOfIdentifiers.IdentifierListOfTokens[j].Value = rightSide4;
                                        }
                                    }
                                }
                            }
                          
                            else if(TableOfWorkingCells.TokenIsContained(tokenStack.Peek()))
                            {
                                foreach (KeyValuePair<int, (string, float)> KeyValue in TableOfWorkingCells.tableOfWorkingCells)
                                {
                                    if (KeyValue.Value.Item1 == tokenStack.Peek())
                                    {
                                        num = KeyValue.Key;
                                        nameOfCell = KeyValue.Value.Item1;
                                        break;
                                    }
                                }
                            }

                            TableOfWorkingCells.tableOfWorkingCells[num] = (nameOfCell, rightSide4);
                            //после того как присвоили значение можем удалять со стека, тут сгенирировать команду присвоения
                            tokenStack.Pop();
                            break;
                    }
                }

            }
            //programmResult.ShowDialog();
            return displayResult;
        }
        
        public static float FindValueOfIdentifier(string inputElem)
        {
            for (int j = 0; j < TableOfIdentifiers.IdentifierListOfTokens.Count(); j++)
            {
                if (inputElem == TableOfIdentifiers.IdentifierListOfTokens[j].View)
                {
                    if (TableOfIdentifiers.IdentifierListOfTokens[j].Type == "int")
                    {
                        
                        return (int)float.Parse(TableOfIdentifiers.IdentifierListOfTokens[j].Value.ToString(), CultureInfo.InvariantCulture);
                    }
                    else if (TableOfIdentifiers.IdentifierListOfTokens[j].Type == "float")
                    {
                        return TableOfIdentifiers.IdentifierListOfTokens[j].Value;
                    }
                }
            }
            return 0;// new Exception("Table of Identifies is Empty");
        }

        public static void ExecuteBinaryOperator(out float leftSide, out float rightSide, Stack<string> tokenStack)
        {
            List<string> tmpElem = tokenStack.ToList();
            float tmpValue = 0;

            if (TableOfIdentifiers.TokenIsContained(tmpElem.First()))
            {
                rightSide = FindValueOfIdentifier(tmpElem.First());

            }
            else if(TableOfWorkingCells.TokenIsContained(tmpElem.First()))
            {
                foreach (KeyValuePair<int, (string, float)> KeyValue in TableOfWorkingCells.tableOfWorkingCells)
                {
                    if(KeyValue.Value.Item1 == tmpElem.First())
                    {
                        tmpValue = KeyValue.Value.Item2;
                        break;
                    }
                }
                rightSide = tmpValue;
                //rightSide = 0; //сюда не должно поступать
            }
            else
            {
                rightSide = float.Parse(tmpElem.First(), CultureInfo.InvariantCulture);
            }

            if (TableOfIdentifiers.TokenIsContained(tmpElem[1]))
            {
                leftSide = FindValueOfIdentifier(tmpElem[1]);
            }
            else if (TableOfWorkingCells.TokenIsContained(tmpElem[1]))
            {
                foreach (KeyValuePair<int, (string, float)> KeyValue in TableOfWorkingCells.tableOfWorkingCells)
                {
                    if (KeyValue.Value.Item1 == tmpElem[1])
                    {
                        tmpValue = KeyValue.Value.Item2;
                        break;
                    }
                }
                leftSide = tmpValue;
                //leftSide = 0; //сюда не должно поступать
            }
            else
            {
                leftSide = float.Parse(tmpElem[1], CultureInfo.InvariantCulture);
            }
        }
        //public static void CheckInputElemOnIdentifiers(string inputElem)
        //{
        //    if (TableOfIdentifiers.TokenIsContained(inputElem))
        //    {
        //        for (int j = 0; j < TableOfIdentifiers.IdentifierListOfTokens.Count(); j++)
        //        {
        //            if (inputElem == TableOfIdentifiers.IdentifierListOfTokens[j].View)
        //            {
        //                tokenStack.Push((SourceTableOfTokens.SourceListOfTokens[i].Value - 2 * SourceTableOfTokens.SourceListOfTokens[i].Value).ToString());
        //                //listOfNumbers.Add(Double.Parse(inputElem));
        //            }
        //        }
        //    }
        //    else // можна сделать проверку на наличие в таблице констант
        //    {
        //        string lastStackElemConstant = tokenStack.Pop();
        //        //если элемент константа, то просто добавляем противоположное значение
        //        float tmp = float.Parse(lastStackElemConstant);
        //        tmp = tmp - 2 * tmp;

        //        tokenStack.Push(tmp.ToString());

        //    }
        //}
    }
}
