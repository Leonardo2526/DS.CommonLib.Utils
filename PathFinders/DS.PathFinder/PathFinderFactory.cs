using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder
{

    /// <summary>
    /// An object is used to find path between <typeparamref name="T"/> points.
    /// </summary>
    public class PathFinderFactory<T, P>: IPathFinder<T, P>
    {
        private readonly IPathFindAlgorithm<T, P> _algorithm;

        /// <summary>
        /// Instantiate an object to find path between <typeparamref name="T"/> points by <paramref name="algorithm"/>.
        /// </summary>
        /// <param name="algorithm"></param>
        public PathFinderFactory(IPathFindAlgorithm<T, P> algorithm)
        {
            _algorithm = algorithm;
        }

        /// <inheritdoc/>
        public List<P> Path {get; private set;}

        /// <inheritdoc/>
        public List<P> FindPath(T startPoint, T endPoint)
        {
            return Path =_algorithm.FindPath(startPoint, endPoint);
        }

        /// <inheritdoc/>
        public async Task<List<P>> FindPathAsync(T startPoint, T endPoint)
        {
            return await Task.Run(() => FindPath(startPoint, endPoint));
        }
    }
}
