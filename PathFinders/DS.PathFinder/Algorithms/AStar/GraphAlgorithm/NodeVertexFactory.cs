using DS.GraphUtils.Entities;
using QuickGraph.Algorithms;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using DS.ClassLib.VarUtils;
using Serilog;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;
using MoreLinq;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    internal class NodeVertexFactory : ITaggedVertexFactory<Node>, ISerilogged
    {
        private readonly GraphNodeBuilder _graphNodeBuilder;
        private readonly IVertexAndEdgeListGraph<NodeVertex, Edge<NodeVertex>> _graph;

        public NodeVertexFactory(GraphNodeBuilder graphNodeBuilder, IVertexAndEdgeListGraph<NodeVertex, Edge<NodeVertex>> graph)
        {
            _graphNodeBuilder = graphNodeBuilder;
            _graph = graph;
        }

        public ILogger Logger { get; set; }

        public NodeVertex Create(NodeVertex parent)
        {
            var specificator = _graphNodeBuilder
              .Create()
              .TryGetLocation(parent);
            if (specificator == null) { return null; }

            var id = _graph.VertexCount;
            var node = specificator
                  .WithPath()
                  .WithHeuristic();
            return new NodeVertex(id, node);
        }

        public NodeVertex Create(NodeVertex parent, Node tag)
        {
            throw new NotImplementedException();
        }
    }
}
