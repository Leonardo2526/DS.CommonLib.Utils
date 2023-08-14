using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.PathFinder
{
    /// <summary>
    /// The interface is used to find path between <typeparamref name="T"/> points.
    /// </summary>
    public interface IPathFinder<T, P>
    {
        /// <summary>
        /// Path coordinates.
        /// </summary>
        List<P> Path { get; }

        /// <summary>
        /// Find path between <paramref name="startPoint"/> and <paramref name="endPoint"/>.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>
        /// Path coordinates.
        /// </returns>
        List<P> FindPath(T startPoint, T endPoint);

        /// <summary>
        /// Find path between <paramref name="startPoint"/> and <paramref name="endPoint"/> asyncrounously.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>
        /// Path coordinates.
        /// </returns>
        Task<List<P>> FindPathAsync(T startPoint, T endPoint);
    }
}
