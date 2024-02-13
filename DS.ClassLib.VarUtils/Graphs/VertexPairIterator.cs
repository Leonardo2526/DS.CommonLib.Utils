using QuickGraph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Iterator through <see cref="IVertex"/>'s pairs.
    /// </summary>
    public class VertexPairIterator : IEnumerator<(IVertex, IVertex)>
    {
        private readonly IEnumerator<IVertex> _graphIterator;
        private readonly AdjacencyGraph<IVertex, Edge<IVertex>> _graph;
        private readonly List<IVertex> _leftClosed = new List<IVertex>();
        private readonly List<IVertex> _rightClosed = new List<IVertex>();
        private List<IVertex> _leftInitial = new List<IVertex>();

        /// <summary>
        /// Instansiate an iterator through <see cref="IVertex"/>'s pairs.
        /// </summary>
        public VertexPairIterator(IEnumerator<IVertex> graphIterator, AdjacencyGraph<IVertex, Edge<IVertex>> graph)
        {
            _graphIterator = graphIterator;
            _graph = graph;
            if (!TryInitiate(graph)) { throw new InvalidOperationException(); }
        }

        /// <summary>
        /// Not visited pairs.
        /// </summary>
        public Queue<(IVertex, IVertex)> Open { get; } = new Queue<(IVertex, IVertex)>();

        /// <summary>
        /// Visited pairs.
        /// </summary>
        public Queue<(IVertex, IVertex)> Close { get; } = new Queue<(IVertex, IVertex)>();


        /// <inheritdoc/>
        public (IVertex, IVertex) Current => Close.Last();

        object IEnumerator.Current => Current;


        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            while (Open.Count == 0)
            {
                if (!TryMoveGraphIterator())
                { return false; };
            }

            var task = Open.Dequeue();
            Close.Enqueue(task);

            return true;
        }

        private bool TryMoveGraphIterator()
        {
            if (_graphIterator.MoveNext())
            {
                var currentVertex = _graphIterator.Current;
                if (_leftInitial.Select(v => v.Id).Contains(currentVertex.Id))
                {
                    _leftClosed.Add(currentVertex);
                    _rightClosed.ForEach(v => Open.Enqueue((v, currentVertex)));
                }
                else
                {
                    _rightClosed.Add(currentVertex);
                    _leftClosed.ForEach(v => Open.Enqueue((v, currentVertex)));
                }
                return true;
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _graphIterator.Reset();
            Open.Clear(); Close.Clear();
            _leftClosed.Clear();
            _rightClosed.Clear();
        }

        private bool TryInitiate(AdjacencyGraph<IVertex, Edge<IVertex>> graph)
        {
            var splitted = graph.SplitRootBranches();

            if (splitted.Count != 2)
            { return false; }
            else
            {
                _leftInitial = splitted.ElementAt(0).Value.ToList();
                return true;
            }
        }
    }
}
