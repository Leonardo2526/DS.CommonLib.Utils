using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to create objects for collisions (intersections) detection between objects.
    /// </summary>
    public interface ICollisionDetector<T,P>
    {
        /// <summary>
        /// Detected collisions between objects.
        /// </summary>
        List<(T,P)> Collisions { get; }
    }
}
