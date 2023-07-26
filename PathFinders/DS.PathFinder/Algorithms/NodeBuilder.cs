using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace FrancoGustavo.Algorithm
{
    public class NodeBuilder : INodeBuilder
    {
        private readonly bool _mCompactPath;
        private readonly bool _punishTurns;
        private readonly HeuristicFormula _mFormula;
        private readonly double _mHEstimate;
        private readonly Point3d _startPoint;
        private readonly Point3d _endPoint;
        private readonly double _step;
        private readonly List<Vector3d> _orths;
        private readonly double _stepCost;
        private PointPathFinderNode _node;
        private readonly int _tolerance = 3;
        private PointPathFinderNode _parentNode;
        private Line _mainLine;

        public NodeBuilder(HeuristicFormula mFormula, int mHEstimate, Point3d startPoint, Point3d endPoint, double step,
            List<Vector3d> orths, bool mCompactPath = true, bool punishTurns = true)
        {
            _mCompactPath = mCompactPath;
            _punishTurns = punishTurns;
            _mFormula = mFormula;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _mainLine = new Line(startPoint, endPoint);
            _step = step;
            _orths = orths;
            _stepCost = step / 10;
            _mHEstimate = mHEstimate * _stepCost;
        }

        public PointPathFinderNode Node => _node;

        public PointPathFinderNode BuildWithPoint(PointPathFinderNode parentNode, Vector3d nodeDir)
        {
            _parentNode = parentNode;
            _node = parentNode;
            _node.Dir = nodeDir;

            if (Math.Round(Vector3d.VectorAngle(_parentNode.Dir, _node.Dir).RadToDeg(), _tolerance) != 0)
            { _node.UpdateStep(_endPoint, _orths, _step); }

            _node.Point += _node.StepVector;

            return _node;
        }

        public PointPathFinderNode BuildWithParameters()
        {
            _node.Parent = _parentNode.Point;

            _node.G += _node.StepVector.Length;
            _node.B = _mCompactPath ? 1 * _mainLine.DistanceTo(_node.Point, true) : 0;
            _node.H = GetH(_node.Point, _endPoint, _mHEstimate);

            //set ANP for new node
            _node.ANP = _parentNode.Dir.Round(_tolerance) == _node.Dir.Round(_tolerance) ?
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
                case HeuristicFormula.Custom1:
                    break;
            }

            if (_punishTurns &&
                Math.Round(Vector3d.VectorAngle(_node.Dir, _parentNode.Dir).RadToDeg(), _tolerance) != 0)
            {
                var cost = 2 * _stepCost;
                h += cost;
            }

            return h;
        }
    }
}
