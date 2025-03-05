using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDataAggregator
{
    public static class App
    {
        public static void Run ()
        {
            var textFile = new TextFileCreator();
            textFile.WriteTicketsToTextFile();
            string path = textFile.BuildFilePath();
            Console.WriteLine($"Result saved to {path}");
        }
    }
}
