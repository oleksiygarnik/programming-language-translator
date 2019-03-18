using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.DijkstraAlgorithmLab7
{
    public class Raw
    {
        public int Step { get; set; }

        public string Command { get; set; }

        public string Action { get; set; }

        public List<string> Polis { get; set; }
    }

    public static class TableForExecutePolis
    {
        public static List<Raw> tableForExecutePolis { get; set; } = new List<Raw>();

        public static string ExecutePolis()
        {
            int step = 0;

            Queue<string> tokenQueue = new Queue<string>();
            //var list = TableOfUpstreamParsing.tableOfUpstreamParsing.Last().Polis;

            List<string> polis = PolisTableByDijkstraAlgo.polisTableByDijkstraAlgo.Last().Polis;

            for (int i = 0; i < polis.Count(); i++)
            {
                tokenQueue.Enqueue(polis[i].ToString());
            }

            List<string> tmpPolis = new List<string>(polis);

            Raw raw = new Raw()
            {
                Step = step++,
                Command = null,
                Action = null,
                Polis = tmpPolis
            };

            tableForExecutePolis.Add(raw);


            //Next ALGO for solutin

            return null;
        }
    }
}
