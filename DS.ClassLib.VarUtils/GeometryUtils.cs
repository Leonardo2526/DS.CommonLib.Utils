using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Useful utils for geometry elements.
    /// </summary>
    public static class GeometryUtils
    {
        /// <summary>
        /// Try to create the <see cref="Rectangle3d"/> from <paramref name="lines"/> min and max points.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="rectangle"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="lines"/> are closed and coplanar.
        /// <para>
        /// Otherwise <see langword="false"/> .
        /// </para>
        /// </returns>
        public static bool TryCreateRectangle(IEnumerable<Line> lines, out Rectangle3d rectangle)
        {
            rectangle = default;

            var polyLine = ToPolyLine(lines);
            if(!polyLine.IsClosed || 
                !polyLine.ToPolylineCurve().TryGetPlane(out var plane)) { return false; }

            polyLine.MergeColinearSegments(1.DegToRad(), true);
            var points = polyLine.ToList();
            var box = new Box(plane, points);
            var boxCorners = box.GetCorners();
            rectangle = new Rectangle3d(box.Plane, boxCorners[0], boxCorners[2]);
            return rectangle.IsValid;
        }

        /// <summary>
        /// Convert <paramref name="lines"/> to <see cref="Polyline"/>.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="shortElementsTolerance"></param>
        /// <returns>
        /// <see cref="Polyline"/> created by <paramref name="lines"/> start and end points.
        /// </returns>
        public static Polyline ToPolyLine(IEnumerable<Line> lines, int shortElementsTolerance = 3)
        {
            var points = new List<Point3d>();
            lines.ToList().ForEach(l => { points.Add(l.From); points.Add(l.To); });
            var polyline = new Polyline(points);
            polyline.DeleteShortSegments(Math.Pow(0.1, shortElementsTolerance));
            return polyline;
        }
    }
}
