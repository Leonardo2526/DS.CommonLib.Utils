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
    /// An object that represents boolean operations between <see cref="Rectangle3d"/>s.
    /// </summary>
    public static class RectangleBooleanTools
    {
        /// <summary>
        /// Find intersection <see cref="Rectangle3d"/> between <paramref name="r1"/> and <paramref name="r2"/>.
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="strict">If true, the <paramref name="r2"/> needs to be fully inside of <paramref name="r1"/>. 
        /// I.e. rectangles with coincident borders will be considered 'outside'.</param>
        /// <param name="intersectionRectangle"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="r1"/> intersect with <paramref name="r2"/>.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool Intersection(this Rectangle3d r1, Rectangle3d r2, bool strict, out Rectangle3d intersectionRectangle)
        {
            intersectionRectangle = default;
            if (!r1.Plane.IsCoplanar(r2.Plane))
            { return false; }
            
            var targetRectangle1 = r1;
            var rotationTransform1 = Transform.PlaneToPlane(r1.Plane, Plane.WorldXY);
            if (!targetRectangle1.Transform(rotationTransform1)) { throw new Exception(); }

            var targetRectangle2 = r2;
            var rotationTransform2 = Transform.PlaneToPlane(r1.Plane, Plane.WorldXY);
            if (!targetRectangle2.Transform(rotationTransform2)) { throw new Exception(); }

            var corners1 = new List<Point3d>();
            targetRectangle1.GetCorners().ToList().ForEach(corner => corners1.Add(new Point3d(corner.X, corner.Y,0)));
            var box1 = new BoundingBox(corners1);

            var corners2 = new List<Point3d>();
            targetRectangle2.GetCorners().ToList().ForEach(corner => corners2.Add(new Point3d(corner.X, corner.Y, 0)));
            var box2 = new BoundingBox(corners2);
            var intersectionBox = BoundingBox.Intersection(box1, box2);

            if (!intersectionBox.IsValid || (strict && intersectionBox.Volume == 0))
            { return false; }
            else
            {
                intersectionRectangle = new Rectangle3d(r1.Plane, intersectionBox.Min, intersectionBox.Max);
                return true;
            }
        }

    }
}
