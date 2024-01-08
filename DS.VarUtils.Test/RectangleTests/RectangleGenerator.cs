using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.VarUtils.Test.RectangleTests
{
    internal static class RectangleFactory
    {
        private static Transform _xyToxz = Transform.PlaneToPlane(Plane.WorldXY, Plane.WorldZX);


        public static IEnumerable<Rectangle3d> CreateXY()
        {
            var rectangles = new List<Rectangle3d>()
            {
                CreateXY(Point3d.Origin, new Point3d(1, 0, 0)),
                CreateXY(Point3d.Origin, new Point3d(0, 1, 0)),
                CreateXY(new Point3d(1,1,0), new Point3d(2, 2, 0)),
                CreateXY(new Point3d(1,1,0), new Point3d(15, 15, 0)),
            };



            return rectangles;
        }


        public static Rectangle3d CreateXY(Point3d p1, Point3d p2)
        => new(Plane.WorldXY, p1, p2);

        public static Rectangle3d CreateXZ(Point3d p1, Point3d p2)
            => new(Plane.WorldZX, p1, p2);

        public static Rectangle3d CreateYZ(Point3d p1, Point3d p2)
          => new(Plane.WorldYZ, p1, p2);

        public static Rectangle3d CreateBaseXY() => CreateXY(Point3d.Origin, new Point3d(10, 10, 0));

        public static Rectangle3d XYToXZ(Rectangle3d sourceXYRectangle)
        {
            var targetRectangle = sourceXYRectangle;
            if (!targetRectangle.Transform(_xyToxz))
            { throw new Exception(); }
            return targetRectangle;
        }
    }
}
