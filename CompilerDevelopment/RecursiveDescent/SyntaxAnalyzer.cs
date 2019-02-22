using CompilerDevelopment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.RecursiveDescent
{
        class SyntaxAnalyzer
        {
            public static List<string> errors = new List<string> { };

            public static void NextToken(ref int i)
            {
                if (SourceTableOfTokens.SourceListOfTokens.Count - 1 == i)
                {
                    throw new Exception("Исходная таблица лексем исчерпана, но не хватает лексем для базовой структуры программы");
                    // errors.Add("Исходная таблица лексем исчерпана, но не хватает лексем для базовой структуры программы");
                }
                else
                {
                    i++;
                }
            }



            public static bool Analyze(int i) // готовая подпрограмма
            {
                bool found = false;
                if (ListDeclaration(ref i)) //список обьявлений
                {
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "{") // ожидается символ {
                    {
                        NextToken(ref i);
                        //i++;
                        if (ListOperator(ref i)) // список операторов
                        {
                            if (SourceTableOfTokens.SourceListOfTokens[i].View == "}") // ожидается символ }
                            {
                                found = true;
                            }
                            else
                            {
                                errors.Add("Ожидается символ }  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                //throw new Exception("Ожидается символ }  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            }
                        }
                        else
                        {
                            errors.Add("Неверный список операторов  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            //throw new Exception("Неверный список операторов  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                    }
                    else
                    {
                        errors.Add("Ожидается символ {  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        // throw new Exception("Ожидается символ {  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                else
                {
                    errors.Add("Неверный список обьявлений  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    //throw new Exception("Неверный список обьявлений  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }

                return found;
            }

            public static bool ListDeclaration(ref int i)
            {
                bool found = false;
                if (Declaration(ref i))
                {
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == ";" && found == true)
                    {
                        NextToken(ref i);
                        //i++;
                        if (Declaration(ref i)) { }
                        else
                        {
                            found = false;
                            errors.Add("Отсутствует обьявление. Ошибка в обьявлении  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            //throw new Exception("Отсутствует обьявление. Ошибка в обьявлении  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                    }
                }
                else
                {
                    errors.Add("Отсутствует первое обьявление  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    //throw new Exception("Отсутствует первое обьявление  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }
                return found;
            }


            public static bool Type(ref int i) // проверка на возможный тип, int или float
            {
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "int" || SourceTableOfTokens.SourceListOfTokens[i].View == "float"
                     || SourceTableOfTokens.SourceListOfTokens[i].View == "bool")
                {
                    NextToken(ref i);
                    //i++;
                    return true;
                }
                else
                    return false;
            }

            public static bool Declaration(ref int i)
            {
                bool found = false;
                if (Type(ref i))
                {
                    if (ListOfIdentifier(ref i))
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                        errors.Add("Неверный список идентификаторов  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        //throw new Exception("Неверный список идентификаторов  в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                else
                {
                    found = false;
                    errors.Add("Ожидается тип данных в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    //throw new Exception("Ожидается тип данных в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }
                return found;
            }

            public static bool ListOfIdentifier(ref int i)
            {
                bool found = false;
                if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                {
                    NextToken(ref i);
                    //i++;
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == "," && found == true)
                    {
                        NextToken(ref i);
                        //i++;
                        if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                        {
                            NextToken(ref i);
                            //i++;
                        }
                        else
                        {
                            found = false;
                            errors.Add("Ожидается идентификатор после лексемы ',' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                        //throw new Exception(");
                    }
                }
                else
                {
                    errors.Add("Ожидается идентификатор в обьявлении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    //throw new Exception("Ожидается идентификатор в обьявлении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }
                return found;
            }

            public static bool StopTokensForListOperator(ref int i, string allow = "none")
            {
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "}")
                {
                    return true;
                }
                if (allow == "condition")
                {
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "else" ||
                     SourceTableOfTokens.SourceListOfTokens[i].View == "endif")
                    {
                        return true;
                    }
                }
                else if (allow == "cycle")
                {
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "next")
                    {
                        return true;
                    }
                }
                return false;
            }

            public static bool ListOperator(ref int i, string allow = "none")
            {
                bool found = false;
                if (Operator(ref i))
                {
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == ";")
                    {
                        NextToken(ref i);
                        //i++;
                        found = true;

                        while (StopTokensForListOperator(ref i, allow) != true && found == true)
                        {

                            if (Operator(ref i))
                            {
                                if (SourceTableOfTokens.SourceListOfTokens[i].View == ";")
                                {
                                    NextToken(ref i);
                                    //i++;
                                }
                                else
                                {
                                    found = false;
                                    errors.Add("Пропущено окончание оператора. Ожидается ';' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                    //i = i - 1;
                                    //throw new Exception("Пропущено окончание оператора. Ожидается ';' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                }

                            }
                            else
                            {
                                //i--;
                                found = false;
                                errors.Add("Данная конструкция является недопустимой. Неизвестный оператор в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                //throw new Exception("Данная конструкция является недопустимой. Неизвестный оператор в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            }
                        }

                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "}")
                        {
                            if (SourceTableOfTokens.SourceListOfTokens.Count - 1 == i)
                            {

                            }
                            else
                            {
                                errors.Add("Обнаруженое ранее окончание. Неизвестный код в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                //throw new Exception("Обнаруженое ранее окончание. Неизвестный код в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            }
                        }
                    }
                    else
                    {
                        found = false;
                        errors.Add("Пропущено ; в первом операторе в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        //i = i - 1;
                        // throw new Exception("Пропущено ; в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                else
                {
                    errors.Add("Отсутствует первый оператор в списке операторов в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    // throw new Exception("Отсутствует первый оператор в списке операторов в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }
                return found;
            }

            public static bool Operator(ref int i)
            {
                bool found = false;
                if (Output(ref i)) // operator = cout
                {
                    found = true;
                }
                else if (Input(ref i))
                {
                    found = true;
                }
                else if (Appropriation(ref i))
                {
                    found = true;
                }
                else if (ConditionalOperator(ref i))
                {
                    found = true;
                }
                else if (Cycle(ref i))
                {
                    found = true;
                }
                else
                {
                    errors.Add("Даного оператора не существует в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                }
                return found;
            }

            public static bool Input(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "cin")
                {
                    NextToken(ref i);
                    // i++;
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "<<")
                    {
                        NextToken(ref i);
                        //i++;
                        if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                        {
                            NextToken(ref i);
                            //i++;
                            found = true;
                            while (SourceTableOfTokens.SourceListOfTokens[i].View == "<<" && found == true)
                            {
                                NextToken(ref i);
                                //i++;
                                if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                                {
                                    found = true;
                                    NextToken(ref i);
                                    //i++;
                                }
                                else
                                {
                                    //i--;
                                    errors.Add("Ожидается идентификатор в операторе cin в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                    found = false;
                                }
                            }
                        }
                        else
                        {
                            //i--;
                            errors.Add("Ожидается идентификатор после лексемы '<<' в операторе ввода в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            found = false;
                        }
                    }
                    else
                    {
                        //i--;
                        errors.Add("Ожидается лексема '<<' в операторе ввода в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                else
                    found = false;
                return found;
            }

            public static bool Output(ref int i) // недоработанное ?????  
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "cout")
                {
                    NextToken(ref i);
                    //i++;

                    if (SourceTableOfTokens.SourceListOfTokens[i].Code == 25)
                    {
                        NextToken(ref i); //i++;
                        if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                        {
                            NextToken(ref i); //i++;
                            found = true;
                            while (SourceTableOfTokens.SourceListOfTokens[i].Code == 25 && found == true)
                            {
                                NextToken(ref i); //i++;
                                if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                                {
                                    found = true;
                                    NextToken(ref i);// i++;
                                }
                                else
                                {
                                    //i--;
                                    errors.Add("Ожидается идентификатор в операторе cout в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                    found = false;
                                }
                            }
                        }
                        else
                        {
                            //i--;
                            errors.Add("Ожидается идентификатор после лексемы '>>' в операторе вывода в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            found = false;
                        }
                    }
                    else
                    {
                        //i--;
                        errors.Add("Ожидается лексема '>>' в операторе вывода в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        found = false;
                    }
                }
                else
                    found = false;
                return found;
            }

            public static bool Appropriation(ref int i)
            {
                bool found = false;
                if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                {
                    NextToken(ref i); //i++;
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "=")
                    {
                        NextToken(ref i); //i++;
                        if (Expression(ref i))
                        {
                            found = true;
                        }
                    }
                    else
                    {
                        //i--;
                        errors.Add("В операторе присвоения пропущена лексема '=' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");

                    }
                }
                return found;
            }

            public static bool ConditionalOperator(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "if")
                {
                    NextToken(ref i); //i++;
                    if (LogicalExpression(ref i))
                    {
                        if (SourceTableOfTokens.SourceListOfTokens[i].View == ";")
                        {
                            NextToken(ref i);// i++;
                            if (ListOperator(ref i, "condition"))
                            {
                                if (SourceTableOfTokens.SourceListOfTokens[i].View == "else")
                                {
                                    NextToken(ref i); //i++;
                                    if (ListOperator(ref i, "condition"))
                                    {
                                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "endif")
                                        {
                                            NextToken(ref i); //i++;
                                            found = true;
                                        }
                                        else
                                        {
                                            errors.Add("В условном операторе пропущена лексема 'endif' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                        }
                                    }
                                    else
                                    {
                                        errors.Add("В условном операторе после else пропущен список операторов в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                    }
                                }
                                else
                                {
                                    errors.Add("В условном операторе пропущена лексема 'else' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                }
                            }
                            else
                            {
                                errors.Add("В условном операторе пропущен список операторов в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            }
                        }
                        else
                        {
                            errors.Add("Ожидается ';' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }

                    }
                    else
                    {
                        errors.Add("В условном операторе после if пропущено логическое выражение в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                return found;
            }


            public static bool Cycle(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "for")
                {
                    NextToken(ref i); //i++;
                    if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                    {
                        NextToken(ref i);// i++;
                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "=")
                        {
                            NextToken(ref i);// i++;
                            if (Expression(ref i))
                            {

                                if (SourceTableOfTokens.SourceListOfTokens[i].View == "to")
                                {
                                    NextToken(ref i); //i++;
                                    if (Expression(ref i))
                                    {
                                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "step")
                                        {
                                            NextToken(ref i);// i++;
                                            if (Expression(ref i))
                                            {
                                                if (SourceTableOfTokens.SourceListOfTokens[i].View == "do")
                                                {
                                                    NextToken(ref i); //i++;
                                                    if (ListOperator(ref i, "cycle"))
                                                    {
                                                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "next")
                                                        {
                                                            NextToken(ref i);// i++;
                                                            found = true;
                                                        }
                                                        else
                                                        {
                                                            errors.Add("В цикле пропущена лексема 'next' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errors.Add("В цикле пропущен список операторов в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                                    }
                                                }
                                                else
                                                {
                                                    // i--;
                                                    errors.Add("В цикле пропущена лексема 'do' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                                }
                                            }
                                            else
                                            {
                                                errors.Add("В цикле после лексемы 'step' пропущено выражение в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                            }
                                        }
                                        else
                                        {
                                            errors.Add("В цикле пропущена лексема 'step' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                        }
                                    }
                                    else
                                    {
                                        errors.Add("В цикле после лексемы 'to' пропущено выражение в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                    }
                                }
                                else
                                {
                                    errors.Add("В цикле пропущена лексема 'to' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                }
                            }
                            else
                            {
                                errors.Add("В цикле после лексемы '=' пропущено выражение в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            }
                        }
                        else
                        {
                            errors.Add("В цикле после идентификатора пропущена лексема '=' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                    }
                    else
                    {
                        errors.Add("В цикле после лексемы 'for' пропущен идентификатор в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                return found;
            }
            public static bool LogicalExpression(ref int i, bool closing_bracket = false)
            {
                bool found = false;
                if (LogicalTerm(ref i)) // i++ includes
                {
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == "or")
                    {
                        //if (closing_bracket)
                        //{
                        //    if (SourceTableOfTokens.SourceListOfTokens[i].View == "]")
                        //    {
                        //        break;
                        //    }
                        //}
                        //else if (closing_bracket == false && SourceTableOfTokens.SourceListOfTokens[i].View == "]")
                        //{
                        //    found = false;
                        //    errors.Add("Обнаруженная лишняя лексема ']' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        //}
                        NextToken(ref i);// i++; 
                        if (LogicalTerm(ref i))
                        {
                            found = true;
                        }
                        else
                        {
                            found = false;
                            errors.Add("Отсутсвует логическое выражение после or в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядку");
                        }
                    }
                }
                //else
                //{
                //    throw new Exception("LT error");
                //}
                return found;
            }

            public static bool LogicalTerm(ref int i)
            {
                bool found = false;

                if (LogicalF(ref i))
                {
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == "and")
                    {
                        NextToken(ref i); //i++;
                        if (LogicalF(ref i))
                        {
                            found = true;
                        }
                        else
                        {
                            found = false;
                            errors.Add("Отсутсвует логическое выражение после and в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядку");
                        }
                    }
                }
                return found;
            }

            public static bool LogicalF(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "not")
                {
                    NextToken(ref i); //i++;
                    if (LogicalF(ref i))
                    {
                        found = true;
                    }
                }
                else if (SourceTableOfTokens.SourceListOfTokens[i].View == "[")
                {
                    NextToken(ref i); //i++;
                    if (LogicalExpression(ref i, true))
                    {
                        if (SourceTableOfTokens.SourceListOfTokens[i].View == "]")
                        {
                            NextToken(ref i); //i++;
                            found = true;
                        }
                        else
                        {
                            errors.Add("Пропущена лексема ']' в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                    }
                    else
                    {
                        errors.Add("После лексемы '[' ожидается логическое выражение в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }
                else if (LogicalRel(ref i))
                {
                    found = true;
                }
                return found;
            }

            public static bool LogicalRel(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "true" || SourceTableOfTokens.SourceListOfTokens[i].View == "false")
                {
                    NextToken(ref i); //i++;
                    found = true;
                }
                else if (Expression(ref i))
                {
                    if (Sign(ref i))
                    {
                        if (Expression(ref i))
                        {
                            found = true;
                        }
                        else
                        {
                            errors.Add("Ожидается выражение после логического знака в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядку");
                        }
                    }
                    else
                    {
                        errors.Add("Ожидается логический знак в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядку");
                    }

                }
                return found;
            }

            public static bool Sign(ref int i)
            {
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "<" || SourceTableOfTokens.SourceListOfTokens[i].View == ">"
                     || SourceTableOfTokens.SourceListOfTokens[i].View == "<=" || SourceTableOfTokens.SourceListOfTokens[i].View == ">="
                     || SourceTableOfTokens.SourceListOfTokens[i].View == "==" || SourceTableOfTokens.SourceListOfTokens[i].View == "!=")
                {
                    NextToken(ref i); //i++;
                    return true;
                }
                else
                    return false;
            }





            public static bool Expression(ref int i)
            {
                bool found = false;
                if (Term(ref i))
                {
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == "+" || SourceTableOfTokens.SourceListOfTokens[i].View == "-") // весь список лексем перед коТорыми может стоять выражение, ; < >
                    {
                        NextToken(ref i); //i++;
                        if (Term(ref i))
                        {
                            found = true;
                        }
                        else
                        {
                            found = false;
                            errors.Add("Отсутствует слагаемое/вычитаемое в выражении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                        }
                        // }
                        //else
                        //    throw new Exception("Ожидается выражение/Ошибка в построении выражения");

                    }

                }
                return found;
            }

            public static bool Term(ref int i)
            {
                bool found = false;
                if (SourceTableOfTokens.SourceListOfTokens[i].View == "-")
                {
                    NextToken(ref i); //i++;
                    if (M(ref i))
                    {
                        found = true;
                        while (SourceTableOfTokens.SourceListOfTokens[i].View == "/" || SourceTableOfTokens.SourceListOfTokens[i].View == "*") // весь список лексем перед коТорыми может стоять выражение, ; < >
                        {
                            NextToken(ref i);// i++;
                            if (M(ref i))
                            {
                                found = true;
                            }
                            else
                            {
                                errors.Add("Отсутствует делитель/множитель в выражении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                                found = false;
                            }
                        }
                    }
                }
                else if (M(ref i))
                {
                    found = true;
                    while (SourceTableOfTokens.SourceListOfTokens[i].View == "/" || SourceTableOfTokens.SourceListOfTokens[i].View == "*") // весь список лексем перед коТорыми может стоять выражение, ; < >
                    {
                        NextToken(ref i); //i++;
                        if (M(ref i))
                        {
                            found = true;
                        }
                        else
                        {
                            errors.Add("Отсутствует делитель/множитель в выражении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            found = false;
                        }
                    }
                }

                return found;
            }

            public static bool M(ref int i)
            {
                bool found = false;

                if (SourceTableOfTokens.SourceListOfTokens[i].View == "(")
                {
                    //bracket.Push(SourceTableOfTokens.SourceListOfTokens[i].View);
                    NextToken(ref i); //i++;

                    if (Expression(ref i))
                    {
                        if (SourceTableOfTokens.SourceListOfTokens[i].View == ")")
                        {
                            NextToken(ref i); //i++;
                                              //bracket.Pop();
                            found = true;
                        }
                        else
                        {
                            errors.Add("Пропущен символ ')' в выражении в " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                            found = false;
                        }
                    }
                    else
                    {
                        errors.Add("В скобках необходимо стоять выражение в  " + SourceTableOfTokens.SourceListOfTokens[i].Row + " рядке");
                    }
                }

                else if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                {
                    NextToken(ref i);// i++;
                    found = true;
                }
                else if (TableOfConstants.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                {
                    NextToken(ref i);// i++;
                    found = true;
                }

                return found;
            }
    }

}
