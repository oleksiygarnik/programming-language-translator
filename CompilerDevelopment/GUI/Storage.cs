using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDevelopment.GUI
{
    static class Storage
    {
        public static string StringTemplate { get; set; }

        public static Dictionary<string, string> valueForIdentifiers { get; set; } = new Dictionary<string, string>();

        public static string Expression { get; set; }
    }
}
