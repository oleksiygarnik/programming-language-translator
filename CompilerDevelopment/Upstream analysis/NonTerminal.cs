using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis
{
    class NonTerminal : Element
    {
        public string Next { get; set; }
        public string Previous { get; set; }

        public NonTerminal(string Name) : base(Name)
        {

        }
        public NonTerminal(string Next, string Previous, string Name) : base(Name)
        {
            this.Next = Next;
            this.Previous = Previous;
        }
    }
}
