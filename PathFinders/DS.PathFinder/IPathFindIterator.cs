using System.Collections.Generic;

namespace DS.PathFinder
{
    /// <summary>
    /// The interface that represents iterator to find path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPathFindIterator<T>
    {
        /// <summary>
        /// Find path.
        /// </summary>
        /// <returns>
        /// Found path.
        /// </returns>
        List<T> FindPath();
    }
}
