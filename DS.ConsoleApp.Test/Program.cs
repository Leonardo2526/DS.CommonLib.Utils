using DS.ConsoleApp.Test.RhinoTests;
using System;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var test = new MultiResolverTest()
                .CreateResolver();

            //test.Resolve();
            Task.Run(() => test.ResolveAsync().Wait());


            //new LinesBooleanTests()
            //     .BoolSubstractMultiple();
            //.BoolSubstract();
            //.BoolIntersect();

            //new PointsOrderTest().OrderByDistance();

            Console.ReadLine();
        }
    }
}
