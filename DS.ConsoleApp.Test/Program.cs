using DS.ConsoleApp.Test.RhinoTests;
using System;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            new LinesIntersectionTest()
                 .BoolIntersect();

            //new PointsOrderTest().OrderByDistance();

            Console.ReadLine();
        }
    }
}
