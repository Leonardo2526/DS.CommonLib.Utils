using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Enumerables;
using DS.GraphUtils.Entities;
using DS.PathFinder.Algorithms.AStar;
using Rhino.Geometry;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace DS.PathFinder
{
    /// <summary>
    /// An object that represents factory to refine path.
    /// </summary>
    public class PathRefineFactory : IRefineFactory<Point3d>, ISerilogged
    {
        private readonly int _tolerance = 3;
        private readonly ITraceSettings _traceSettings;
        private readonly ITraceCollisionDetector<Point3d> _collisionDetector;
        private readonly Basis3d _sourceBasis;
        private readonly double _maxLinkLength = 50;

        /// <summary>
        /// Instansiate an object that represents factory to refine path.
        /// </summary>
        /// <param name="traceSettings"></param>
        /// <param name="collisionDetector"></param>
        /// <param name="sourceBasis"></param>
        public PathRefineFactory(
            ITraceSettings traceSettings,
            ITraceCollisionDetector<Point3d> collisionDetector,
            Basis3d sourceBasis)
        {
            _traceSettings = traceSettings;
            _collisionDetector = collisionDetector;
            _sourceBasis = sourceBasis;
        }

        /// <summary>
        /// Specifies if minimize nodes of path.
        /// </summary>
        public bool MinNodes { get; set; }

        public ITaggedEdgeValidator<TaggedGVertex<Point3d>, Basis3d> EdgeValidator { get; set; }
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public List<Point3d> Refine(List<PathNode> path)
        {
            if (path == null || path.Count == 0)
            { return new List<Point3d>(); }

            var points = path.Select(n => n.Point).ToList();
            points.Reverse();

            List<Point3d> minPoints = null;
            try
            {
                minPoints = MinNodes ? MinimizeNodes(points) : points;
            }
            catch (System.Exception)
            {
                Logger.Warning("Failed to minimize nodes.");
            }

            return minPoints != null && minPoints.Count > 0 ? 
                minPoints : points;
        }

        private List<Point3d> MinimizeNodes(List<Point3d> points)
        {
            var graph = new SimpleGraph(points);

            var angles = new List<int>()
            {
              (int)_traceSettings.A
            };

            var intersectionFactory = new LineIntersectionFactory(angles, new DirectionIteratorBuilder())
            {
                EdgeValidator = EdgeValidator
            };
            var minizator = new NodesMinimizator(intersectionFactory, _collisionDetector)
            {
                MinLinkLength = _traceSettings.F,
                MaxLinkLength = _maxLinkLength,
                InitialBasis = _sourceBasis
            };

            return minizator.ReduceNodes(graph).Vertices;
        }
    }
}
