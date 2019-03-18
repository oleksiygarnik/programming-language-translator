using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.PolisTableByDijkstra
{
    class State
    {
        public Dictionary<string, (int? stack, int destination)> dictionary = new Dictionary<string, (int? stack, int destination)>();
        public bool isFinal;
        public string Info { get; set; }
        public string Error { get; set; }
    }
}
