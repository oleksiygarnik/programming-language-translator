using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerDevelopment.MPA
{
    static class TableOfTransitions
    {
        public static Dictionary<int, State> tableOfTransitions = new Dictionary<int, State>();


        public static Dictionary<int, State> Loading()
        {
            XDocument xDocument = XDocument.Load("TableOfTransitions.xml");
            XElement root = xDocument.Element("states");

            foreach (var xmlState in root.Elements())
            {
                State state = new State
                {
                    isFinal = bool.Parse(xmlState.Attribute("final").Value),
                };

                foreach (var link in xmlState.Elements())
                {
                    if (link.Name == "link")
                    {
                        int? stack1 = null;

                        if (int.TryParse(link.Element("stack").Value, out int stack2))
                        {
                            stack1 = stack2;
                        }

                        if (int.TryParse(link.Element("destination").Value, out int destination1))
                        {
                        }

                        var (stack, destination) = (stack1, destination1);

                        state.dictionary[link.Element("class").Value] = (stack, destination);
                    }
                    else
                    {
                        state.Info = xmlState.Element("info").Value;
                        state.Error = xmlState.Element("error").Value;
                    }

                }
                tableOfTransitions[int.Parse(xmlState.Attribute("id").Value)] = state;
            }

            return tableOfTransitions;
        }
    }
}
