using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DS.PathFinder.Algorithms.AStar
{
    /// <summary>
    /// An object that represents builder for <see cref="PathNode"/>.
    /// </summary>
    public class NodeBuilder : INodeBuilder
    {
        private readonly bool _mCompactPath;
        private readonly bool _punishChangeDirection;
        private readonly HeuristicFormula _mFormula;
        private readonly double _mHEstimate;
        private readonly List<Plane> _baseEndPointPlanes;
        private readonly Point3d _startPoint;
        private readonly Point3d _endPoint;
        private readonly double _step;
        private readonly List<Vector3d> _orths;
        private int _tolerance = 3;
        private readonly double _stepCost = 0.1;
        private PathNode _node;
        private PathNode _parentNode;

        /// <summary>
        /// Instansiate an object to build for <see cref="PathNode"/>.
        /// </summary>
        public NodeBuilder(HeuristicFormula mFormula, int mHEstimate, Point3d startPoint, Point3d endPoint, double step,
            List<Vector3d> orths, bool mCompactPath = false, bool punishChangeDirection = false)
        {
            _mCompactPath = mCompactPath;
            _punishChangeDirection = punishChangeDirection;
            _mFormula = mFormula;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _step = step;
            _orths = orths;
            _mHEstimate = mHEstimate * _stepCost;

            _baseEndPointPlanes = new List<Plane>()
            {
                new Plane(endPoint, orths[2]),
                new Plane(endPoint, orths[1]),
                new Plane(endPoint, orths[0])
            };
        }

        /// <summary>
        /// Path find tolerance.
        /// </summary>
        public int Tolerance { get => _tolerance; set => _tolerance = value; }

        /// <inheritdoc/>
        public PathNode Node => _node;

        /// <inheritdoc/>
        public PathNode Build(PathNode parentNode, Vector3d nodeDir)
        {
            _parentNode = parentNode;
            _node = parentNode;
            _node.Dir = nodeDir;

            if (Math.Round(Vector3d.VectorAngle(_parentNode.Dir, _node.Dir).RadToDeg()) != 0)
            { _node.StepVector = GetStep(_node, _endPoint, _baseEndPointPlanes, _step); }

            _node.StepVector = _node.StepVector.Round(_tolerance);
            _node.Point += _node.StepVector;
            _node.Point = _node.Point.Round(_tolerance);

            return _node;
        }

        /// <inheritdoc/>
        public PathNode BuildWithParameters()
        {
            _node.Parent = _parentNode.Point;

            _node.G += _node.StepVector.Length;
            _node.H = GetH(_node.Point, _endPoint, _mHEstimate);
            _node.F = _node.G + _node.H;
            //_node.B = _mCompactPath ? 1 * _mainLine.DistanceTo(_node.Point, true) : 0;

            //set ANP for new node
            _node.ANP = _parentNode.Dir == _node.Dir ?
            _parentNode.ANP : _parentNode.Point;

            return _node;
        }

        private double GetH(Point3d point, Point3d endPoint, double mHEstimate)
        {
            double h = 0;

            switch (_mFormula)
            {
                default:
                case HeuristicFormula.Manhattan:
                    h = mHEstimate * (Math.Abs(point.X - endPoint.X) + Math.Abs(point.Y - endPoint.Y) + Math.Abs(point.Z - endPoint.Z));
                    break;
                case HeuristicFormula.MaxDXDY:
                    h = mHEstimate * (Math.Max(Math.Abs(point.X - endPoint.X), Math.Abs(point.Y - endPoint.Y)));
                    break;
                case HeuristicFormula.Euclidean:
                    h = (int)(mHEstimate * Math.Sqrt(
                        Math.Pow((point.X - endPoint.X), 2) + Math.Pow((point.Y - endPoint.Y), 2) + Math.Pow((point.Z - endPoint.Z), 2))
                        );
                    break;
                case HeuristicFormula.EuclideanNoSQR:
                    h = (int)(mHEstimate * (Math.Pow((point.X - endPoint.X), 2) + Math.Pow((point.Y - endPoint.Y), 2)));
                    break;
                case HeuristicFormula.DiagonalShortCut:
                    h = mHEstimate * point.DistanceTo(endPoint);
                    break;
            }

            if (_punishChangeDirection && _parentNode.Dir.Length != 0
                && Math.Round(Vector3d.VectorAngle(_node.Dir, _parentNode.Dir).RadToDeg(), _tolerance) != 0
                )
            {
                var cost = 50 * _stepCost;
                h += cost;
            }

            return h;
        }

        private Vector3d GetStep(PathNode node, Point3d endPoint, List<Plane> baseEndPointPlanes, double step)
        {
            //get intersection point
            Point3d intersecioinPoint = GetIntersectionPoint(node, endPoint, baseEndPointPlanes);
            //_pointVisualisator?.Show(intersecioinPoint);

            //get real calculation step
            double calcStep;
            if (intersecioinPoint == node.Point)
            {
                calcStep = step;
            }
            else
            {
                var vectorLength = (intersecioinPoint - node.Point).Length;
                int stepsCount = (int)Math.Round(vectorLength / step);
                calcStep = vectorLength / stepsCount;
            }


            return Vector3d.Multiply(node.Dir, calcStep);

            Point3d GetIntersectionPoint(PathNode node, Point3d endPoint, List<Plane> baseEndPointPlanes)
            {
                Point3d intersecioinPoint = new Point3d();
                var line1 = new Line(node.Point, node.Point + node.Dir);

                List<Point3d> foundPoints = new List<Point3d>();
                foreach (var plane in baseEndPointPlanes)
                {
                    var intersection = Intersection.LinePlane(line1, plane, out double lineParam);
                    if (intersection)
                    {
                        var pointAtLine1 = line1.PointAt(lineParam).Round(_tolerance);
                        if (pointAtLine1 == node.Point) { continue; }
                        else
                        { foundPoints.Add(pointAtLine1); }
                    }
                }

                if (foundPoints.Count == 1)
                { intersecioinPoint = foundPoints.FirstOrDefault(); }
                else if (foundPoints.Count > 1)
                { intersecioinPoint = foundPoints.OrderByDescending(p => node.Point.DistanceTo(p)).Last(); }

                return intersecioinPoint;
            }
        }
    }
}
