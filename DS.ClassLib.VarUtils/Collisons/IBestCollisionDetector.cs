using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// The interface used to create objects for collisions (intersections) detection between objects.
    /// </summary>
    public interface IBestCollisionDetector<T, P>
    {
        /// <summary>
        /// Detected collisions between objects.
        /// </summary>
        List<(T, P)> Collisions { get; }

        /// <summary>
        /// Get collisions of <paramref name="checkObject"/>.
        /// </summary>
        List<(T, P)> GetCollisions(T checkObject);
    }
}
