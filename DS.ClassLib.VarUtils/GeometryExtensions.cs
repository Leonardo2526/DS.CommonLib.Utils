using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Extensions methods for geomtry objetcs like <see cref="Rectangle3d"/>, <see cref="Circle"/> etc.
    /// </summary>
    public static class GeometryExtensions
    {
        /// <summary>
        /// Try to extend <paramref name="rectangle"/> on <paramref name="offset"/>.
        /// <para>
        /// Reduced <paramref name="rectangle"/> if <paramref name="offset"/> is less 0.
        /// </para>
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="offset"></param>
        /// <param name="resultRectangle"></param>
        /// <returns>
        /// <see langword="true"/> if extend and reduce was succeful.
        /// <para>
        /// <see langword="false"/> if reduce was failed: 
        /// <paramref name="offset"/> is more than <paramref name="rectangle"/>'s half of width or height.
        /// </para>
        /// </returns>
        public static bool TryExtend(this Rectangle3d rectangle, double offset, out Rectangle3d resultRectangle)
        {
            resultRectangle = default;
            if (offset <= -rectangle.Width / 2 || offset <= -rectangle.Height / 2) { return false; }

            var corners = new List<Point3d>();
            for (int i = 0; i < 4; i++)
            {corners.Add(rectangle.Corner(i));}
            var p01 = corners[0];
            var p02 = corners[2];

            var x = corners[1] - corners[0];
            x.Unitize();
            var y = corners[3] - corners[0];
            y.Unitize();

            var vector = Vector3d.Multiply(offset, x) + Vector3d.Multiply(offset, y);

            var p11 = p01 - vector;
            var p12 = p02 + vector;

            resultRectangle = new Rectangle3d(rectangle.Plane, p11, p12);
            return true;
        }

        /// <summary>
        /// Convert <paramref name="rectangle"/> to <see cref="Line"/>s.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns>
        /// <see cref="Line"/>s as <paramref name="rectangle"/> segments.
        /// </returns>
        public static List<Rhino.Geometry.Line> ToLines(this Rectangle3d rectangle)
        => rectangle.ToPolyline().GetSegments().ToList();

        /// <summary>
        /// Try to increse/reduce <paramref name="circle"/>'s radius on <paramref name="offset"/>.       
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="offset"></param>
        /// <param name="resultCircle"></param>
        /// <returns>
        /// <see langword="true"/> if extend and reduce was succeful.
        /// <para><see langword="false"/> if reduce was failed: 
        /// <paramref name="offset"/> is more than <paramref name="circle"/>'s radius.</para>
        /// </returns>
        public static bool Extend(this Circle circle, double offset, out Circle resultCircle)

        {
            resultCircle = default;
            if (offset <= circle.Radius) { return false; }
            resultCircle = new(circle.Plane, circle.Radius + offset);
            return true;
        }
    }
}
