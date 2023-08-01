using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DS.PathFinder
{
    /// <summary>
    /// An object that represents algorithm to find path between <typeparamref name="T"/> points.
    /// </summary>
    public interface IPathFindAlgorithm<T>
    {
        /// <summary>
        /// Find path between <paramref name="startPoint"/> and <paramref name="endPoint"/>.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>
        /// Path coordinates.
        /// </returns>
        List<T> FindPath(T startPoint, T endPoint);
    }
}
