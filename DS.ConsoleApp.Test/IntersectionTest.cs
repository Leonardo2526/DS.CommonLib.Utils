using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class IntersectionTest
    {
        private double _tolerance = 0.001;

        public IntersectionTest()
        {
            (Point3d point1, Point3d point2, bool intersection) = Run();
            Console.WriteLine("Intersection is: " + intersection);
            Console.WriteLine("Intersection point on line1 is: " + point1);
            Console.WriteLine("Intersection point on line2 is: " + point2);
        }

        private (Point3d point1, Point3d point2, bool intersection) Run()
        {
            Point3d p11 = new Point3d(0, 0, 0);
            Point3d p12 = new Point3d(5, 0, 0);
            Line line1 = new Line(p11, p12);


            Point3d p21 = new Point3d(20, 2, 0);
            Point3d p22 = new Point3d(20, -2, 0);
            Line line2 = new Line(p21, p22);

            var intersection = Intersection.LineLine(line1, line2, out double a, out double b, _tolerance, true);
            Point3d point1 = line1.PointAt(a);
            Point3d point2 = line2.PointAt(b);

            return (point1, point2, intersection);    
        }
    }
}
