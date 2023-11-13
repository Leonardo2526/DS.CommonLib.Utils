using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Extension methods for <see cref="Line"/>.
    /// </summary>
    public static class LineExtensions
    {

        /// <summary>
        /// Specifies if <paramref name="point"/> is inside <paramref name="line"/> bounds with specified <paramref name="tolerance"/>.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="point"/> lies on <paramref name="line"/> or 
        /// coinsident with <paramref name="line"/>'s end points.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool Contains(this Line line, Point3d point, double tolerance = 0.001)
        {
            var dist = line.DistanceTo(point, true);
            return dist < tolerance;
        }
    }
}
