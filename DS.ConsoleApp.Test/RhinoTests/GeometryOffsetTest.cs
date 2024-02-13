using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class GeometryOffsetTest
    {
        public static void Run1()
        {
            var p01 = new Point3d(0, 0, 0);  
            var p02 = new Point3d(3, 2, 0);

            
            Plane plane = Plane.WorldXY;
            Rectangle3d rect = new Rectangle3d(plane, p01, p02);
            var p1 = rect.BoundingBox.Min;
            var p2 = rect.BoundingBox.Max;

            Console.WriteLine(p1);
            Console.WriteLine(p2);

            var x = rect.Plane.XAxis;
            var y = rect.Plane.YAxis;
            var offset = 4;
            var vector = Vector3d.Multiply(offset, x) + Vector3d.Multiply(offset, y);
            //vector.Unitize();

            var p11 = p1 - vector;
            var p12 = p2 + vector;

            var b = GeometryExtensions.TryExtend(rect, offset, out var rect1);
            var b11 = rect1.BoundingBox.Min;
            var b21 = rect1.BoundingBox.Max;

            Console.WriteLine(b);
            Console.WriteLine(b11);
            Console.WriteLine(b21);

            var lines = rect1.ToLines();
            lines.ForEach(line => Console.WriteLine(line));
        }

       public static void Run2()
        {
            Plane plane = Plane.WorldXY;
            var radius = 2;

            Circle circle0 = new Circle(plane, radius);
            Console.WriteLine(circle0.Center);
            Console.WriteLine(circle0.Radius);

            var offset = 3;

            var b = circle0.Extend(offset, out var circle01);
            Console.WriteLine(b);
            Console.WriteLine(circle01.Center);
            Console.WriteLine(circle01.Radius);
        }
    }
}
