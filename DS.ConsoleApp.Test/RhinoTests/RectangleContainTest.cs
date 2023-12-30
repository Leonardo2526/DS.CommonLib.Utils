using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class RectangleContainTest
    {

        public static void Run1()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10,10,0));    
            var r2 = GetXYRectangle(new Point3d(1,0,0), new Point3d(2,2,0));
            var r3 = GetXYZRectangle(new Point3d(), new Point3d(2,2,2));
            var c1 = r3.Corner(0);
            var c2 = r3.Corner(1);
            var c3 = r3.Corner(2);
            var c4 = r3.Corner(3);


            var testPoint = new Point3d(0,0,0);
            Console.WriteLine(r1.Contains(testPoint));
            testPoint = new Point3d(-1,0,0);
            Console.WriteLine(r1.Contains(testPoint));
            testPoint = new Point3d(1, 1, 0);
            Console.WriteLine(r1.Contains(testPoint));
            testPoint = new Point3d(1, 1, 1);
            Console.WriteLine(r1.Contains(testPoint));
        }

        private static Rectangle3d GetXYRectangle(Point3d p1, Point3d p2)
        {
            var origin = Point3d.Origin;
            var v1 = Vector3d.XAxis;
            var v2 = Vector3d.YAxis;

            var plane = new Plane(origin, v1, v2);

            return new Rectangle3d(plane, p1, p2);
        }

        private static Rectangle3d GetXYZRectangle(Point3d p1, Point3d p2)
        {
            var origin = Point3d.Origin;
            var v1 = Vector3d.XAxis;
            var v2 = Vector3d.YAxis;
            var x = v1 + v2;
            x.Unitize();

            var plane = new Plane(origin, x, Vector3d.ZAxis);

            return new Rectangle3d(plane, p1, p2);
        }
    }
}
