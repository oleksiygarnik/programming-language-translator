using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Entities
{
    class Token
    {
        public int NumberInTable { get; set; }
        public string View { get; set; }
        public int Code { get; set; }
        public int Row { get; set; }
        public int CodeIDN { get; set; }
        public int CodeCON { get; set; }
        public string Type { get; set; }
        public float Value { get; set; }

        public Token() : this(null, 0, 0, 0, 0, 0, null, -123456)
        { }

        public Token(string View, int Code, int Row, int CodeIDN, int CodeCON, int NumberInTable, string Type)
        {
            this.Code = Code;
            this.View = View;
            this.Row = Row;
            this.CodeIDN = CodeIDN;
            this.Type = Type;
            this.CodeCON = CodeCON;
            this.NumberInTable = NumberInTable;
        }

        public Token(string View, int Code, int Row, int CodeIDN, int CodeCON, int NumberInTable, string Type, float Value)
        {
            this.Code = Code;
            this.View = View;
            this.Row = Row;
            this.CodeIDN = CodeIDN;
            this.Type = Type;
            this.CodeCON = CodeCON;
            this.NumberInTable = NumberInTable;
            this.Value = Value;
        }
    }
}
