using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DS.PathFinder.Algorithms.InterLine
{
    /// <summary>
    /// An object that represents algorithm to find path between points by <see cref="Line"/>'s intersections.
    /// </summary>
    public class InterLineAlgorithm : IPathFindAlgorithm<Point3d, Point3d>
    {
        private static readonly int _cTolerance = 3;
        private static readonly int _tolerance = 5;
        private static readonly double _ct = Math.Pow(0.1, _cTolerance);
        private readonly ILineIntersectionFactory _lineIntersectionFactory;

        /// <summary>
        ///  Instantiate an object to find path between points by <see cref="Line"/>'s intersections.
        /// </summary>
        /// <param name="lineIntersectionFactory"></param>
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

        /// <inheritdoc/>
        public CancellationTokenSource ExternalToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public List<Point3d> FindPath(Point3d startPoint, Point3d endPoint)
        {
            var path = new List<Point3d>() { startPoint };

            //convert boundaty conditions
            startPoint = startPoint.Round(_tolerance);
            endPoint = endPoint.Round(_tolerance);

            StartDirection = StartDirection.Round(_tolerance);
            EndDirection = EndDirection.Round(_tolerance);

            var planes = new List<Plane>() { Plane.WorldXY, Plane.WorldZX, Plane.WorldYZ };
            _lineIntersectionFactory.FirstNodePlanes = planes;
            _lineIntersectionFactory.LastNodePlanes = planes;
            _lineIntersectionFactory.MinLinkLength = MinLinkLength;

            var intersection =
                _lineIntersectionFactory.GetIntersection((startPoint, StartDirection), (endPoint, EndDirection));


            if (double.IsNaN(intersection.X))
            { return null; }

            if (intersection.DistanceTo(startPoint) > _ct)
            { path.Add(intersection); }

            if(intersection.DistanceTo(endPoint) > _ct)
            { path.Add(endPoint); }

            return path;
        }
    }
}
