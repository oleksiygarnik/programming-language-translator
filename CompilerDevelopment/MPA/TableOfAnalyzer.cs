using CompilerDevelopment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.PolisTableByDijkstra
{
    static class TableOfAnalyzer
    {
        public class Row
        {
            public string InputToken { get; set; }
            public int CurrentState { get; set; }
            public string FullStack { get; set; }

        }

        public static Dictionary<int, Row> tableOfAnalysis = new Dictionary<int, Row>();

        public static Stack<int?> fullStack = new Stack<int?>();

        public static string getKeyForDictionary(string View)
        {
            if (TableOfConstants.TokenIsContained(View))
            {
                return "const";
            }
            if (TableOfIdentifiers.TokenIsContained(View))
            {
                return "id";
            }
            return View;
        }


        public static string PushInTable(int start_state)
        {
            int i = 0;
            int count = 0;
            //int start_state = 9;

            State state = new State(); //состояние
            bool found = true;

            while (found == true)
            {
                string stack = string.Join(",", fullStack.Where(n => n != null));
                string InputTokenTmp = SourceTableOfTokens.SourceListOfTokens.Count > i ? SourceTableOfTokens.SourceListOfTokens[i].View : " ";

                Row row = new Row()
                {
                    CurrentState = start_state,
                    InputToken = InputTokenTmp,
                    FullStack = stack
                };

                if (TableOfTransitions.tableOfTransitions.TryGetValue(start_state, out state)) // получить всю информацию о стане
                {
                    if (state.dictionary.TryGetValue(getKeyForDictionary(InputTokenTmp), out var value)) // проверяем есть ли в нашем стане необходимая метка
                    {
                        if (state.Info.Contains("[=] - Exit"))
                        {
                            if (fullStack.Count != 0)
                            {
                                start_state = (int)fullStack.Pop();
                                tableOfAnalysis.Add(count + 1, row);
                                count++;
                                i++;
                            }
                            else
                            {
                                tableOfAnalysis.Add(count + 1, row);
                                found = false; // программа завершена
                                return "Программа успешно прошла синтаксический анализ";
                            }

                        }
                        else
                        {
                            start_state = value.destination;
                            if (value.stack != 0)
                            {
                                if (value.stack != null)
                                {
                                    fullStack.Push(value.stack);
                                }
                            }
                            tableOfAnalysis.Add(count + 1, row);
                            count++;
                            i++;
                        }
                    }
                    else // если нету соответсвующей метки в стане
                    {
                        tableOfAnalysis.Add(count + 1, row);
                        count++;
                        if (state.Info.Contains("[!=] - Error"))
                        {
                            //Console.WriteLine(state.Error);
                            found = false;
                            return state.Error;
                        }
                        else if (state.Info.Contains("[!=] - Exit"))
                        {
                            if (fullStack.Count != 0)
                            {
                                start_state = (int)fullStack.Pop();
                            }
                        }
                        else if (state.Info.Contains("[!=] - SubAuto"))
                        {
                            string str = null;
                            str = state.Info.Replace("[!=] - SubAuto(destination:", " ");
                            str = str.Replace("stack:", " ");
                            str = str.Replace(")", " ");
                            if (str.Contains("[=] - Exit"))
                            {
                                str = str.Replace("[=] - Exit", " ");
                            }
                            string[] words = str.Split(new char[] { ',' });
                            start_state = int.Parse(words[0]);
                            fullStack.Push(int.Parse(words[1]));
                        }
                    }
                }
                else
                {
                    found = false;
                    return "Key is not found.";
                }

            }
            return state.Error;
        }

        public static void ViewTableOfAnalysis()
        {
            Console.WriteLine("              Table of Analysis");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("|   №   |    Input Token     |    State    |    Stack   |");
            foreach (KeyValuePair<int, Row> keyValue in tableOfAnalysis)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("|{0, 4}   |{1, 11}         |{2, 8}     |{3, 8}    |", keyValue.Key,
                                                                       keyValue.Value.InputToken,
                                                                       keyValue.Value.CurrentState,
                                                                       keyValue.Value.FullStack);
            }
            Console.WriteLine("---------------------------------------------------------");
        }
    }
}
