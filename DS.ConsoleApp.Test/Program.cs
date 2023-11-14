using DS.ConsoleApp.Test.RhinoTests;
using System;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            new LinesBooleanTests()
                 .BoolSubstractMultiple();
                 //.BoolSubstract();
            //.BoolIntersect();

            //new PointsOrderTest().OrderByDistance();

            Console.ReadLine();
        }
    }
}
