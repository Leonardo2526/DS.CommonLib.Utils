using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Directions;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DS.PathFinder.Algorithms.AStar
{
    /// <summary>
    /// An algorithm to find path between <see cref="Point3d"/> points in continuous data field.
    /// </summary>
    public class AStarAlgorithmCDF : IPathFindAlgorithm<Point3d>
    {
        #region Fields

        private readonly ITraceSettings _traceSettings;
        private readonly INodeBuilder _nodeBuilder;
        private readonly ITraceCollisionDetector<Point3d> _collisionDetector;
        private readonly IRefineFactory<Point3d> _refineFactory;
        private readonly List<Vector3d> _searchDirections = new List<Vector3d>();
        private readonly bool _mReopenNodes = true;
        private readonly double _mTolerance = 0.1;
        private readonly PriorityQueueB<PathNode> _mOpen = new PriorityQueueB<PathNode>(new ComparePFNode());
        private readonly List<PathNode> _mClose = new List<PathNode>();
        private readonly List<Point3d> _unpassablePoints = new List<Point3d>();

        private Point3d _upperBound;
        private Point3d _lowerBound;
        private int _tolerance = 3;
        private int _cTolerance = 2;

        #endregion


        /// <summary>
        /// Instanciate a new algorithm to find path between <see cref="Point3d"/> points in continuous data field.
        /// </summary>
        /// <param name="traceSettings"></param>
        /// <param name="nodeBuilder"></param>
        /// <param name="searchDirections"></param>
        /// <param name="collisionDetector"></param>
        /// <param name="refineFactory"></param>
        public AStarAlgorithmCDF(ITraceSettings traceSettings, INodeBuilder nodeBuilder, List<Vector3d> searchDirections,
            ITraceCollisionDetector<Point3d> collisionDetector, IRefineFactory<Point3d> refineFactory)
        {
            _traceSettings = traceSettings;
            _nodeBuilder = nodeBuilder;
            _searchDirections = searchDirections;
            _collisionDetector = collisionDetector;
            _refineFactory = refineFactory;
        }

        #region Properties

        /// <summary>
        /// Path find tolerance.
        /// </summary>
        public int Tolerance { get => _tolerance; set => _tolerance = value; }

        /// <summary>
        /// Compound numbers tolerance.
        /// </summary>
        public int CTolerance { get => _cTolerance; set => _cTolerance = value; }

        /// <summary>
        /// Token to cancel finding path operation.
        /// </summary>
        public CancellationTokenSource TokenSource { get; set; }

        /// <summary>
        /// Specifies visualisator to show points.
        /// </summary>
        public IPointVisualisator<Point3d> PointVisualisator { get; set; }

        #endregion


        /// <summary>
        /// Set <paramref name="lowerBound"/> and <paramref name="upperBound"/> to restrict find path area.
        /// </summary>
        /// <param name="upperBound"></param>
        /// <param name="lowerBound"></param>
        public AStarAlgorithmCDF WithBounds(Point3d lowerBound, Point3d upperBound)
        {
            _lowerBound = lowerBound.Round(_tolerance);
            _upperBound = upperBound.Round(_tolerance);
            return this;
        }

        /// <inheritdoc/>
        public List<Point3d> FindPath(Point3d startPoint, Point3d endPoint)
        {
            startPoint = startPoint.Round(_tolerance);
            endPoint = endPoint.Round(_tolerance);

            var parentNode = new PathNode()
            {
                Point = startPoint,
                Parent = startPoint,
                ANP = startPoint
            };

            bool found = false;
            _mOpen.Clear();
            _mClose.Clear();

            var parentDir = new List<Vector3d>();

            _mOpen.Push(parentNode);
            while (_mOpen.Count > 0)
            {
                //return null;
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                { Debug.WriteLine("Path search time is up."); return null; }

                parentNode = _mOpen.Pop();
                PointVisualisator?.Show(parentNode.Point);

                if (parentNode.Point.Round(_cTolerance) == endPoint.Round(_cTolerance))
                {
                    parentNode.Point = endPoint;
                    _mClose.Add(parentNode);
                    found = true;
                    break;
                }

                //specify available direction for nodes (successors)
                var distToANP = parentNode.ANP.DistanceTo(parentNode.Point);
                parentDir.Clear();
                parentDir.Add(parentNode.Dir);
                List<Vector3d> availableDirections = distToANP == 0 || distToANP >= _traceSettings.F ?
                   _searchDirections : parentDir;

                //Lets calculate each successors
                for (int i = 0; i < availableDirections.Count; i++)
                {
                    Vector3d nodeDir = availableDirections[i];
                    if (parentNode.Dir.Length != 0)
                    {
                        var angle = (int)Math.Round(Vector3d.VectorAngle(nodeDir, parentNode.Dir).RadToDeg());
                        if (angle == 0) { }
                        else if (angle > 90)
                        { continue; }
                        else if (!_traceSettings.AList.Contains(angle))
                        { continue; }
                    }

                    var newNode = _nodeBuilder.Build(parentNode, nodeDir);

                    if (newNode.Point.IsLess(_lowerBound) || newNode.Point.IsGreater(_upperBound)
                        || _unpassablePoints.Contains(newNode.Point))
                    { continue; }

                    var foundInOpen = _mOpen.InnerList.FirstOrDefault(n => n.Point.DistanceTo(newNode.Point) < _mTolerance);
                    var foundInClose = _mClose.FirstOrDefault(n => n.Point.DistanceTo(newNode.Point) < _mTolerance);

                    if (!_mReopenNodes && foundInOpen.G != 0 || foundInClose.G != 0)
                    { continue; }

                    newNode = _nodeBuilder.BuildWithParameters();

                    //reopen closed node only if it has G value less than was in found list.
                    if (foundInOpen.G != 0 && foundInOpen.G <= newNode.G)
                    { continue; }
                    if (foundInClose.G != 0 && foundInClose.G <= newNode.G)
                    { continue; }

                    if (foundInOpen.G == 0 || foundInClose.G == 0)
                    {
                        //check collisions 
                        _collisionDetector.GetCollisions(parentNode.Point, newNode.Point);
                        if (_collisionDetector.Collisions.Count > 0)
                        { _unpassablePoints.Add(newNode.Point); continue; } //unpassable point
                    }


                    _mOpen.Push(newNode);
                }

                _mClose.Add(parentNode);
            }

            Debug.WriteLine("Close nodes count: " + _mClose.Count);
            var pathNodes = RestorePath(found, _mClose);
            var path = _refineFactory.Refine(pathNodes);

            return path;
        }

        private List<PathNode> RestorePath(bool found, List<PathNode> closeNodes)
        {
            var path = new List<PathNode>();

            if (!found) { return path; }

            path.AddRange(closeNodes);
            var fNode = path[path.Count - 1];
            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (fNode.Parent == path[i].Point || i == path.Count - 1)
                {
                    fNode = path[i];
                }
                else
                { path.RemoveAt(i); }
            }

            return path;
        }

        //var checkPoint = new Point3d(10.335828058516039, -7.41881388825947, 0);
        //if (newNode.Point.DistanceTo(checkPoint) < 0.001)
        //{

        //}                 
    }
}
