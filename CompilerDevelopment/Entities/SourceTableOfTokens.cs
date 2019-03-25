using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class SourceTableOfTokens
    {
        public static List<Token> SourceListOfTokens = new List<Token> { };

        public static void AddTokenToSourceTable(string token, int code, int codeIDN, int codeCON, int row)
        {
            int number = SourceListOfTokens.Count;
            Token tok = new Token(View: token, Code: code, Row: row, CodeIDN: codeIDN, CodeCON: codeCON, NumberInTable: ++number, Type: null);
            SourceListOfTokens.Add(tok);
        }



        public static void CheckAvailability(string token, int row)
        {
            bool IsContainedInTokensTable = false;
            foreach (KeyValuePair<int, string> KeyValue in TableOfTokens.Tokens)
            {
                //SemanticDataValidation.WriteType(token);
                if (token == KeyValue.Value)
                {
                    AddTokenToSourceTable(token, KeyValue.Key, codeIDN: 0, codeCON: 0, row: row);
                    IsContainedInTokensTable = true;
                }
            }
            if (!IsContainedInTokensTable)
            {
                //double num;
                //bool isNum = double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
                //if (isNum)
                //{
                //    string type = null;
                //    if (!TableOfConstants.TokenIsContained(token))
                //    {
                //        if (SourceListOfTokens[SourceListOfTokens.Count - 1].View == "=")
                //        {

                //            for (int j = TableOfIdentifier.IdentifierListOfTokens.Count(); j > 0; j--)
                //            {
                //                if (SourceListOfTokens[SourceListOfTokens.Count - 2].View == TableOfIdentifier.IdentifierListOfTokens[j - 1].View)
                //                {
                //                    type = TableOfIdentifier.IdentifierListOfTokens[j - 1].Type;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (token.Contains('.'))
                //            {
                //                //if (SemanticDataValidation.Type == "float")
                //                //{
                //                type = "float";
                //                //}

                //                //else
                //                //{
                //                //    int num1 = (int)num;
                //                //    token = num1.ToString();
                //                //    type = SemanticDataValidation.Type;
                //                //}
                //            }
                //            else
                //            {
                //                type = "int";
                //                //Type typeN = num.GetType();
                //                //type = typeN.ToString();
                //                //type = SemanticDataValidation.Type;
                //            }
                //        }

                //        TableOfConstants.AddTokenToTableOfConstant(token, type);
                //    }
                //    //else
                //    //{
                //    //    if (token.Contains('.'))
                //    //    {
                //    //        //if (SemanticDataValidation.Type == "float")
                //    //        //{
                //    //            type = "float";
                //    //        //}

                //    //        //else
                //    //        //{
                //    //        //    int num1 = (int)num;
                //    //        //    token = num1.ToString();
                //    //        //    type = SemanticDataValidation.Type;
                //    //        //}
                //    //    }
                //    //    else
                //    //    {
                //    //        type = "int";
                //    //        //Type typeN = num.GetType();
                //    //        //type = typeN.ToString();
                //    //        //type = SemanticDataValidation.Type;
                //    //    }
                //    //    TableOfConstants.AddTokenToTableOfConstant(token, type);
                //    //}

                //}
                //else
                //{
                if (!TableOfIdentifiers.TokenIsContained(token))
                {
                    string type = null;
                    //if (SemanticDataValidation.IsUseDeclarationIdentifier(token, row))
                    //{
                        //type = SemanticDataValidation.Type;
                        //произвести поиск по таблице SourceTableOfTokens в обратном порядке до тех пор пока не найдет тип
                        for (int i = SourceListOfTokens.Count - 1; i >= 0; i--)
                        {
                            if (SourceListOfTokens[i].View == "int" || SourceListOfTokens[i].View == "float" || SourceListOfTokens[i].View == "bool")
                            {
                                type = SourceListOfTokens[i].View;
                                break;
                            }
                        }
                  //  }
                    TableOfIdentifiers.AddTokenToIdentifierTable(token, type);
                }
                //else
               // {
                //    SemanticDataValidation.IsRepeatDeclarationIdentifier(token, row);

               // }
                //SemanticDataValidation.WriteType(token);
            }
            for (int i = 0; i < TableOfIdentifiers.IdentifierListOfTokens.Count; i++)
            {
                if (TableOfIdentifiers.IdentifierListOfTokens[i].View == token)
                {

                    AddTokenToSourceTable(token, 100, TableOfIdentifiers.IdentifierListOfTokens[i].NumberInTable, 0, row);
                }
            }

            //   }
        }

        public static void ViewSourceTableOfTokens()
        {
            Console.WriteLine("              Source Table of Tokens");
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Console.WriteLine("|   Number in Table   |    Row     |   Token    |     Code     |   CodeIDN    |    CodeCON   |");

            foreach (Token token in SourceListOfTokens)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------");
                Console.WriteLine("|{2, 13}        |{3, 7}     |{1, 8}    |{0, 8}      |{4,8}      |{5,8}      |", token.Code, token.View, token.NumberInTable, token.Row, token.CodeIDN, token.CodeCON);
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }


        public static bool StartParse(string path = @"D:\ASM\SourceCode.txt")
        {
            bool HAS_TO_READ = true;
            int state = 1;
            using (StreamReader sr = new StreamReader(path))
            {
                int ch = 0;
                string token = null;
                int row = 1;

                while (ch != -1)
                {
                    switch (state)
                    {
                        case 1:

                            if (HAS_TO_READ)
                            {
                                ch = sr.Read();
                            }
                            else
                            {
                                token = null;
                            }

                            while (ch == 32)  // 32 - space
                            {
                                ch = sr.Read();
                                token = " ";
                            }

                            //while (ch == 13 || ch == 32)
                            //{
                            //    ch = sr.Read();
                            //    if (ch == 10)
                            //    {
                            //        row++;
                            //        ch = sr.Read();
                            //    }
                            //    token = "";
                            //    HAS_TO_READ = false;
                            //}


                            if (ch == 13) // ch = 13 carriage return ch = 10 new line
                            {
                                ch = sr.Read();
                                if (ch == 10)
                                {
                                    ch = sr.Read();
                                    row++;
                                    while (ch == 32 || ch == 10 || ch == 13)  // 32 - space
                                    {
                                        ch = sr.Read();
                                        token = " ";
                                    }
                                    HAS_TO_READ = false;
                                }
                            }

                            //if (ch >= 97 && ch <= 122)
                            if (Validation.IsLetter(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 2;
                            }
                            //if (ch >= 48 && ch <= 57)
                            if (Validation.IsDigit(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 3;
                            }
                            if (ch == 46)     //.
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 5;
                            }
                            if (ch == 60) // <
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 6;
                            }
                            if (ch == 62) // >
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 7;
                            }
                            if (ch == 61)  // =
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 8;
                            }
                            if (ch == 33)   // !
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 9;
                            }
                            //if (ch == 91 || ch == 93 || ch == 123 || ch == 125 || ch == 40 || ch == 41 ||
                            //    ch == 44 || ch == 59 || ch == 43 || ch == 45 || ch == 42 || ch == 47 || ch == 63 || ch == 58)
                            if (Validation.IsSingleCharacterSeparator(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                            else
                            {
                                if (ch != -1)
                                {
                                    if (ch != 10 && ch != 13 && ch != 32)
                                    {
                                        char sym = Convert.ToChar(ch);
                                        throw new Exception($"Используется неизвестный символ - { sym } в рядке " + row);
                                    }
                                }

                                ch = sr.Read();
                            }
                            break;

                        case 2:
                            //if (ch >= 97 && ch <= 122)
                            if (Validation.IsLetter(ch) || Validation.IsDigit(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 2;
                            }

                            else
                            {
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }

                        case 3:
                            //if (ch >= 48 && ch <= 57)
                            if (Validation.IsDigit(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 3;
                            }
                            if (ch == 46) // ..
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 4;
                            }
                            else if (Validation.IsLetter(ch))
                            {
                                throw new Exception("Error! Буква после константы. Неопознаная лексема: " + token + " в рядке " + row);
                            }
                            else
                            {
                                token = token.Trim();
                                TableOfConstants.CheckConstant(token, row);
                                //TableOfConstants.AddTokenToTableOfConstant(token);
                                //CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }

                            break;
                        case 4:
                            //if (ch >= 48 && ch <= 57)
                            if (Validation.IsDigit(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 4;
                            }
                            else if (Validation.IsLetter(ch))
                            {
                                throw new Exception("Неопознаная лексема: " + token + " в рядке " + row);
                            }
                            else
                            {
                                token = token.Trim();
                                TableOfConstants.CheckConstant(token, row);
                                //CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                        case 5:
                            //if (ch >= 48 && ch <= 57)
                            if (Validation.IsDigit(ch))
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                goto case 4;
                            }
                            else
                            {
                                throw new Exception("Лексическая ошибка в рядке " + row + ". Следюущая лексема после точки не цифра");
                            }
                            break;
                        case 6:
                            if (ch == 60 || ch == 61)
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                            else
                            {
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                        case 7:
                            if (ch == 62 || ch == 61)
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                            else
                            {
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                        case 8:

                            if (ch == 61)
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                            else
                            {
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                        case 9:

                            if (ch == 61)
                            {
                                token += Convert.ToChar(ch);
                                ch = sr.Read();
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                            else
                            {
                                token = token.Trim();
                                CheckAvailability(token, row);
                                HAS_TO_READ = false;
                                goto case 1;
                            }
                    }
                }
            }
            return true;
        }
    }
}
