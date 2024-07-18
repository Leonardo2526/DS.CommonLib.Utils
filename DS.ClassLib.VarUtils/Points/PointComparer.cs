using Rhino;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// Instansiate the object to compare <see cref="Point3d"/> coordinates.
    /// </summary>
    /// <param name="tolerance"></param>
    public class Point3dComparer(double tolerance = RhinoMath.ZeroTolerance) :
        IEqualityComparer<Point3d>
    {
        private readonly double _tolerance = tolerance;

        /// <inheritdoc/>
        public bool Equals(Point3d x, Point3d y)
        => x.IsAlmostEqualTo(y, _tolerance);

        /// <inheritdoc/>
        public int GetHashCode(Point3d obj)
        => -1;
    }
}
