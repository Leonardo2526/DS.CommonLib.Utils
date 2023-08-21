using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Points
{
    public static class PointsUtils
    {
        public static PointModel GetAverage(List<PointModel> points)
        {
            int x = (int)points.Select(p => p.X).Average();
            int y = (int)points.Select(p => p.Y).Average();
            int z = (int)points.Select(p => p.Z).Average();

            return new PointModel(x, y, z);
        }

        /// <summary>
        /// Get min and max points from <paramref name="points"/>.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static (Point3d minPoint, Point3d maxPoint) GetMinMax(List<Point3d> points)
        {
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            List<double> zlist = new List<double>();

            foreach (Point3d point in points)
            {
                xlist.Add(point.X);
                ylist.Add(point.Y);
                zlist.Add(point.Z);
            }

            var minPoint = new Point3d(xlist.Min(a => a), ylist.Min(a => a), zlist.Min(a => a));
            var maxPoint = new Point3d(xlist.OrderByDescending(a => a).First(),
                ylist.OrderByDescending(a => a).First(), zlist.OrderByDescending(a => a).First());

            return (minPoint, maxPoint);
        }

        /// <summary>
        /// Get bound points by <paramref name="point1"/>, <paramref name="point2"/> and <paramref name="moveVector"/>.
        /// </summary>
        /// <remarks>
        /// Min and max points between <paramref name="point1"/> and <paramref name="point2"/> to find bound will be set automatically.
        /// </remarks>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="moveVector"></param>
        /// <returns>
        /// Min and max points moved by <paramref name="moveVector"/>. 
        /// </returns>
        public static (Point3d minBoundPoint, Point3d maxBoundPoint) GetBound(Point3d point1, Point3d point2, Vector3d moveVector)
        {
            var points = new List<Point3d>() { point1, point2 };
            (Point3d minPoint, Point3d maxPoint) = GetMinMax(points);

            var p11 = minPoint + moveVector;
            var p12 = minPoint - moveVector;
            (Point3d minP1, Point3d maxP1) = GetMinMax(new List<Point3d> { p11, p12 });

            var p21 = maxPoint + moveVector;
            var p22 = maxPoint - moveVector;
            (Point3d minP2, Point3d maxP2) = GetMinMax(new List<Point3d> { p21, p22 });

            return (minP1, maxP2);
        }
    }
   
}
