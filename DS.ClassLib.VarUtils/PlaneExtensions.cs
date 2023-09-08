using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Extension methods for <see cref="Plane"/>, <see cref="PlaneSurface"/> and <see cref="Rectangle3d"/>.
    /// </summary>
    public static class PlaneExtensions
    {
        /// <summary>
        /// Specifies if <paramref name="point"/> lies on <paramref name="plane"/>.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="point"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// <see langword="true"/> if distance from <paramref name="point"/> to <paramref name="plane"/> is less than <paramref name="tolerance"/>.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool Contains(this Plane plane, Point3d point, double tolerance = 0.001)
            => plane.DistanceTo(point) < tolerance;

        /// <summary>
        /// Specifies if <paramref name="point"/> lies inside <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// <see langword="true"/> if distance from <paramref name="point"/> to <paramref name="rectangle"/> is less than <paramref name="tolerance"/>
        /// and <paramref name="point"/> lies inside <paramref name="rectangle"/> bounds or coincidents with them.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool ContainsStrict(this Rectangle3d rectangle, Point3d point, double tolerance = 0.001)
        {
            var contaiment = rectangle.Contains(point);
            var dist = Math.Abs(rectangle.Plane.DistanceTo(point));
            return dist < 0.001 && (contaiment == PointContainment.Coincident || contaiment == PointContainment.Inside);

        }

    }
}
