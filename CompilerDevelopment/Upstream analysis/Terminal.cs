using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis
{
    class Terminal : Element
    {
        public string Next { get; set; }
        public string Previous { get; set; }

        public Terminal(string Name) : base(Name)
        {

        }
        public Terminal(string Next, string Previous, string Name) : base(Name)
        {
            this.Next = Next;
            this.Previous = Previous;
        }
    }
}
