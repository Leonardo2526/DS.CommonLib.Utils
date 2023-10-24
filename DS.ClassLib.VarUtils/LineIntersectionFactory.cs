using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Collisons;
using DS.ClassLib.VarUtils.Enumerables;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    public class LineIntersectionFactory : ILineIntersectionFactory
    {
        private static readonly int _cTolerance = 3;
        private static readonly int _tolerance = 5;
        private static readonly double _at = 3.DegToRad();
        private static readonly double _ct = Math.Pow(0.1, _cTolerance);
        private readonly double _rayLength = 10000;
        private readonly List<int> _angles;
        private readonly IDirectionIteratorBuilder _iteratorBuilder;
        private IEnumerator<Vector3d> _firstNodeIterator;
        private IEnumerator<Vector3d> _lastNodeIterator;
        private Point3d _nullPoint = new(double.NaN, double.NaN, double.NaN);

        private ITraceCollisionDetector<Point3d> _collisionDetector;
        private Basis3d _basis;
        private Point3d _firstNode;
        private Point3d _lastNode;

        public LineIntersectionFactory(List<int> angles, IDirectionIteratorBuilder iteratorBuilder)
        {
            _angles = angles;
            _iteratorBuilder = iteratorBuilder;
        }

        public Point3d IntersectionPoint { get; private set; }

        /// <summary>
        /// Minimum links length.
        /// </summary>
        public double MinLinkLength { get; set; }

        public List<Plane> FirstNodePlanes { get; set; }
        public List<Plane> LastNodePlanes { get; set; }

        public Point3d GetIntersection(
            (Point3d point, Vector3d direction) node1, (Point3d point, Vector3d direction) node2)
        {
            Point3d intersectionPoint = _nullPoint;
            double s = double.MaxValue;

            var point1 = node1.point;
            var point2 = node2.point;

            _firstNodeIterator = _iteratorBuilder.Build(FirstNodePlanes, _angles, node1.direction);
            _lastNodeIterator = _iteratorBuilder.Build(LastNodePlanes, _angles, - node2.direction);

            while (_firstNodeIterator.MoveNext())
            {
                var item1 = (Vector3d)_firstNodeIterator.Current.Round(_cTolerance);
                var line1 = new Line(point1, item1, _rayLength);
                _lastNodeIterator.Reset();
                while (_lastNodeIterator.MoveNext())
                {
                    var item2 = (Vector3d)_lastNodeIterator.Current.Round(_cTolerance);
                    var line2 = new Line(point2, item2, _rayLength);

                    //check angle between lines
                    var a1 = (int)Vector3d.VectorAngle(line1.UnitTangent, -line2.UnitTangent).RadToDeg();
                    if (line1.UnitTangent.IsParallelTo(line2.UnitTangent, _at) == 0
                        && !_angles.Contains(a1))
                    { continue; }

                    if (Intersection.LineLine(line1, line2, out double a, out double b, _ct, true))
                    {
                        var p = line1.PointAt(a);

                        var d1 = Math.Round(point1.DistanceTo(p), _cTolerance);
                        var d2 = Math.Round(point2.DistanceTo(p), _cTolerance);
                        var sum = d1 + d2;
                        if ((d1 == 0 || d1 >= MinLinkLength) && (d2 ==0 || d2 >= MinLinkLength) && sum < s)
                        {
                            if (_collisionDetector is null)
                            { intersectionPoint = p; s = sum; }
                            else
                            {
                                var basis1 = _basis.GetBasis(line1.UnitTangent);
                                var basis2 = _basis.GetBasis(line2.UnitTangent);

                                var collisions1 = _collisionDetector.GetCollisions(point1, p, basis1, _firstNode, _lastNode, _cTolerance);
                                var collisions2 = _collisionDetector.GetCollisions(point2, p, basis2, _firstNode, _lastNode, _cTolerance);

                                if (collisions1.Count == 0 && collisions2.Count == 0)
                                { intersectionPoint = p; s = sum; }
                            }
                        }
                    }
                }
            }

            return IntersectionPoint = intersectionPoint;
        }

        public ILineIntersectionFactory WithDetector(ITraceCollisionDetector<Point3d> collisionDetector,
            Basis3d basis, Point3d firstNode, Point3d lastNode)
        {
            _collisionDetector = collisionDetector;
            _basis = basis;
            _firstNode = firstNode;
            _lastNode = lastNode;
            return this;
        }
    }
}
