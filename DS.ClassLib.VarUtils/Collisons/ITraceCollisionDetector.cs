using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to create objects for collisions (intersections) detection by trace.
    /// </summary>
    public interface ITraceCollisionDetector : ICollisionDetector
    {
        /// <summary>      
        /// Get collisions between points.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>Collisions between points.
        /// <para>
        /// Returns empty list if no collisions was detected.
        /// </para>
        /// </returns>
        List<ICollision> GetCollisions(Point3D point1, Point3D point2);
    }
}
