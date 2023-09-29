using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Graphs;
using DS.ClassLib.VarUtils.Points;
using DS.PathFinder.Algorithms.AStar;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.PathFinder
{
    /// <summary>
    /// An object that represents factory to refine path.
    /// </summary>
    public class PathRefineFactory : IRefineFactory<Point3d>
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

        /// <inheritdoc/>
        public List<Point3d> Refine(List<PathNode> path)
        {
            var points = new List<Point3d>();

            if (path == null || path.Count == 0)
            { return points; }

            var firstNode = path[0];
            var basePoint = firstNode.Point;
            var baseDir = firstNode.Dir;

            points.Add(basePoint.Round(_tolerance));

            for (int i = 1; i < path.Count; i++)
            {
                var currentNode = path[i];
                var currentPoint = currentNode.Point;
                var currentDir = path[i].Dir;

                if (baseDir.Length == 0 || currentDir.Round(_tolerance) != baseDir.Round(_tolerance))
                {
                    if (i != 1)
                    { points.Add(basePoint.Round(_tolerance)); }
                    baseDir = currentDir;
                }
                basePoint = currentPoint;
            }
            points.Add(basePoint.Round(_tolerance));

            points = MinNodes ? MinimizeNodes(points) : points;

            //double minimizator to fix path find issues
            points = MinNodes ? MinimizeNodes(points) : points;

            return points;
        }

        private List<Point3d> MinimizeNodes(List<Point3d> points)
        {
            var graph = new SimpleGraph(points);

            var angles = new List<int>()
            {
              (int)_traceSettings.A
            };

            var minizator = new NodesMinimizator(angles, _collisionDetector);
            minizator.MinLinkLength = _traceSettings.F;
            minizator.MaxLinkLength = _maxLinkLength;
            minizator.InitialBasis = _sourceBasis;

            return minizator.ReduceNodes(graph).Nodes;
        }
    }
}
