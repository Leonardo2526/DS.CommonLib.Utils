using DS.ClassLib.VarUtils.Points;
using MoreLinq;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var corners = rectangle.GetCorners().ToList();
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
        /// Get <paramref name="rectangle"/>'s corners.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns>
        /// Ordered list of <paramref name="rectangle"/>'s corners.
        /// </returns>
        public static IEnumerable<Point3d> GetCorners(this Rectangle3d rectangle)
        => new List<Point3d>()
        { rectangle.Corner(0), rectangle.Corner(1), rectangle.Corner(2), rectangle.Corner(3) };

        /// <summary>
        /// Get the min <see cref="Box"/> from <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns>
        /// The smallest <see cref="Box"/> that contains a set of <paramref name="rectangle"/> corners.
        /// </returns>
        public static Box GetMinBox(this Rectangle3d rectangle)
            => new(rectangle.Plane, rectangle.GetCorners());

        /// <summary>
        /// Get the <see cref="Box"/> from <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="offset">Offset by <paramref name="rectangle"/>'s plane normal.</param>
        /// <returns>
        /// The <see cref="Box"/> that contains a set of <paramref name="rectangle"/> corners 
        /// moved on <paramref name="offset"/> from <paramref name="rectangle"/>'s both plane sides.
        /// </returns>
        public static Box GetExtendedBox(this Rectangle3d rectangle, double offset = 0)
        {
            if(offset == 0) { return GetMinBox(rectangle); }

            var plane = rectangle.Plane;

            var corners = rectangle.GetCorners().ToList();
            var extCorners = new List<Point3d>()
                {
                    corners[0], corners[1]
                };
            var translationVector = Vector3d.Multiply(plane.Normal, offset);
            var t1 = Transform.Translation(translationVector);
            var t1Inverse = Transform.Translation(-translationVector);

            var ce2 = corners[2];
            var ce3 = corners[3];

            ce2.Transform(t1);
            extCorners.Add(ce2);
            ce3.Transform(t1Inverse);
            extCorners.Add(ce3);
            return new Box(plane, extCorners);
        }

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

        /// <summary>
        /// Check if <paramref name="plane1"/> is coplanar with <paramref name="plane2"/>.
        /// </summary>
        /// <param name="plane1"></param>
        /// <param name="plane2"></param>
        /// <returns>
        /// <see langword="true"/> if planes are coplanar.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsCoplanar(this Plane plane1, Plane plane2)
            => plane1.Normal.IsParallelTo(plane2.Normal, 1.DegToRad()) != 0 
            && plane1.DistanceTo(plane2.Origin) < 0.001;

        /// <summary>
        /// Try to get type of <paramref name="plane"/> in Cartesian coordinate system.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// XY, YZ or XZ type if <paramref name="plane"/>'s normal is parallel to one of 
        /// Cartesian coordinate system axis.
        /// <para>
        /// Otherwise default value.
        /// </para>
        /// </returns>
        public static OrthoPlane TryGetOrthoType(this Plane plane,
         double tolerance = 0.0174533)
        {
            var normal = plane.Normal;
            OrthoPlane orthoPlane = default;
            if (normal.IsParallelTo(Vector3d.XAxis, tolerance) != 0)
            { orthoPlane = OrthoPlane.YZ; }
            else if (normal.IsParallelTo(Vector3d.YAxis, tolerance) != 0)
            { orthoPlane = OrthoPlane.XZ; }
            else if (normal.IsParallelTo(Vector3d.ZAxis, tolerance) != 0)
            { orthoPlane = OrthoPlane.XY; }

            return orthoPlane;
        }

        /// <summary>
        /// Try to get type of <paramref name="curve"/>'s <see cref="Plane"/> 
        /// in Cartesian coordinate system.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// XY, YZ or XZ type if <paramref name="curve"/>'s <see cref="Plane"/>
        /// normal is parallel to one of 
        /// Cartesian coordinate system axis.
        /// <para>
        /// Otherwise default value.
        /// </para>
        /// </returns>
        public static OrthoPlane TryGetPlaneOrthoType(this Curve curve,
         double tolerance = 0.0174533)
         => curve.TryGetPlane(out var plane) ?
         TryGetOrthoType(plane, tolerance) :
         default;

        /// <summary>
        /// Get extrusion lines of <paramref name="box"/> that represent
        /// lines between two sides of <paramref name="box"/>'s plane.
        /// </summary>
        /// <param name="box"></param>
        /// <returns>
        /// The collection of four <see cref="Line"/>s.
        /// </returns>
        public static IEnumerable<Line> GetExtrusionLines(this Box box)
        {
            var corners = box.GetCorners();
            var corners1 = corners.Take(4);
            var corners2 = corners.TakeLast(4);

            var lines = new List<Line>();
            for (int i = 0; i < 4; i++)
            {
                var l1 = new Line(corners1.ElementAt(i), corners2.ElementAt(i));
                lines.Add(l1);
            }
            return lines;
        }
    }
}
