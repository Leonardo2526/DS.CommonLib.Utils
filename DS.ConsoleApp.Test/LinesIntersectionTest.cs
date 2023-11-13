using Rhino.Geometry.Intersect;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils;

namespace DS.ConsoleApp.Test
{
    internal class LinesIntersectionTest
    {

        public LinesIntersectionTest()
        {
        }

        public LinesIntersectionTest Run()
        {
            var length = 10000;

            var p1 = new Point3d(0, 0, 0);
            var dir1 = new Vector3d(1, 0, 0);
            var l1 = new Line(p1, dir1, length);

            var p2 = new Point3d(5, 5, 0);
            var dir2 = new Vector3d(0, -1, 0);
            var l2 = new Line(p2, dir2, length);


            var intersection = Intersection.LineLine(l1, l2, out double a, out double b, 0.001, true);
            Console.WriteLine(intersection);
            Console.WriteLine(l1.PointAt(a));
            Console.WriteLine(l2.PointAt(b));

            return this;
        }

        public LinesIntersectionTest BoolIntersect()
        {
            var line1 = new Line(new Point3d(-1,0,0), new Point3d(5, 0, 0));
            var line2 = new Line(new Point3d(-2, 0, 0), new Point3d(3, 0, 0));

            var intersectionLine = LineBoolTools.Intersect(line1, line2);
            Console.WriteLine(intersectionLine.ToString());
            return this;
        }
    }
}
