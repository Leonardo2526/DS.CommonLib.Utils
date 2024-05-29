using DS.ClassLib.VarUtils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class BenchmarkTestProgram
    {
        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
             .WriteTo.Console()
             .CreateLogger();
            SimpleBenchmark.Logger = logger;
            var result = SimpleBenchmark.Run(() => GetValues(), 100, 0.15, true);
            Console.WriteLine($"WarmUp time is:  {result.WarmUpTime.Milliseconds} ms");
            Console.WriteLine($"Action time is:  {result.ActionTime.Milliseconds} ms");
            Console.WriteLine($"Total time is:  {(int)result.TotalTime.TotalMilliseconds} ms");
            Console.ReadLine();
        }

        private static void TestMethod1()
        {
            Thread.Sleep(10);
        }

        public static List<int> GetValues()
        {
            int count = 100000;
            int[] numbersArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                numbersArray[i] = i;
            }

            //Print.PrintIndexAndValues(numbersArray);

            List<int> outlist = new List<int>();

            for (int i = 5; i < 200000; i++)
            {
                int c = Array.BinarySearch(numbersArray, i);
                //int c = Array.IndexOf(numbersArray, i);
                if (c > 0)
                    outlist.Add(c);
            }

            return outlist;
        }
    }
}
