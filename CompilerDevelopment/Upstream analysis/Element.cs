using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis
{
    abstract class Element
    {
        public string Name { get; set; }

        public Element()
        {

        }
        public Element(string Name)
        {
            this.Name = Name;
        }
    }
}
