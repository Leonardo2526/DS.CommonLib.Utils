using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to create objects for collisions (intersections) detection by trace.
    /// </summary>
    public interface ITraceCollisionDetector<T> : ICollisionDetector
    {
        /// <summary>      
        /// Get collisions between points.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="basis"></param>
        /// <returns>Collisions between points.
        /// <para>
        /// Returns empty list if no collisions was detected.
        /// </para>
        /// </returns>
        List<ICollision> GetCollisions(T point1, T point2, Basis3d basis);

        List<ICollision> GetFirstCollisions(T point2, Basis3d basis);
        List<ICollision> GetLastCollisions(T point1, Basis3d basis);
    }
}
