using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.DijkstraAlgorithmLab7
{
    public static class TableOfLabels
    {
        public static Dictionary<int, (string, int)> tableOfLabels { get; set; } = new Dictionary<int, (string, int)>(); 

        public static string GenerateNewLabel()
        {
            if (tableOfLabels.Count == 0)
            {
                tableOfLabels.Add(1, ("m1", 0));
            }
            else
            {
                int lastElementInTable = tableOfLabels.Count;
                tableOfLabels.Add(lastElementInTable + 1, ("m" + (lastElementInTable + 1), 0));
            }
            return tableOfLabels.Last().Value.Item1; // возвращаю название метки
        }
    }
}
