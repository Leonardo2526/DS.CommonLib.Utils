using DS.PathFinder;
using DS.PathFinder.Algorithms.Enumeratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.PathFindTest
{
    internal class HeuristicEnumeratorTest
    {
        private readonly TestNodeBuilder _nodeBuilder;

        public HeuristicEnumeratorTest()
        {
            _nodeBuilder = new TestNodeBuilder()
            {
                Heuristic = 100
            };
            Run();
        }

        public void Run()
        {
            int i = 0;
            var enumerator = new HeuristicEnumerator(_nodeBuilder, false);
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                i++;
                Console.WriteLine(i + " - "+ enumerator.Current);
            }

            Console.WriteLine("\nReset\n");
            i = 0;
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                i++;
                Console.WriteLine(i + " - " + enumerator.Current);
            }
        }
    }
}
