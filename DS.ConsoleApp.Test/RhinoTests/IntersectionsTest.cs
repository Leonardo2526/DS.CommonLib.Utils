using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class IntersectionsTest
    {
        public static void Run1()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 10, 0));
            var r2 = GetXYRectangle(new Point3d(1, 0, 0), new Point3d(2, 2, 0));

            var p1 = r1.Plane;
            var p2 = r2.Plane;
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
