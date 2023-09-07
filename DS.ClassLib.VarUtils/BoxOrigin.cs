using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    public static class BoxOrigin
    {
        private static readonly Point3d _origin = Point3d.Origin;
        private static readonly double _width = double.MaxValue;
        private static readonly double _height = double.MaxValue;
        private static readonly Point3d _minPoint = new(double.MinValue, double.MinValue, double.MinValue);
        private static readonly Point3d _maxPoint = new(double.MaxValue, double.MaxValue, double.MaxValue);

        static BoxOrigin()
        {
            XYquadrants = GetQuadrants(_origin, BasisOrigin.X, BasisOrigin.Y, _width, _height);
            XZquadrants = GetQuadrants(_origin, BasisOrigin.Z, BasisOrigin.X, _width, _height);
            YZquadrants = GetQuadrants(_origin, BasisOrigin.Y, BasisOrigin.Z, _width, _height);
            Quadrants = new Rectangle3d[12]
            {
                XYquadrants[0], XYquadrants[1], XYquadrants[2], XYquadrants[3],
                XZquadrants[0], XZquadrants[1], XZquadrants[2], XZquadrants[3],
                YZquadrants[0], YZquadrants[1], YZquadrants[2], YZquadrants[3]
            };

            Octants = GetOctants(_origin, BasisOrigin, double.MaxValue);
        }

        private static IBasis<Vector3d> BasisOrigin { get; } = new Basis3dOrigin();
        public static BoundingBox BoundingBox { get; } = new BoundingBox(_minPoint, _maxPoint);
        public static BoundingBox[] Octants { get; }
        public static Rectangle3d[] Quadrants { get; }
        public static Rectangle3d[] XYquadrants { get; }
        public static Rectangle3d[] XZquadrants { get; }
        public static Rectangle3d[] YZquadrants { get; }

        private static Rectangle3d[] GetQuadrants(Point3d origin, Vector3d basisX, Vector3d basisY,
             double width, double height)
        {
            var qXY1 = GetQuadrant(origin, basisX, basisY, width, height);
            var qXY2 = GetQuadrant(origin, basisY, -basisX, width, height);
            var qXY3 = GetQuadrant(origin, -basisX, -basisY, width, height);
            var qXY4 = GetQuadrant(origin, -basisY, basisX, width, height);

            return new Rectangle3d[4] { qXY1, qXY2, qXY3, qXY4 };

            static Rectangle3d GetQuadrant(Point3d origin, Vector3d basisX, Vector3d basisY,
                 double width, double height)
            {
                var plane = new Plane(origin, basisX, basisY);
                return new Rectangle3d(plane, width, height);
            }
        }

        private static BoundingBox[] GetOctants(Point3d origin, IBasis<Vector3d> basis, double diagonalLength)
        {
            var o1 = GetOctant(origin, basis, diagonalLength);
            var o2 = GetOctant(origin, new Basis3d(origin, -basis.X, basis.Y, basis.Z), diagonalLength);
            var o3 = GetOctant(origin, new Basis3d(origin, -basis.X, -basis.Y, basis.Z), diagonalLength);
            var o4 = GetOctant(origin, new Basis3d(origin, basis.X, -basis.Y, basis.Z), diagonalLength);
            var o5 = GetOctant(origin, new Basis3d(origin, basis.X, basis.Y, -basis.Z), diagonalLength);
            var o6 = GetOctant(origin, new Basis3d(origin, -basis.X, basis.Y, -basis.Z), diagonalLength);
            var o7 = GetOctant(origin, new Basis3d(origin, -basis.X, -basis.Y, -basis.Z), diagonalLength);
            var o8 = GetOctant(origin, new Basis3d(origin, basis.X, -basis.Y, -basis.Z), diagonalLength);

            return new BoundingBox[8] { o1, o2, o3, o4, o5, o6, o7, o8 };

            static BoundingBox GetOctant(Point3d origin, IBasis<Vector3d> basis, double diagonalLength)
            {
                var diagonalVector = basis.X + basis.Y + basis.Z;
                diagonalVector = Vector3d.Divide(diagonalVector, diagonalVector.Length);
                diagonalVector *= diagonalLength;

                var p1 = origin;
                var p2 = origin + diagonalVector;

                (Point3d minPoint, Point3d maxPoint) =
                PointsUtils.GetMinMax(new List<Point3d> { p1, p2 });

                return new BoundingBox(minPoint, maxPoint);
            }
        }
    }
}
