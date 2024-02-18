using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DS.PathFinder
{
    /// <summary>
    /// An object that represents algorithm to find path between <typeparamref name="T"/> points.
    /// </summary>
    public interface IPathFindAlgorithm<T, P>
    {
        /// <summary>
        /// Token to cancel finding path operation.
        /// </summary>
        public CancellationTokenSource ExternalToken { get; set; }

        /// <summary>
        /// Find path between <paramref name="startPoint"/> and <paramref name="endPoint"/>.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>
        /// Path coordinates.
        /// </returns>
        List<P> FindPath(T startPoint, T endPoint);
    }

    /// <summary>
    /// An object that represents algorithm to find path between <typeparamref name="T"/> points.
    /// </summary>
    public interface IBestPathFindAlgorithm<T, P>
    {
        /// <summary>
        /// Token to cancel finding path operation.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Find path between <paramref name="startPoint"/> and <paramref name="endPoint"/>.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>
        /// Path coordinates.
        /// </returns>
        IEnumerable<P> FindPath(T startPoint, T endPoint);
    }
}
