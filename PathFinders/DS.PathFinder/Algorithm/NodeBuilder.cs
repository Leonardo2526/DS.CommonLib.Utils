using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FrancoGustavo.Algorithm
{
    public class NodeBuilder : INodeBuilder
    {
        private readonly bool _mCompactPath;
        private readonly bool _punishTurns;
        private readonly HeuristicFormula _mFormula;
        private readonly double _mHEstimate;
        private readonly Point3D _startPoint;
        private readonly Point3D _endPoint;
        private readonly double _step;
        private readonly OrthoBasis _stepBasis;
        private readonly List<Vector3D> _orths;
        private readonly ITraceCollisionDetector _collisionDetector;
        private readonly double _offset;
        private readonly double _stepCost;
        private PointPathFinderNode _node;
        //private Vector3D _stepDir;
        private Vector3D _equalizer;
        private readonly int _tolerance = 3;
        private PointPathFinderNode _parentNode;
        private Vector3D _mainVector;
        private Line _mainLine;

        public NodeBuilder(HeuristicFormula mFormula, int mHEstimate, Point3D startPoint, Point3D endPoint, double step, 
            List<Vector3D> orths, ITraceCollisionDetector collisionDetector, double offset, 
            bool mCompactPath = true, bool punishTurns = true)
        {
            _mCompactPath = mCompactPath;
            _punishTurns = punishTurns;
            _mFormula = mFormula;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _mainVector = endPoint - startPoint;
            _mainLine = new Line(startPoint.Convert(), endPoint.Convert());
            _step = step;
            //_stepBasis = stepBasis;
            _orths = orths;
            _collisionDetector = collisionDetector;
            _offset = offset;
            _stepCost = step / 10;
            _mHEstimate = mHEstimate * _stepCost;
        }

        public PointPathFinderNode Node => _node;

        public PointPathFinderNode BuildWithPoint(PointPathFinderNode parentNode, Vector3D nodeDir)
        {
            _parentNode = parentNode;
            _node = parentNode;
            _node.Dir = nodeDir;

            //lets try to recoup delta step incurrancy          
            //_equalizer = _node.DeltaVector.Length > 0 ? GetEqualizer(nodeDir, _node.DeltaVector) : new Vector3D();

            //_node.Dir += _equalizer;
            if(Math.Round(Vector3D.AngleBetween(_parentNode.Dir, _node.Dir), _tolerance) !=0 )
            //if(!_parentNode.Dir.IsAlmostEqualTo(_node.Dir, _tolerance))
                { _node.UpdateStep(_endPoint, _orths, _step);}

            _node.Point += _node.StepVector;

            return _node;
        }

        public PointPathFinderNode BuildWithParameters()
        {
            _node.Parent = _parentNode.Point;

            _node.G += GetG(_node.Point, _startPoint, _endPoint, _parentNode.Point);
            _node.H = GetH(_node.Point, _endPoint, _mHEstimate);
            _node.B = GetB(_node.Point);

            //set ANP for new node
            _node.ANP = _parentNode.Dir.IsAlmostEqualTo(_node.Dir, _tolerance) ?
            _parentNode.ANP : _parentNode.Point;

            ////calculate delta step
            //if (_equalizer.Length > 0)
            //{ _node.DeltaVector -= _equalizer; _node.DeltaVector = _node.DeltaVector.Round(_tolerance); }
            ////{ _node.DeltaStep = new Vector3D(Math.Abs(_recoupVector.X), Math.Abs(_recoupVector.Y), Math.Abs(_recoupVector.Z)); }
            //else
            //{
            //    var deltaVector = GetDelta(_node.Dir, _stepBasis);
            //    _node.DeltaVector = deltaVector;

            //    if (_node.DeltaVector.Length > 0)
            //    {

            //    }
            //}


            return _node;
        }

        private double GetH(Point3D point, Point3D endPoint, double mHEstimate)
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

            if(_punishTurns && 
                Math.Round(Vector3D.AngleBetween(_node.Dir, _parentNode.Dir), _tolerance) !=0)
            {
                var cost = 2 * _stepCost;
                h += cost;
            }

            return h;
        }

        private double GetG(Point3D point, Point3D startPoint, Point3D endPoint, Point3D parentPoint)
        {
            double g = _node.StepVector.Length;

            //if (_mCompactPath)
            //{
            //    var cost = 5 * _stepCost;
            //    foreach (var orth in _orths)
            //    {
            //        var mult = (orth.Length + _offset) / _offset;
            //        var moveOrth = Vector3D.Multiply(orth, mult);
            //        var p = _node.Point + moveOrth;
            //        _collisionDetector.GetCollisions(p, _node.Point);
            //        if (_collisionDetector.Collisions.Count > 0)
            //        {
            //            g -= cost;
            //            break;
            //        }

            //    }
            //}

            return g;
        }

        private double GetB(Point3D point)
        {
            if (!_mCompactPath) { return 0; }
            var cost = 1 * _stepCost;
            return cost * _mainLine.DistanceTo(point.Convert(), true);
        }

        private Vector3D GetEqualizer(Vector3D nodeDir, Vector3D deltaVector)
        {
            var equalizer = new Vector3D();

            if (!IsOrthoDirection(nodeDir, _stepBasis)) { return equalizer; }
            equalizer = deltaVector.ProjectOn(nodeDir).Round(_tolerance);
            //var angle = Vector3D.AngleBetween(deltaVector, nodeDir);
            //double deltaLength = Math.Abs(deltaVector.Length * Math.Cos(angle.DegToRad()));

            //if (Math.Round(deltaLength, _tolerance) != 0)
            //{
            //    double mult = deltaLength / nodeDir.Length;
            //    equalizer = Vector3D.Multiply(nodeDir, mult);
            //}

            return equalizer;
        }

        private Vector3D GetDelta(Vector3D nodeDir, OrthoBasis stepBasis)
        {
            var mainPlaneProj = GetProj(nodeDir, stepBasis);
            var delta = (mainPlaneProj - nodeDir).Round(_tolerance);
            //var mainOrthProj = mainPlaneProj.ProjectOn(nodeDir);
            //double deltaLength = Math.Round(mainOrthProj.Length - nodeDir.Length, _tolarance);
            //var delta = Node.NormDir * deltaLength;

            Vector3D totalDelta = delta + Node.DeltaVector;

            //get total delta to fraction by stepBasis.
            var cx = (int)Math.Floor(totalDelta.X / _stepBasis.X.Length);
            var cy = (int)Math.Floor(totalDelta.Y / _stepBasis.Y.Length);
            var cz = (int)Math.Floor(totalDelta.Z / _stepBasis.Z.Length);

            var totx = totalDelta.X - cx * _stepBasis.X.Length;
            var toty = totalDelta.Y - cy * _stepBasis.Y.Length;
            var totz = totalDelta.Z - cz * _stepBasis.Z.Length;

            return new Vector3D(totx, toty, totz).Round(_tolerance);
        }

        private Vector3D GetProj(Vector3D nodeDir, OrthoBasis stepBasis)
        {
            var (minOrth1, minAngle1) = nodeDir.GetWithMinAngle(_orths);
            if (Math.Round(minAngle1, _tolerance) == 0) { return minOrth1; }
            var orths = _orths.Where(x => !x.IsAlmostEqualTo(minOrth1, _tolerance)).ToList();
            var (minOrth2, minAngle2) = nodeDir.GetWithMinAngle(orths);

            return minOrth1 + minOrth2;
        }

        private double GetDeltaCoord(double stepDirCoord, double stepCoord)
        {
            int d = (int)Math.Ceiling(Math.Abs(stepDirCoord / stepCoord));
            var coord = Math.Round(d * stepCoord - Math.Abs(stepDirCoord), _tolerance);

            return Math.Round(coord, _tolerance) == 0 ? 0 : coord;
        }

        private bool IsOrthoDirection(Vector3D nodeDir, OrthoBasis orthoBasis)
        {
            var angle = Math.Round(Vector3D.AngleBetween(nodeDir, orthoBasis.X), _tolerance);
            if (angle == 0 || angle == 180) return true;
            angle = Math.Round(Vector3D.AngleBetween(nodeDir, orthoBasis.Y), _tolerance);
            if (angle == 0 || angle == 180) return true;
            angle = Math.Round(Vector3D.AngleBetween(nodeDir, orthoBasis.Z), _tolerance);
            if (angle == 0 || angle == 180) return true;

            return false;
        }

    }
}
