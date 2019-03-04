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
        public string SemanticSubProgramm { get; set; }

        public Node() { }

        public Node(List<Element> elements)
        {
            this.elements = elements;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            if (node != null)
            {
                if(node.elements.Count == this.elements.Count)
                {
                    for(int i = 0;  i< this.elements.Count; i++)
                    {
                        if(node.elements[i].Name == this.elements[i].Name)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                //foreach(var item in node.elements)
                //{
                //    foreach(var item2 in this.elements)
                //    {
                //        if(item.Name == item2.Name)
                //        {
                //            continue;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //}
            }
            return false;
        }

    }
}
