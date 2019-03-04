using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class SemanticDataValidation
    {
        public static string Type = null;

        public static void WriteType(string token)
        {
            if (token == "int" || token == "float" || token == "bool")
            {
                Type = token;

            }
            //if (TableOfIdentifier.IdentifierListOfTokens.Count > 0)
            //{
            //    for (int i = TableOfIdentifier.IdentifierListOfTokens.Count - 1; i <= 0; i--)
            //    {
            //        if (token == TableOfIdentifier.IdentifierListOfTokens[i].View)
            //        {
            //            Type = TableOfIdentifier.IdentifierListOfTokens[i].Type;
            //        }
            //    }
            //}
        }

        public static string FindOutTheType(string token)
        {
            return null;
        }

        public static bool CheckCast(string token, out string type, int row)
        {
            type = null;
            string previouslyType = null;
            if (token.Contains('.'))
            {
                previouslyType = "float";
            }
            else
            {
                previouslyType = "int";
            }
            if (SourceTableOfTokens.SourceListOfTokens.Count > 0)
            {
                if (SourceTableOfTokens.SourceListOfTokens[SourceTableOfTokens.SourceListOfTokens.Count - 1].View == "=")
                {
                    for (int j = TableOfIdentifiers.IdentifierListOfTokens.Count(); j > 0; j--)
                    {
                        if (SourceTableOfTokens.SourceListOfTokens[SourceTableOfTokens.SourceListOfTokens.Count - 2].View == TableOfIdentifiers.IdentifierListOfTokens[j - 1].View)
                        {
                            type = TableOfIdentifiers.IdentifierListOfTokens[j - 1].Type;
                        }
                    }
                }
                else
                {
                    type = previouslyType;
                }
            }
            else
            {
                type = previouslyType;
            }
            if (previouslyType == type || (previouslyType == "int" && type == "float"))
            {
                return true;
            }
            else
            {
                throw new Exception("Семантическая ошибка. Не удается неявно преобразовать тип float в int. " + "Ошибка в рядке: " + row);
            }
            return true;
        }

        public static void IsRepeatDeclarationIdentifier(string token, int row)
        {
            for (int i = SourceTableOfTokens.SourceListOfTokens.Count - 1; i >= 0; i--)
            {
                if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View) || SourceTableOfTokens.SourceListOfTokens[i].View == ",")
                {
                    continue;
                }

                else if (SourceTableOfTokens.SourceListOfTokens[i].View == "int" || SourceTableOfTokens.SourceListOfTokens[i].View == "float" || SourceTableOfTokens.SourceListOfTokens[i].View == "bool")
                {
                    throw new Exception("Повторное обьявление индентификатора: " + token + " в рядке " + row);
                }
                else
                {
                    break;
                }
            }
        }

        public static bool IsUseDeclarationIdentifier(string token, int row)
        {
            if (SourceTableOfTokens.SourceListOfTokens.Count > 0)
            {
                for (int i = SourceTableOfTokens.SourceListOfTokens.Count - 1; i >= 0; i--)
                {
                    if (SourceTableOfTokens.SourceListOfTokens[i].View == "int" || SourceTableOfTokens.SourceListOfTokens[i].View == "float" || SourceTableOfTokens.SourceListOfTokens[i].View == "bool")
                    {

                        return true;
                    }
                    else if (SourceTableOfTokens.SourceListOfTokens[i].View == ",")
                    {
                        continue;
                    }
                
                    else if (SourceTableOfTokens.SourceListOfTokens[i].View == ":")
                {
                    continue;
                }
                else if (TableOfIdentifiers.TokenIsContained(SourceTableOfTokens.SourceListOfTokens[i].View))
                    {
                        continue;
                    }
                    else
                    {
                        throw new Exception("Обьявление неоголошенной переменной: " + token + " в рядке " + row);
                    }
                }
            }
            else
            {
                throw new Exception("Обьявление неоголошенной переменной: " + token + " в рядке 1 ");
            }
            return false;
        }
    }
}
