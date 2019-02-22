using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class TableOfIdentifiers
    {
        public static List<Token> IdentifierListOfTokens = new List<Token> { };

        public static void AddTokenToIdentifierTable(string token, string type)
        {
            int number = IdentifierListOfTokens.Count;
            Token tok = new Token(View: token, Code: 100, Row: 0, CodeIDN: 0, CodeCON: 0, NumberInTable: ++number, Type: type);
            IdentifierListOfTokens.Add(tok);
        }

        public static bool TokenIsContained(string token)
        {
            for (int i = 0; i < IdentifierListOfTokens.Count; i++)
            {
                if (IdentifierListOfTokens[i].View == token)
                {
                    return true;
                }
            }
            return false;
        }

        public static void ViewTableOfIdentifier()
        {
            Console.WriteLine("              Table of Identifier");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("|   Number in Table   |    View    |    Type    |");
            for (int i = 0; i < IdentifierListOfTokens.Count; i++)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("|{0, 13}        |{1, 7}     |{2, 8}    |", IdentifierListOfTokens[i].NumberInTable,
                                                                       IdentifierListOfTokens[i].View,
                                                                       IdentifierListOfTokens[i].Type);
            }
            Console.WriteLine("--------------------------------------------------");
        }

    }
}
