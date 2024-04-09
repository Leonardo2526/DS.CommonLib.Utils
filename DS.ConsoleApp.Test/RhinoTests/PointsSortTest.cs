using DS.ClassLib.VarUtils.Points;
using MoreLinq;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class PointsSortTest
    {
        public static void Run1()
        {
            var points = new List<Point3d>()
            {
                new Point3d(0,0, 0),
                new Point3d(1,0, 0.0000009),
                new Point3d(2,0, 0),
                new Point3d(1,0, 0.0000008),
                new Point3d(2,0, 0),
            };

            var pointComparer = new PointComparer();
            var distinctPoints = points.Distinct(pointComparer).ToList();
            Console.WriteLine("Distinct points");
            distinctPoints.ForEach(p => Console.WriteLine(p));

            var intersectionPoints = points.GroupBy(p => p)
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key)
                    .ToList();
            Console.WriteLine("\nIntersection points");
            intersectionPoints.ForEach(p => Console.WriteLine(p));
        }
    }

    internal class PointComparer : IEqualityComparer<Point3d>
    {
        public bool Equals(Point3d x, Point3d y)
        {
            return x.IsAlmostEqualTo(y);
        }

        public int GetHashCode(Point3d obj)
        {
            return obj.GetHashCode();
        }
    }
}
