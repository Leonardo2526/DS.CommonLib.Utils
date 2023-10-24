using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils.Enumerables;
using Rhino.Geometry.Intersect;
using DS.ClassLib.VarUtils.Points;
using DS.ClassLib.VarUtils.Graphs;
using DS.ClassLib.VarUtils.GridMap;

namespace DS.PathFinder.Algorithms.InterLine
{
    public class InterLineAlgorithm : IPathFindAlgorithm<Point3d, Point3d>
    {
        private static readonly int _cTolerance = 3;
        private static readonly int _tolerance = 5;
        private static readonly double _at = 3.DegToRad();
        private static readonly double _ct = Math.Pow(0.1, _cTolerance);
        private readonly double _rayLength = 10000;
        private readonly ILineIntersectionFactory _lineIntersectionFactory;

        public InterLineAlgorithm(ILineIntersectionFactory lineIntersectionFactory)
        {
            _lineIntersectionFactory = lineIntersectionFactory;
        }

        /// <summary>
        /// Minimum links length.
        /// </summary>
        public double MinLinkLength { get; set; }

        /// <summary>
        /// Direction at start point.
        /// </summary>
        public Vector3d StartDirection { get; set; }      

        /// <summary>
        /// Direction at end point.
        /// </summary>
        public Vector3d EndDirection { get; set; }

        public ITraceCollisionDetector<Point3d> CollisionDetector { get; set; }


        public CancellationTokenSource ExternalTokenSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Point3d> FindPath(Point3d startPoint, Point3d endPoint)
        {
            var path = new List<Point3d>() { startPoint};

            //convert boundaty conditions
            startPoint = startPoint.Round(_tolerance);
            endPoint = endPoint.Round(_tolerance);

            var dir = endPoint - startPoint;
            StartDirection = StartDirection == default ? dir : StartDirection;
            StartDirection = StartDirection.Round(_tolerance);

            EndDirection = EndDirection == default ? dir : EndDirection;
            EndDirection = EndDirection.Round(_tolerance);

            var axiliaryStartPoint = startPoint - StartDirection;
            var axiliaryEndPoint = endPoint + EndDirection;

            var graph1 = new SimpleGraph(new List<Point3d>() { axiliaryStartPoint, startPoint, endPoint });
            var graph2 = new SimpleGraph(new List<Point3d>() { startPoint, endPoint, axiliaryEndPoint });
            graph1.IsPlane(out Plane firstPlane);
            graph2.IsPlane(out Plane lastPlane);

            var planes = new List<Plane>() { Plane.WorldXY, Plane.WorldZX, Plane.WorldYZ };
            _lineIntersectionFactory.FirstNodePlanes = planes;
            _lineIntersectionFactory.LastNodePlanes = planes;
            _lineIntersectionFactory.MinLinkLength = MinLinkLength;

            var intersection =
                _lineIntersectionFactory.GetIntersection((startPoint, StartDirection), (endPoint, EndDirection));

            var cIntersection = intersection.Round(_cTolerance);

            if (double.IsNaN(intersection.X)) 
            { return null; }           
            else if (cIntersection == startPoint || cIntersection == endPoint)
            { path.Add(endPoint); return path; }
            else 
            { 
                path.Add(intersection.Round(_tolerance));
                path.Add(endPoint);            
            }


            return path;
        }
    }
}
