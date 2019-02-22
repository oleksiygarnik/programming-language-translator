using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class TableOfConstants
    {
        public static List<Token> ConstantListOfTokens = new List<Token> { };

        public static void AddTokenToTableOfConstant(string token, string type)
        {
            int number = ConstantListOfTokens.Count;
            Token tok = new Token(View: token, Code: 101, Row: 0, CodeIDN: 0, CodeCON: 0, NumberInTable: ++number, Type: type);
            ConstantListOfTokens.Add(tok);
        }

        public static bool TokenIsContained(string token)
        {
            if (ConstantListOfTokens.Count > 0)
            {
                for (int i = 0; i < ConstantListOfTokens.Count; i++)
                {
                    if (ConstantListOfTokens[i].View == token)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void CheckConstant(string token, int row)
        {
            // один из способов записание типа в таблицу констант
            string type;
            if (!TokenIsContained(token))
            {
                if (SemanticDataValidation.CheckCast(token, out type, row))
                {
                    AddTokenToTableOfConstant(token, type);
                }
            }

            //    if (SourceTableOfTokens.SourceListOfTokens.Count > 1)
            //    {
            //        if (SourceTableOfTokens.SourceListOfTokens[SourceTableOfTokens.SourceListOfTokens.Count - 1].View == "=")
            //        {

            //            for (int j = TableOfIdentifier.IdentifierListOfTokens.Count(); j > 0; j--)
            //            {
            //                if (SourceTableOfTokens.SourceListOfTokens[SourceTableOfTokens.SourceListOfTokens.Count - 2].View == TableOfIdentifier.IdentifierListOfTokens[j - 1].View)
            //                {
            //                    type = TableOfIdentifier.IdentifierListOfTokens[j - 1].Type;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (token.Contains('.'))
            //            {
            //                type = "float";
            //            }
            //            else
            //            {
            //                type = "int";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (token.Contains('.'))
            //        {
            //            //if (SemanticDataValidation.Type == "float")
            //            //{
            //            type = "float";
            //            //}

            //            //else
            //            //{
            //            //    type = SemanticDataValidation.Type;
            //            //}
            //        }
            //        else
            //        {
            //            type = "int";
            //            //type = SemanticDataValidation.Type;
            //        }
            //    }
            //        AddTokenToTableOfConstant(token, type);   
            //}

            for (int i = ConstantListOfTokens.Count - 1; i >= 0; i--)
            {
                if (ConstantListOfTokens[i].View == token)
                {
                    SourceTableOfTokens.AddTokenToSourceTable(token, 101, 0, ConstantListOfTokens[i].NumberInTable, row);
                    break;
                }
            }
        }

        public static void ViewTableOfConstant()
        {

            Console.WriteLine("              Table of Constants");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("|   Number in Table   |    View    |    Type    |");
            for (int i = 0; i < ConstantListOfTokens.Count; i++)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("|{0, 13}        |{1, 7}     |{2, 8}    |", ConstantListOfTokens[i].NumberInTable,
                                                                         ConstantListOfTokens[i].View,
                                                                         ConstantListOfTokens[i].Type);
            }
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
