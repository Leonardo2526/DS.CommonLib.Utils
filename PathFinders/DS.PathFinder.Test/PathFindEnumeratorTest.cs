using DS.PathFinder;
using DS.PathFinder.Algorithms.Enumeratos;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.PathFindTest
{
    internal class PathFindEnumeratorTest
    {
        private readonly TestNodeBuilder _nodeBuilder;

        public PathFindEnumeratorTest()
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.Debug()
                   .WriteTo.Console()
                   .CreateLogger();

            _nodeBuilder = new TestNodeBuilder()
            {
                Heuristic = 100
            };
            Run();
        }

        private void Run()
        {
            var dummyFactory = new DummyAlgorithmFactory();

            double maxModelStep = 2500;
            //var stepEnumerator = new StepEnumerator(_nodeBuilder, maxModelStep, true);
            //var heuristicEnumerator = new HeuristicEnumerator(_nodeBuilder, false);
            //var toleranceEnumerator = new ToleranceEnumerator(dummyFactory, true);
            //var pathFindEnumerator = new PathFindEnumerator(stepEnumerator, heuristicEnumerator, toleranceEnumerator, dummyFactory);

            //while (pathFindEnumerator.MoveNext())
            //{ var path = pathFindEnumerator.Current; }
        }
    }
}
