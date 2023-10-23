using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Enumerables;
using DS.ClassLib.VarUtils.Points;
using Rhino;
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
    public class AStarAlgorithmCDF : IPathFindAlgorithm<Point3d, Point3d>
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
        private CancellationTokenSource _internalTokenSource;
        private CancellationTokenSource _linkedTokenSource;
        private Line _line;

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

        public AStarAlgorithmCDF()
        {
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

        /// <inheritdoc/>
        public CancellationTokenSource ExternalTokenSource { get; set; } = new CancellationTokenSource();


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

        /// <summary>
        /// Source basis.
        /// </summary>
        public Basis3d SourceBasis { get; set; }

        /// <summary>
        /// Check directions.
        /// </summary>
        public IDirectionValidator DirectionValidator { get; set; }

        /// <summary>
        /// Specifies if it was failed to exit from startPoint.
        /// </summary>
        public bool IsFailedOnStart { get; private set; }

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
            ResetToken();

            _startPoint = startPoint.Round(_tolerance);
            _endPoint = endPoint.Round(_tolerance);
            _line = (_endPoint - _startPoint).IsParallelTo(Vector3d.XAxis, RhinoMath.ToRadians(3)) == 0 ?
               default : new Line(_startPoint, _endPoint);

            MTolerance = _nodeBuilder.Step;

            //initiate a new search
            var parentNode = new PathNode()
            {
                Point = startPoint,
                Parent = startPoint,
                Dir = StartDirection,
                ANP = StartANP,
                Basis = SourceBasis
            };
            bool found = false;
            bool start = true;
            _mOpen.Clear();
            _mClose.Clear();
            _mOpen.Push(parentNode);

            double minDistToANP = _traceSettings.R + _traceSettings.D;

            //iterate over open list.
            while (_mOpen.Count > 0)
            {
                //return null;
                if (_linkedTokenSource.IsCancellationRequested)
                {
                    Debug.WriteLine("Path search time is up.");
                    Debug.WriteLine("Close nodes count is: " + _mClose.Count);
                    return null;
                }

                parentNode = _mOpen.Pop();
                //PointVisualisator?.Show(parentNode.Point);

                //var checkPoint1 = new Point3d(25.497, -6.245, 0);
                //if (parentNode.Point.DistanceTo(checkPoint1) < 0.001)
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
                Action pushNode = parentNode.ANP == StartANP || distToANP >= minDistToANP ?
             pushNodeWithIterator : pushNodeWithParent;

                //get and add nodes (successors)
                pushNode.Invoke();
                _mClose.Add(parentNode);

                if(start) { start = false; minDistToANP = _traceSettings.F; }
            }

            Debug.WriteLine("Close nodes count: " + _mClose.Count);
            var pathNodes = RestorePath(found, _mClose);
            var path = _refineFactory.Refine(pathNodes);

            IsFailedOnStart = _mClose.Count == 1;
            Debug.WriteLineIf(IsFailedOnStart, "PathFind failed on start point");

            return path;
        }

        /// <inheritdoc/>
        public void ResetToken()
        {
            _internalTokenSource = new CancellationTokenSource(3000);
            _linkedTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(ExternalTokenSource.Token, _internalTokenSource.Token);
        }

        private bool TryPushNode(PathNode parentNode, Vector3d nodeDir)
        {

            var newNode = _nodeBuilder.Build(parentNode, nodeDir);
            //PointVisualisator?.ShowVector(parentNode.Point, newNode.Point);
            //PointVisualisator?.Show(newNode.Point);
            if (newNode.Equals(default(PathNode)) || newNode.Point.IsLess(_lowerBound) || newNode.Point.IsGreater(_upperBound))
            {
                return false;
            }

            if (_line != default
                && !DirectionValidator.IsValid(newNode.Point.Round(_cTolerance))
                //&& !DirectionValidator.IsValid(nodeDir)
                )
            { return false; }

            bool endNode = newNode.Point.Round(_cTolerance) == _endPoint.Round(_cTolerance);

            if (endNode && EndDirection.Length != 0)
            {
                var endAngle =
                        (int)Math.Round(Vector3d.VectorAngle(EndDirection, nodeDir).RadToDeg());
                if (endAngle != 0
                    && _traceSettings.A != endAngle
                    && _traceSettings.A != -endAngle)
                // && !_traceSettings.AList.Contains(endAngle)
                //&& !_traceSettings.AList.Contains(-endAngle))
                { return false; }
                else if (EndANP != default && newNode.Point.DistanceTo(EndANP) < _traceSettings.R + _traceSettings.D)
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
                if (!endNode)
                {
                    if (foundInOpen.G != 0 && foundInOpen.G < newNode.G)
                    { return false; }
                    if (foundInClose.G != 0 && foundInClose.G < newNode.G)
                    { return false; }
                }
            }

            newNode = _nodeBuilder.BuildWithParameters();

            if (endNode)
            {
                var minDist = newNode.Dir.IsParallelTo(EndDirection, 3.DegToRad()) == 1 ?
                    _traceSettings.R + _traceSettings.D : _traceSettings.F;
                if (newNode.ANP.DistanceTo(_endPoint) < minDist)
                { return false; }
            }

            //check collisions 
            if (_mOpen.Count == 0 && _mClose.Count == 0)
                { _collisionDetector.GetFirstCollisions(newNode.Point, newNode.Basis); }
            else if (endNode)
            { _collisionDetector.GetLastCollisions(parentNode.Point, newNode.Basis); }
            else
            { _collisionDetector.GetCollisions(parentNode.Point, newNode.Point, newNode.Basis); }
            //_collisionDetector.GetCollisions(parentNode.Point, newNode.Point, newNode.Basis);
            if (_collisionDetector.Collisions.Count > 0)
            { _unpassablePoints.Add(newNode.Point); return false; } //unpassable point

            //add punishment to nodes throught traversable walls.
            newNode.F += _collisionDetector.Punishment * newNode.StepVector.Length;

            //PointVisualisator?.ShowVector(parentNode.Point, newNode.Point);
            //PointVisualisator?.Show(newNode.Basis);
            _mOpen.Push(newNode);

            return true;
        }

        private List<PathNode> RestorePath(bool found, List<PathNode> closeNodes)
        {
            var path = new List<PathNode>();

            if (!found) { return path; }

            path.AddRange(closeNodes);
            var fNode = path[path.Count - 1];

            var firstANP = path.First().ANP;
            var lastANP = path.Last(p => p.ANP != _startPoint).ANP;


            for (int i = path.Count - 1; i >= 0; i--)
            {
                if(fNode.ANP != firstANP 
                    && fNode.ANP != lastANP 
                    && fNode.ANP.DistanceTo(path[i].ANP) < _traceSettings.F)
                    { path.RemoveAt(i); continue; }

                if (fNode.ANP == path[i].Point || i == path.Count - 1)
                {
                    fNode = path[i];
                }
                else if(path[i].Point == _startPoint) { continue; }
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
