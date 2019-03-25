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

        public static bool TokenIsContained(string token)
        {
            if (tableOfLabels.Count > 0)
            {
                foreach (KeyValuePair<int, (string , int)> KeyValue in tableOfLabels)
                {
                    if (KeyValue.Value.Item1 == token)
                    {
                        return true;
                    }
                    else if((KeyValue.Value.Item1 + ":")== token)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
       public static void UpdateTable(string inputToken, List<string> polis)
        {
            //Dictionary<int, (string, int)> tmp = new Dictionary<int, (string, int)>();

            (string, int) save = (null, 0);
            (string, int) save1 = (null, 0);
            int key = 0;

            foreach (KeyValuePair<int, (string, int)> KeyValue in TableOfLabels.tableOfLabels)
            {
                if (inputToken == KeyValue.Value.Item1)
                {
                    //tmp[KeyValue.Key] = (KeyValue.Value.Item1, polis.Count());
                    save1 = (KeyValue.Value.Item1, polis.Count());
                    tableOfLabels.TryGetValue(KeyValue.Key, out save);
                    {
                        key = KeyValue.Key;
                        save = save1;
                    }
                }
            }

            tableOfLabels[key] = save1;


        }
    }
}
