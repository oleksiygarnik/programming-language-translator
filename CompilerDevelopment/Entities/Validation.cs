using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    static class Validation
    {
        public static bool IsWhiteSeparator(char ch)
        {
            return (char.IsWhiteSpace(ch) || char.IsControl(ch));
        }

        public static bool IsDigit(int ch)
        {
            return (ch >= 48 && ch <= 57);
        }

        public static bool IsLetter(int ch)
        {
            return (ch >= 97 && ch <= 122);
        }

        public static bool IsSingleCharacterSeparator(int ch) // 91 - [  93 = ]  123 = {  125 = } 40 = ( 41 = ) 43 = + 44 = ,
                                                              //42 = * 45 = - 58 = : 59 = ; 47 = / 63 = ?
        {
            return (ch == 91 || ch == 93 || ch == 123 || ch == 125 || ch == 40 || ch == 41 ||
                               ch == 44 || ch == 59 || ch == 43 || ch == 45 || ch == 42 || ch == 47 || ch == 63 || ch == 58);
        }
    }
}
