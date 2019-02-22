using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.Upstream_analysis
{
    class Node
    {
        public List<Element> elements = new List<Element>();

        public Node(List<Element> elements)
        {
            this.elements = elements;
        }
    }
}
