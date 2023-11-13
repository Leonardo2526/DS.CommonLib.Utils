using Castle.Core.Internal;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents tools for boolean operations with <see cref="Line"/>s.
    /// </summary>
    public static class LineBoolTools
    {
        /// <summary>
        /// Find intersection between <paramref name="line1"/> ands <paramref name="line2"/>.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// A new <see cref="Line"/> that has points that <paramref name="line1"/> and <paramref name="line2"/> contains.
        /// <para>
        /// <see cref="Line"/> default value if lines are not parallel.
        /// </para>
        /// </returns>
        public static Line Intersect(Line line1, Line line2, double tolerance = 0.001)
        {
            var points1 = TryGetInsidePoints(line1.From, line1.To, line2, tolerance);
            if (points1.Count() == 2) { return new Line(points1[0], points1[1]); }            

            var points2 = TryGetInsidePoints(line2.From, line2.To, line1, tolerance);
            if (points2.Count() == 2) { return new Line(points2[0], points2[1]); }

            var allPoins = new List<Point3d>();
            points1.ForEach(allPoins.Add); 
            points2.ForEach(allPoins.Add);

            if(allPoins.Count <2) { return default; }

            return new Line(allPoins[0], allPoins[1]);

            static List<Point3d> TryGetInsidePoints(Point3d point1, Point3d point2, Line line, double tolerance)
            {
                var points = new List<Point3d>();

                if (line.Contains(point1, tolerance))
                { points.Add(point1); }
                if (line.Contains(point2, tolerance))
                { points.Add(point2); }

                return points;
            }
        }

    }
}
