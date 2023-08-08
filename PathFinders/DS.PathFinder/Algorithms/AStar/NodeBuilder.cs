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
        private int _heuristic;
        private readonly List<Plane> _baseEndPointPlanes;
        private readonly Point3d _startPoint;
        private readonly Point3d _endPoint;
        private readonly double _stepCost = 0.01;
        private int _tolerance = 3;
        private int _cTolerance = 2;
        private double _cfTolerance = 0.03;
        private double _gcost = 200;
        private PathNode _node;
        private PathNode _parentNode;

        /// <summary>
        /// Instansiate an object to build for <see cref="PathNode"/>.
        /// </summary>
        public NodeBuilder(HeuristicFormula mFormula, Point3d startPoint, Point3d endPoint, double step,
            List<Vector3d> orths, bool mCompactPath = false, bool punishChangeDirection = false)
        {
            _mCompactPath = mCompactPath;
            _punishChangeDirection = punishChangeDirection;
            _mFormula = mFormula;
            _startPoint = startPoint;
            _endPoint = endPoint;
            Step = step;
            _gcost *= _stepCost;

            _baseEndPointPlanes = new List<Plane>()
            {
                new Plane(endPoint, orths[2]),
                new Plane(endPoint, orths[1]),
                new Plane(endPoint, orths[0])
            };
        }

        /// <inheritdoc/>
        public double Step { get; set; }

        /// <summary>
        /// Path find tolerance.
        /// </summary>
        public int Tolerance { get => _tolerance; set => _tolerance = value; }

        /// <summary>
        /// Compound numbers tolerance.
        /// </summary>
        public int CTolerance { get => _cTolerance; set => _cTolerance = value; }

        /// <inheritdoc/>
        public PathNode Node => _node;

        /// <summary>
        /// Specifies visualisator to show points.
        /// </summary>
        public IPointVisualisator<Point3d> PointVisualisator { get; set; }

        /// <inheritdoc/>
        public int Heuristic { get => _heuristic; set => _heuristic = value; }

        /// <inheritdoc/>
        public PathNode Build(PathNode parentNode, Vector3d nodeDir)
        {
            _parentNode = parentNode;
            _node = parentNode;
            _node.Dir = nodeDir;

            if (Math.Round(_node.StepVector.Length, _cTolerance) == 0 ||
                Math.Round(Vector3d.VectorAngle(_parentNode.Dir, _node.Dir).RadToDeg()) != 0)
            { _node.StepVector = GetStep(_node, _endPoint, _baseEndPointPlanes, Step); }

            if (Math.Round(_node.StepVector.Length, _cTolerance) == 0)
            {
                throw new InvalidOperationException("StepVector length is less than tolerance.");
            }

            _node.StepVector = _node.StepVector.Round(_tolerance);
            _node.Point += _node.StepVector;
            _node.Point = _node.Point.Round(_tolerance);

            return _node;
        }

        /// <inheritdoc/>
        public PathNode BuildWithParameters()
        {
            _node.Parent = _parentNode.Point;

            var gValue = _node.StepVector.Length;
            //set ANP for new node
            if (_parentNode.Dir == _node.Dir)
            {
                _node.ANP = _parentNode.ANP;
            }
            else
            {
                _node.ANP = _parentNode.Point;
                if (_punishChangeDirection)
                { gValue *= _gcost; }
            }

            _node.G += gValue;
            _node.H = GetH(_node.Point, _endPoint, _heuristic * _stepCost);
            _node.F = _node.G + _node.H;
            //_node.B = _mCompactPath ? 1 * _mainLine.DistanceTo(_node.Point, true) : 0;


            return _node;
        }

        /// <summary>
        /// Build with <paramref name="step"/>.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public NodeBuilder WithStep(double step)
        {
            Step = step; return this;
        }

        private double GetH(Point3d point, Point3d endPoint, double _heuristic)
        {
            double h = 0;

            switch (_mFormula)
            {
                default:
                case HeuristicFormula.Manhattan:
                    h = _heuristic * (Math.Abs(point.X - endPoint.X) + Math.Abs(point.Y - endPoint.Y) + Math.Abs(point.Z - endPoint.Z));
                    break;
                case HeuristicFormula.MaxDXDY:
                    h = _heuristic * (Math.Max(Math.Abs(point.X - endPoint.X), Math.Abs(point.Y - endPoint.Y)));
                    break;
                case HeuristicFormula.Euclidean:
                    h = (int)(_heuristic * Math.Sqrt(
                        Math.Pow((point.X - endPoint.X), 2) + Math.Pow((point.Y - endPoint.Y), 2) + Math.Pow((point.Z - endPoint.Z), 2))
                        );
                    break;
                case HeuristicFormula.EuclideanNoSQR:
                    h = (int)(_heuristic * (Math.Pow((point.X - endPoint.X), 2) + Math.Pow((point.Y - endPoint.Y), 2)));
                    break;
                case HeuristicFormula.DiagonalShortCut:
                    h = _heuristic * point.DistanceTo(endPoint);
                    break;
            }

            return h;
        }

        private Vector3d GetStep(PathNode node, Point3d endPoint, List<Plane> baseEndPointPlanes, double step)
        {
            bool b = false;
            //get intersection point
            Point3d intersecioinPoint = GetIntersectionPoint(node, endPoint, baseEndPointPlanes);
            if (b)
            {
                PointVisualisator?.Show(intersecioinPoint);
            }

            //get real calculation step
            double calcStep;
            if (intersecioinPoint == node.Point)
            {
                calcStep = step;
            }
            else
            {
                var vectorLength = (intersecioinPoint - node.Point).Length;
                int stepsCount = (int)Math.Ceiling(vectorLength / step);
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
                        if (pointAtLine1.DistanceTo(node.Point) < _cfTolerance) { continue; }
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
