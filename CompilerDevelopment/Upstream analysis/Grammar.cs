using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerDevelopment.Upstream_analysis
{
    static class Grammar
    {
        public static Dictionary<NonTerminal, List<Node>> MyGrammar = new Dictionary<NonTerminal, List<Node>>();

        public static void Loading()
        {
            XDocument xDocument = XDocument.Load("TableOfRelation.xml");
            XElement root = xDocument.Element("grammar");
            NonTerminal ntKey = null;
            List<Node> nodesTmp = null;
            List<Element> elementsTmp = null;
            Node nodeTmp = null;
            foreach (var link in root.Elements())
            {
                foreach (var side in link.Elements())
                {
                    if (side.Name == "left")
                    {
                        ntKey = new NonTerminal(side.Value);
                    }
                    else
                    {
                        nodesTmp = new List<Node>();
                        foreach (var node in side.Elements())
                        {
                            elementsTmp = new List<Element>();
                            foreach (var n in node.Elements())
                            {
                                Element elemTmp;
                                if (n.FirstAttribute.Value == "Terminal")
                                {
                                    elemTmp = new Terminal(n.Value);
                                }
                                else
                                {
                                    elemTmp = new NonTerminal(n.Value);
                                }
                                elementsTmp.Add(elemTmp);
                            }
                            nodeTmp = new Node(elementsTmp);
                            nodesTmp.Add(nodeTmp);
                        }
                    }
                }
                MyGrammar[ntKey] = nodesTmp;
                //nodesTmp.Clear();
            }


        }

        public static List<string> GetAllElementsDictionary()
        {
            List<string> AllElements = new List<string>();
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in MyGrammar)
            {
                if (AllElements.Contains(KeyValue.Key.Name)) // проверяем левую часть грамматики
                {

                }
                else
                {
                    AllElements.Add(KeyValue.Key.Name);
                }
                foreach (var node in KeyValue.Value) //проверяем правую часть грамматику по ключу
                {
                    foreach (var elem in node.elements)
                    {
                        if (AllElements.Contains(elem.Name))
                        {

                        }
                        else
                        {
                            AllElements.Add(elem.Name);
                        }
                    }
                }
            }
            return AllElements;
        }

        public static List<string> GetAllElementsStartWithNonTerminal()
        {
            List<string> AllElementsStartWithNonTerminal = new List<string>();
            AllElementsStartWithNonTerminal = GetAllNonTerminalFirslty();
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in MyGrammar)
            {

                foreach (var node in KeyValue.Value) //проверяем правую часть грамматику по ключу
                {
                    foreach (var elem in node.elements)
                    {
                        if (AllElementsStartWithNonTerminal.Contains(elem.Name))
                        {

                        }
                        else
                        {
                            AllElementsStartWithNonTerminal.Add(elem.Name);
                        }
                    }
                }
            }
            return AllElementsStartWithNonTerminal;

        }

        public static List<string> GetAllNonTerminalFirslty()
        {
            List<string> AllNonTerminalElements = new List<string>();
            foreach (KeyValuePair<NonTerminal, List<Node>> KeyValue in MyGrammar)
            {
                if (AllNonTerminalElements.Contains(KeyValue.Key.Name)) // проверяем левую часть грамматики
                {

                }
                else
                {
                    AllNonTerminalElements.Add(KeyValue.Key.Name);
                }
            }
            return AllNonTerminalElements;
        }

    }
}
