using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Cartesian coordinate system built by 
    /// <see cref="Vector3d.XAxis"/>, <see cref="Vector3d.YAxis"/> and <see cref="Vector3d.ZAxis"/> 
    /// with <see cref="Point3d.Origin"/> as origin point.
    /// </summary>
    public static class CCS
    {
        private static readonly double _width = double.MaxValue;
        private static readonly double _height = double.MaxValue;
        private static Point3d _origin = Point3d.Origin;
        private static Rectangle3d[] _quadrants;
        private static Octant[] _octants;

        /// <summary>
        /// Twelfe infinite regions that represent planes divided by half-axes.
        /// </summary>
        public static Rectangle3d[] Quadrants
        {
            get
            {
                _quadrants ??= GetQuadrants();
                return _quadrants;
            }
        }

        /// <summary>
        /// Eight octants that represent boxes divided by half-axes.
        /// </summary>
        public static Octant[] Octants
        {
            get
            {
                _octants ??= GetOctants(_origin, BasisOrigin, double.MaxValue);
                return _octants;
            }
        }

        /// <summary>
        /// Four quadants at XY plane.
        /// </summary>
        public static IEnumerable<Rectangle3d> XYquadrants =>
            Quadrants.
            ToList().
            Where(q => q.Plane.Normal.IsParallelTo(BasisOrigin.Z) != 0);

        /// <summary>
        /// Four quadants at XZ plane.
        /// </summary>
        public static IEnumerable<Rectangle3d> XZquadrants =>
            Quadrants.
            ToList().
            Where(q => q.Plane.Normal.IsParallelTo(BasisOrigin.Y) != 0);


        /// <summary>
        /// Four quadants at YZ plane.
        /// </summary>
        public static IEnumerable<Rectangle3d> YZquadrants =>
            Quadrants.
            ToList().
            Where(q => q.Plane.Normal.IsParallelTo(BasisOrigin.X) != 0);

        /// <summary>
        /// Three orthonormal vectors that represent axes.
        /// </summary>
        private static IBasis<Vector3d> BasisOrigin { get; } = new Basis3dOrigin();

        private static Rectangle3d[] GetQuadrants()
        {
            var xyQuadrants = Octants.
                ToList().
                Where(o => o.XYQuadrant.Plane.Normal.IsParallelTo(Vector3d.ZAxis) == 1).
                Select(o => o.XYQuadrant).ToList();

            var xzQuadrants = Octants.
               ToList().
               Where(o => o.XZQuadrant.Plane.Normal.IsParallelTo(Vector3d.YAxis) == 1).
               Select(o => o.XZQuadrant).ToList();

            var yzQuadrants = Octants.
            ToList().
            Where(o => o.YZQuadrant.Plane.Normal.IsParallelTo(Vector3d.XAxis) == 1).
              Select(o => o.YZQuadrant).ToList();

            var quadrants = new List<Rectangle3d>();
            quadrants.AddRange(xyQuadrants);
            quadrants.AddRange(xzQuadrants);
            quadrants.AddRange(yzQuadrants);
            return quadrants.ToArray();
        }

        private static Octant[] GetOctants(Point3d origin, IBasis<Vector3d> basis, double diagonalLength)
        {
            var o1 = GetOctant(origin, new Basis3d(origin, basis.X, basis.Y, basis.Z), diagonalLength);
            var o2 = GetOctant(origin, new Basis3d(origin, -basis.X, basis.Y, basis.Z), diagonalLength);
            var o3 = GetOctant(origin, new Basis3d(origin, -basis.X, -basis.Y, basis.Z), diagonalLength);
            var o4 = GetOctant(origin, new Basis3d(origin, basis.X, -basis.Y, basis.Z), diagonalLength);
            var o5 = GetOctant(origin, new Basis3d(origin, basis.X, basis.Y, -basis.Z), diagonalLength);
            var o6 = GetOctant(origin, new Basis3d(origin, -basis.X, basis.Y, -basis.Z), diagonalLength);
            var o7 = GetOctant(origin, new Basis3d(origin, -basis.X, -basis.Y, -basis.Z), diagonalLength);
            var o8 = GetOctant(origin, new Basis3d(origin, basis.X, -basis.Y, -basis.Z), diagonalLength);

            return new Octant[8] { o1, o2, o3, o4, o5, o6, o7, o8 };
        }

        private static Octant GetOctant(Point3d origin, Basis3d basis, double diagonalLength)
        {
            var diagonalVector = basis.X + basis.Y + basis.Z;
            diagonalVector = Vector3d.Divide(diagonalVector, diagonalVector.Length);
            diagonalVector *= diagonalLength;

            var p1 = origin;
            var p2 = origin + diagonalVector;

            (Point3d minPoint, Point3d maxPoint) =
            PointsUtils.GetMinMax(new List<Point3d> { p1, p2 });

            var box = new BoundingBox(minPoint, maxPoint);

            var quadrants = GetQuadrants(origin, basis, _width, _height);

            return new Octant(basis, quadrants, box);

            static Rectangle3d[] GetQuadrants(Point3d origin, Basis3d basis,
               double width, double height)
            {
                var qXY1 = GetQuadrant(origin, basis.X, basis.Y, basis.Z, width, height);
                var qXY2 = GetQuadrant(origin, basis.X, basis.Z, basis.Y, width, height);
                var qXY3 = GetQuadrant(origin, basis.Y, basis.Z, basis.X, width, height);

                return new Rectangle3d[3] { qXY1, qXY2, qXY3 };

                static Rectangle3d GetQuadrant(Point3d origin,
                    Vector3d basisX, Vector3d basisY, Vector3d normal,
                     double width, double height)
                {
                    (var x, var y) = Vector3d.CrossProduct(basisX, basisY).IsParallelTo(normal) == -1 ?
                         (basisY, basisX) : (basisX, basisY);
                    var plane = new Plane(origin, x, y);
                    return new Rectangle3d(plane, width, height);
                }
            }
        }

    }
}
