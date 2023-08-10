using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Directions;
using DS.ClassLib.VarUtils.Enumerables;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private readonly ITraceCollisionDetector<Point3d> _collisionDetector;
        private readonly IRefineFactory<Point3d> _refineFactory;
        private readonly bool _mReopenNodes = true;
        private double _mTolerance;
        private readonly PriorityQueueB<PathNode> _mOpen = new PriorityQueueB<PathNode>(new ComparePFNode());
        private readonly List<PathNode> _mClose = new List<PathNode>();
        private readonly List<Point3d> _unpassablePoints = new List<Point3d>();

        private INodeBuilder _nodeBuilder;
        private readonly DirectionIterator _dirIterator;
        private Point3d _upperBound;
        private Point3d _lowerBound;
        private int _tolerance = 3;
        private int _cTolerance = 2;
        private Point3d _startPoint;
        private Point3d _endPoint;

        #endregion


        /// <summary>
        /// Instanciate a new algorithm to find path between <see cref="Point3d"/> points in continuous data field.
        /// </summary>
        /// <param name="traceSettings"></param>
        /// <param name="nodeBuilder"></param>
        /// <param name="dirIterator"></param>
        /// <param name="collisionDetector"></param>
        /// <param name="refineFactory"></param>
        public AStarAlgorithmCDF(ITraceSettings traceSettings, INodeBuilder nodeBuilder, DirectionIterator dirIterator,
            ITraceCollisionDetector<Point3d> collisionDetector, IRefineFactory<Point3d> refineFactory)
        {
            _traceSettings = traceSettings;
            _nodeBuilder = nodeBuilder;
            _dirIterator = dirIterator;
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
        /// Tolerance coefficient to compare exist nodes.
        /// </summary>
        public int MToleranceCoef { get; set; }


        private double MTolerance
        {
            get => _mTolerance;
            set
            {
                _mTolerance = value / MToleranceCoef;
            }
        }

        /// <summary>
        /// Token to cancel finding path operation.
        /// </summary>
        public CancellationTokenSource TokenSource { get; set; }

        /// <summary>
        /// Specifies visualisator to show points.
        /// </summary>
        public IPointVisualisator<Point3d> PointVisualisator { get; set; }

        /// <summary>
        /// Direction at start point.
        /// </summary>
        public Vector3d StartDirection { get; set; }

        /// <summary>
        /// Angle point at start.
        /// </summary>
        public Point3d StartANP { get; set; }

        /// <summary>
        /// Direction at end point.
        /// </summary>
        public Vector3d EndDirection { get; set; }

        /// <summary>
        /// Angle point at end.
        /// </summary>
        public Point3d EndANP { get; set; }

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
            _startPoint = startPoint.Round(_tolerance);
            _endPoint = endPoint.Round(_tolerance);

            MTolerance = _nodeBuilder.Step;

            //initiate a new search
            var parentNode = new PathNode()
            {
                Point = startPoint,
                Parent = startPoint,
                Dir = StartDirection,
                ANP = StartANP
            };
            bool found = false;
            _mOpen.Clear();
            _mClose.Clear();
            _mOpen.Push(parentNode);

            //iterate over open list.
            while (_mOpen.Count > 0)
            {
                //return null;
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                {
                    Debug.WriteLine("Path search time is up.");
                    Debug.WriteLine("Close nodes count is: " + _mClose.Count);
                    return null;
                }

                parentNode = _mOpen.Pop();
                //PointVisualisator?.Show(parentNode.Point);

                //var checkPoint = new Point3d(124.51497, 163.50836, 0);
                //if (parentNode.Point.DistanceTo(checkPoint) < 0.001)
                //{

                //}

                if (parentNode.Point.Round(_cTolerance) == endPoint.Round(_cTolerance))
                {
                    parentNode.Point = endPoint;
                    _mClose.Add(parentNode);
                    found = true;
                    break;
                }

                var distToANP = parentNode.ANP.DistanceTo(parentNode.Point);

                void pushNodeWithParent() => TryPushNode(parentNode, parentNode.Dir);
                void pushNodeWithIterator()
                {
                    _dirIterator.ParentDir = parentNode.Dir;
                    while (_dirIterator.MoveNext())
                    {
                        Vector3d nodeDir = _dirIterator.Current;
                        TryPushNode(parentNode, nodeDir);
                    }
                    _dirIterator.Reset();
                }
                Action pushNode = parentNode.ANP == default || distToANP >= _traceSettings.F ?
             pushNodeWithIterator : pushNodeWithParent;

                //get and add nodes (successors)
                pushNode.Invoke();
                _mClose.Add(parentNode);
            }

            Debug.WriteLine("Close nodes count: " + _mClose.Count);
            var pathNodes = RestorePath(found, _mClose);
            var path = _refineFactory.Refine(pathNodes);

            return path;
        }

        /// <inheritdoc/>
        public void ResetToken()
        {
            TokenSource = new CancellationTokenSource(3000);
        }

        private bool TryPushNode(PathNode parentNode, Vector3d nodeDir)
        {
            var newNode = _nodeBuilder.Build(parentNode, nodeDir);
            //PointVisualisator?.Show(newNode.Point);
            if (newNode.Point.IsLess(_lowerBound) || newNode.Point.IsGreater(_upperBound))
            {
                return false;
            }

            if (newNode.Point.Round(_cTolerance) == _endPoint.Round(_cTolerance) && EndDirection.Length != 0)
            {
                var endAngle =
                        (int)Math.Round(Vector3d.VectorAngle(EndDirection, nodeDir).RadToDeg());
                if (endAngle != 0
                    && !_traceSettings.AList.Contains(endAngle)
                    && !_traceSettings.AList.Contains(-endAngle))
                { return false; }
                else if (EndANP != default && newNode.Point.DistanceTo(EndANP) < _traceSettings.F)
                { return false; }
            }

            var foundInOpen = _mOpen.InnerList.FirstOrDefault(n => n.Point.DistanceTo(newNode.Point) < _mTolerance);
            var foundInClose = _mClose.FirstOrDefault(n => n.Point.DistanceTo(newNode.Point) < _mTolerance);

            if (!_mReopenNodes)
            {
                if (foundInOpen.G != 0 || foundInClose.G != 0)
                { return false; }
            }
            else
            {
                if (foundInOpen.G != 0 && foundInClose.G < newNode.G)
                { return false; }
                if (foundInClose.G != 0 && foundInClose.G < newNode.G)
                { return false; }
            }

            newNode = _nodeBuilder.BuildWithParameters();

            //check collisions 
            _collisionDetector.GetCollisions(parentNode.Point, newNode.Point);
            if (_collisionDetector.Collisions.Count > 0)
            { _unpassablePoints.Add(newNode.Point); return false; } //unpassable point

            //PointVisualisator?.ShowVector(parentNode.Point, newNode.Point);
            _mOpen.Push(newNode);

            return true;
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


        //var checkPoint = new Point3d(-79.60393, -110.97724, 0);
        //if (newNode.Point.DistanceTo(checkPoint) < 0.001)
        //{

        //}                 
    }
}
