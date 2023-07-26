using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Directions;
using DS.ClassLib.VarUtils.Points;
using FrancoGustavo;
using FrancoGustavo.Algorithm;
using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.PathFinder
{
    /// <summary>
    /// An algorithm to find path between <see cref="Point3d"/> points in continuous data field.
    /// </summary>
    public class AStarAlgorithmCDF : IPathFindAlgorithm<Point3d>
    {
        #region Fields

        private readonly ITraceSettings _traceSettings;
        private readonly INodeBuilder _nodeBuilder;
        private readonly IDirectionFactory _directionFactory;
        private readonly ITraceCollisionDetector<Point3d> _collisionDetector;
        private readonly IRefineFactory<Point3d> _refineFactory;
        private readonly List<Vector3d> _searchDirections = new List<Vector3d>();
        private readonly bool _mReopenCloseNodes = true;
        private readonly int _fractPrec = 7;
        private readonly int _tolerance = 2;
        private readonly PriorityQueueB<PointPathFinderNode> _mOpen = new PriorityQueueB<PointPathFinderNode>(new ComparePFNode());
        private readonly List<PointPathFinderNode> _mClose = new List<PointPathFinderNode>();
        private readonly List<PointPathFinderNode> _passableNodes = new List<PointPathFinderNode>();
        private readonly List<PointPathFinderNode> _unpassableNodes = new List<PointPathFinderNode>();

        private Point3d _upperBound;
        private Point3d _lowerBound;
        private Point3d _startPoint;
        private Point3d _endPoint;

        #endregion


        /// <summary>
        /// Instanciate a new algorithm to find path between <see cref="Point3d"/> points in continuous data field.
        /// </summary>
        /// <param name="traceSettings"></param>
        /// <param name="nodeBuilder"></param>
        /// <param name="directionFactory"></param>
        /// <param name="collisionDetector"></param>
        /// <param name="refineFactory"></param>
        public AStarAlgorithmCDF(ITraceSettings traceSettings, INodeBuilder nodeBuilder, IDirectionFactory directionFactory,
            ITraceCollisionDetector<Point3d> collisionDetector, IRefineFactory<Point3d> refineFactory)
        {
            _traceSettings = traceSettings;
            _nodeBuilder = nodeBuilder;
            _directionFactory = directionFactory;
            directionFactory.Directions.ForEach(d => _searchDirections.Add(d.Round(_fractPrec)));
            _collisionDetector = collisionDetector;
            _refineFactory = refineFactory;
        }

        #region Properties

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
            _lowerBound = lowerBound.Round(_fractPrec);
            _upperBound = upperBound.Round(_fractPrec);
            return this;
        }

        /// <summary>
        /// Specify search directions.
        /// </summary>
        /// <param name="directions"></param>
        /// <returns></returns>
        public AStarAlgorithmCDF WithSearchDirections(List<Vector3d> directions)
        {
            _searchDirections.Clear();
            directions.ForEach(d => _searchDirections.Add(d.Round(_fractPrec)));
            return this;
        }

        /// <inheritdoc/>
        public List<Point3d> FindPath(Point3d startPoint, Point3d endPoint)
        {
            _startPoint = startPoint.Round(_fractPrec);
            _endPoint = endPoint.Round(_fractPrec);

            var parentNode = new PointPathFinderNode(_startPoint, _startPoint, _startPoint, PointVisualisator);
            bool found = false;
            _mOpen.Clear();
            _mClose.Clear();

            _mOpen.Push(parentNode);
            while (_mOpen.Count > 0)
            {
                //return null;
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                { Debug.WriteLine("Path search time is up."); return null; }

                parentNode = _mOpen.Pop();
                PointVisualisator?.Show(parentNode.Point);

                if (parentNode.Point.Round(_tolerance) == endPoint.Round(_tolerance))
                {
                    parentNode.Point = endPoint;
                    _mClose.Add(parentNode);
                    found = true;
                    break;
                }

                //specify available direction for nodes (successors)
                var distToANP = parentNode.LengthToANP;
                List<Vector3d> availableDirections = distToANP == 0 || distToANP >= _traceSettings.F ?
                   _searchDirections : parentNode.DirList;

                //Lets calculate each successors
                for (int i = 0; i < availableDirections.Count; i++)
                {
                    Vector3d nodeDir = availableDirections[i];
                    if (parentNode.Dir.Length != 0)
                    {
                        var angle = Math.Round(Vector3d.VectorAngle(nodeDir, parentNode.Dir).RadToDeg());
                        if (angle == 0) { }
                        else if (angle > 90)
                        //else if (angle > 90 || !_traceSettings.AList.Contains((int)angle))
                                { continue; }
                    }

                    var newNode = _nodeBuilder.BuildWithPoint(parentNode, nodeDir);

                    if (newNode.Point.IsLess(_lowerBound) || newNode.Point.IsGreater(_upperBound))
                    { continue; }

                    var mOpenInd = _mOpen.InnerList.IndexOf(newNode);
                    var mCloseInd = _mClose.IndexOf(newNode);

                    if (mOpenInd != -1 || mCloseInd != -1) { continue; }

                    newNode = _nodeBuilder.BuildWithParameters();

                    if (mOpenInd != -1 && _mOpen[mOpenInd].G <= newNode.G ||
                        mCloseInd != -1 && (_mReopenCloseNodes || _mClose[mCloseInd].G <= newNode.G))
                    { continue; }

                    if (newNode.Point.Round(_tolerance) == endPoint.Round(_tolerance)
                        && newNode.LengthToANP < _traceSettings.F)
                    { continue; }

                    //collisions check
                    if (_unpassableNodes.Contains(newNode)) { continue; }
                    else
                    {
                        if (!_passableNodes.Contains(newNode))
                        {
                            //check collisions 
                            _collisionDetector.GetCollisions(parentNode.Point, newNode.Point);
                            if (_collisionDetector.Collisions.Count > 0)
                            { _unpassableNodes.Add(newNode); continue; } //unpassable point
                            else { _passableNodes.Add(newNode); } // passable point                            
                        }
                    }

                    _mOpen.Push(newNode);
                }

                _mClose.Add(parentNode);
            }

            var pathNodes = RestorePath(found, _mClose);
            var path = _refineFactory.Refine(pathNodes);

            return path;
        }

        private List<PointPathFinderNode> RestorePath(bool found, List<PointPathFinderNode> closeNodes)
        {
            var path = new List<PointPathFinderNode>();

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
    }
}
