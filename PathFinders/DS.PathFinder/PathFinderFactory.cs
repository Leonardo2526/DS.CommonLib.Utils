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
    public class PathFinderFactory<T>: IPathFinder<T>
    {
        private readonly IPathFindAlgorithm<T> _algorithm;

        /// <summary>
        /// Instantiate an object to find path between <typeparamref name="T"/> points by <paramref name="algorithm"/>.
        /// </summary>
        /// <param name="algorithm"></param>
        public PathFinderFactory(IPathFindAlgorithm<T> algorithm)
        {
            _algorithm = algorithm;
        }

        /// <inheritdoc/>
        public List<T> Path {get; private set;}

        /// <inheritdoc/>
        public List<T> FindPath(T startPoint, T endPoint)
        {
            return Path =_algorithm.FindPath(startPoint, endPoint);
        }

        /// <inheritdoc/>
        public async Task<List<T>> FindPathAsync(T startPoint, T endPoint)
        {
            return await Task.Run(() => FindPath(startPoint, endPoint));
        }
    }
}
