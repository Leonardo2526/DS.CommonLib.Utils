﻿using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Filters;
using DS.ConsoleApp.Test.RhinoTests;
using DS.ConsoleApp.Test.RhinoTests.Rectangles;
using Rhino;
using Rhino.Geometry;
using System;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            double v = RhinoMath.UnitScale(UnitSystem.Millimeters, UnitSystem.Feet);
            Console.WriteLine(304 * v);
            //Console.WriteLine(p1.IsCoplanar(p2));

            //ConvertToRectangleTest.Convert2();
            //IntersectionsTest.Run1();
            //RoolsFilterFactoryTest.Test1();
            //ActionTwoWayEnumeratorTest.StringIterator();
            //Basis3dTests.TryCreateTransforms1();
            //Basis3dTests.ConvertBasis();
            //Basis3dTests.TestBasisOrthonormal();


            //var test = new MultiResolverTest()
            //    .CreateResolver();

            //test.Resolve();
            //Task.Run(() => test.ResolveAsync().Wait());


            //new LinesBooleanTests()
            //     .BoolSubstractMultiple();
            //.BoolSubstract();
            //.BoolIntersect();

            //new PointsOrderTest().OrderByDistance();

            Console.ReadLine();
        }
    }
}
