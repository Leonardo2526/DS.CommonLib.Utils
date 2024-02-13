using DS.PathFinder;
using DS.PathFinder.Algorithms.Enumeratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.PathFindTest
{
    internal class StepEnumeratorTest
    {
        private readonly TestNodeBuilder _nodeBuilder;

        public StepEnumeratorTest()
        {
            _nodeBuilder = new TestNodeBuilder();
            Run();
        }

        public void Run()
        {
            int i = 0;
            double maxModelStep = 500;
            //var enumerator = new StepEnumerator(_nodeBuilder, maxModelStep, true);
            //while(enumerator.MoveNext())
            //{
            //    i++;
            //    Console.WriteLine(i + " - "+ enumerator.Current);
            //}
        }
    }
}
