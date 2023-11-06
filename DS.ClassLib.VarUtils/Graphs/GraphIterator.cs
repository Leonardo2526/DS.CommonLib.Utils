using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Iteration over graph <see cref="IVertex"/>'s.
    /// </summary>
    public class GraphVertexIterator : IEnumerator<IVertex>
    {
        private readonly IVertexListGraph<IVertex, Edge<IVertex>> _graph;
        private readonly RootedAlgorithmBase<IVertex, IVertexListGraph<IVertex, Edge<IVertex>>> _algorithm;
        private IVertex _current;
        private int _count = 0;
        private readonly List<int> _visited = new();

        /// <summary>
        /// Instanciate an object to iterate over graph <see cref="IVertex"/>'s.
        /// </summary>
        /// <param name="algorithm"></param>
        /// <exception cref="NotImplementedException"></exception>
        public GraphVertexIterator(RootedAlgorithmBase<IVertex, IVertexListGraph<IVertex, Edge<IVertex>>> algorithm)
        {
            _algorithm = algorithm;
            _graph = algorithm.VisitedGraph;

            switch (algorithm)
            {
                case BreadthFirstSearchAlgorithm<IVertex, Edge<IVertex>> bfs:
                    bfs.FinishVertex += DiscoverVertex; break;
                case DepthFirstSearchAlgorithm<IVertex, Edge<IVertex>> dfs:
                    dfs.FinishVertex += DiscoverVertex; break;
                default: throw new NotImplementedException();
            }
        }

        private void DiscoverVertex(IVertex vertex)
        {
            if (!_visited.Contains(vertex.Id))
            {
                _algorithm.Abort();
                _current = vertex;
                _visited.Add(vertex.Id);
            }
        }

        /// <inheritdoc/>
        public IVertex Current { get => _current; }

        object IEnumerator.Current => Current;


        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            _count = _visited.Count;
            _algorithm.Compute();

            return _count != _visited.Count;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _visited.Clear();
        }
    }
}
