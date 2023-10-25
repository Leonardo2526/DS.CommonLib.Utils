using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Enumerables;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An object to minimize graph nodes.
    /// </summary>
    public class NodesMinimizator
    {
        private static readonly int _cTolerance = 3;
        private static readonly int _tolerance = 5;
        private static readonly double _ct = Math.Pow(0.1, _cTolerance);
        private static readonly Basis3d _defaultBasis = new(Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis);
        private readonly ILineIntersectionFactory _lineIntersectionFactory;
        private readonly ITraceCollisionDetector<Point3d> _collisionDetector;
        private readonly double _rayLength = 10000;
        private Point3d _firstNode;
        private Point3d _lastNode;

        /// <summary>
        /// Instansiate an object to minimize graph nodes.
        /// </summary>
        /// <remarks>
        /// It tries to erase graph nodes between first and last to minimize them by splitting graph on 4-nodes subgraph.
        /// </remarks>
        /// <param name="angles"></param>
        /// <param name="iteratorBuilder"></param>
        /// <param name="lineIntersectionFactory"></param>
        /// <param name="collisionDetector"></param>
        public NodesMinimizator(ILineIntersectionFactory lineIntersectionFactory,
            ITraceCollisionDetector<Point3d> collisionDetector = null)
        {
            _lineIntersectionFactory = lineIntersectionFactory;
            _collisionDetector = collisionDetector;
        }

        /// <summary>
        /// Maximum link length of 4-node graph. 
        /// </summary>
        public double MaxLinkLength { get; set; }

        /// <summary>
        /// Minimum link length of 4-node graph. 
        /// </summary>
        public double MinLinkLength { get; set; }

        /// <summary>
        /// <see cref="Basis3d"/> at first initial graph node.
        /// </summary>
        public Basis3d InitialBasis { get; set; }

        /// <summary>
        /// Reduce <paramref name="initialGraph"/> nodes.
        /// </summary>
        /// <param name="initialGraph"></param>
        /// <returns></returns>
        public SimpleGraph ReduceNodes(SimpleGraph initialGraph)
        {
            var initialNodes = new List<Point3d>();
            initialNodes.AddRange(initialGraph.Nodes);
            _firstNode = initialNodes.First();
            _lastNode = initialNodes.Last();

            var graph = new SimpleGraph(initialNodes);
            var graph4Basis = InitialBasis.X.Length == 0 && InitialBasis.Y.Length == 0 && InitialBasis.Z.Length == 0 ?
                _defaultBasis.GetBasis(initialGraph.Links.First().UnitTangent) : InitialBasis;

            for (int i = 0; i <= graph.Nodes.Count - 4; i++)
            {
                var graph4 = GetFourNodesGraph(graph, i);
                graph4Basis = graph4Basis.GetBasis(graph4.Links.First().UnitTangent);

                if (graph4.IsPlane(out Plane plane) && HasValidLinks(graph4))
                {
                    Vector3d firstNodeParentDir = i == 0 ?
                        default(Vector3d) :
                        new Line(graph.Nodes[i - 1], graph.Nodes[i]).UnitTangent;
                    Vector3d lastNodeParentDir = i == graph.Nodes.Count - 4 ?
                        default(Vector3d) :
                        new Line(graph.Nodes[i + 3], graph.Nodes[i + 4]).UnitTangent;
                    TryReduceNodes4(graph4, plane, firstNodeParentDir, lastNodeParentDir, graph4Basis);
                    Rebuild(graph, graph4, i);
                }
            }

            if (initialGraph.Nodes.Count > graph.Nodes.Count)
            { Debug.WriteLine($"Path nodes minimized from {initialGraph.Nodes.Count} to {graph.Nodes.Count}"); }

            return graph;
        }

        private SimpleGraph GetFourNodesGraph(SimpleGraph graph, int startIndex)
        {
            var nodes4 = graph.Nodes.Skip(startIndex).Take(4).ToList();
            return new SimpleGraph(nodes4);
        }

        private void TryReduceNodes4(SimpleGraph graph4, Plane plane,
            Vector3d firstNodeParentDir, Vector3d lastNodeParentDir, Basis3d graph4Basis)
        {
            var planes = new List<Plane>() { plane };

            var node1 = graph4.Nodes.First();
            var node2 = graph4.Nodes.Last();

            _lineIntersectionFactory.FirstNodePlanes = planes;
            _lineIntersectionFactory.LastNodePlanes = planes;

            Point3d intersectionPoint = _lineIntersectionFactory.
                WithDetector(_collisionDetector, graph4Basis, _firstNode, _lastNode).
                GetIntersection((node1, firstNodeParentDir), (node2, lastNodeParentDir));

            if (!double.IsNaN(intersectionPoint.X))
            {
                graph4.Nodes.Clear();

                graph4.Nodes.Add(node1);
                var p = intersectionPoint.Round(_tolerance);
                if (p.DistanceTo(node1) > _ct && p.DistanceTo(node2) > _ct)
                { graph4.Nodes.Add(p); }
                graph4.Nodes.Add(node2);
            }
        }

        private void Rebuild(SimpleGraph graph, SimpleGraph graph4, int startIndex)
        {
            var graphNodes = graph.Nodes;
            var graph4Nodes = graph4.Nodes;

            if (graph4Nodes.Count == 4) { return; }

            graphNodes.RemoveRange(startIndex, 4);
            graphNodes.InsertRange(startIndex, graph4Nodes);
        }

        private bool HasValidLinks(SimpleGraph graph4)
        {
            if (MaxLinkLength == 0) { return true; }
            return graph4.Links.TrueForAll(l => l.Length < MaxLinkLength);
        }


    }
}
