using DS.ClassLib.VarUtils;
using DS.GraphUtils.Entities;
using QuickGraph;
using QuickGraph.Algorithms;
using Rhino.Geometry;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    /// <summary>
    /// An algorithm to find path between <see cref="Point3d"/> points in continuous data field based on graph.
    /// </summary>
    public class AStarGraphAlgorithm(
        ITaggedVertexFactory<Node> vertexFactory, 
        IEdgeFactory<NodeVertex> edgeFactory) : 
        IBestPathFindAlgorithm<Point3d, Point3d>, 
        IGraphFactory<NodeVertex>, ISerilogged
    {
        private static readonly int _compareTolerance = 2;
        private readonly PriorityQueueB<NodeVertex> _mOpen = new(new PriorityComparer());
        private readonly VertexLocationComparer _locationComparer = new(_compareTolerance);

        private IVertexAndEdgeListGraph<NodeVertex, Edge<NodeVertex>> _graph;
        private Point3d _startPoint;
        private Point3d _endPoint;
        private NodeVertex _source;
        private NodeVertex _target;



        #region Properties

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; set; }

        /// <inheritdoc/>
        public ILogger Logger { get; set; }

        #endregion


        /// <inheritdoc/>
        public IEnumerable<Point3d> FindPath(Point3d startPoint, Point3d endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;

            _graph = CreateGraph();
            return ToPath(_graph);
        }

        /// <summary>
        /// Convert <paramref name="graph"/> to path from <see cref="_source"/> to <see cref="_target"/>.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>
        /// Vertices locations or empty <see cref="Enumerable"/>.
        /// </returns>
        public IEnumerable<Point3d> ToPath(IVertexAndEdgeListGraph<NodeVertex, Edge<NodeVertex>> graph)
        {
            if (graph.VertexCount == 0 || CancellationToken.IsCancellationRequested)
            {
                Logger?.Warning("Failed to create graph.");
                return Enumerable.Empty<Point3d>();
            }

            var result = graph.ShortestPathsDijkstra((e) => 1, _source)
                            .Invoke(_target, out var edges);
            if(!result)
            {
                Logger?.Warning("Failed to find path.");
                return Enumerable.Empty<Point3d>();
            }

            var verices = edges.Select(e => e.Source).ToList();
            verices.Add(_target);

            return verices.Select(v => v.Tag.Location);
        }


        /// <inheritdoc/>
        public IVertexAndEdgeListGraph<NodeVertex, Edge<NodeVertex>> CreateGraph()
        {
            _source = new NodeVertex(0, new Node(_startPoint));
            _target = new NodeVertex(-1, new Node(_endPoint));
            _mOpen.Clear();
            _mOpen.Push(_source);

            AdjacencyGraph<NodeVertex, Edge<NodeVertex>> graph = new();

            while (_mOpen.Count > 0 &&
                !CancellationToken.IsCancellationRequested &&
                !_locationComparer.Equals(graph.Vertices.Last(), _target))
            {
                var parent = _mOpen.Pop();

                var child = vertexFactory.Create(parent);
                if (child == null) { continue; }

                var edge = edgeFactory.Create(parent, child);
                if (edge == null) { continue; }

                graph.AddEdge(edge);
                _mOpen.Push(child);
            }



            return graph;
        }
    }
}
