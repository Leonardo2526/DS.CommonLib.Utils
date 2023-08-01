using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DS.PathFinder
{
    /// <summary>
    /// An object to find path by iteration through steps.
    /// </summary>
    public class PathFindIteratorByStep : IPathFindIterator<Point3d>
    {
        private readonly IAlgorithmFactory _algorithmFactory;
        private readonly Point3d _startPoint;
        private readonly Point3d _endPoint;
        private readonly double _minStep;
        private readonly double _maxStep;
        private readonly double _stepTemp;
        private readonly int _stepsCount;
        private readonly IPathFindAlgorithm<Point3d> _algorithm;


        /// <summary>
        /// Instansiate an object to find path by iteration through steps.
        /// </summary>
        public PathFindIteratorByStep(IAlgorithmFactory algorithmFactory, Point3d startPoint, Point3d endPoint,
            double minStep, double maxStep, double stepTemp)
        {
            _algorithmFactory = algorithmFactory;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _algorithm = algorithmFactory.Algorithm;

            if (minStep <= 0 || minStep > maxStep || stepTemp > (maxStep - minStep))
            { throw new ArgumentOutOfRangeException(); }

            _minStep = minStep;
            _maxStep = maxStep;
            _stepTemp = stepTemp;

            _stepsCount = (int)Math.Round((maxStep - minStep) / stepTemp);
        }


        /// <summary>
        /// Token to cancel finding path operation.
        /// </summary>
        public CancellationTokenSource TokenSource { get; set; }

        /// <inheritdoc/>
        public List<Point3d> FindPath()
        {
            var path = new List<Point3d>();


            var currentStep = _maxStep;
            int i = 1;
            while (currentStep >= _minStep && path.Count == 0)
            {
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                { Debug.WriteLine("Path search iteration time is up."); return path; }

                Debug.WriteLine($"Start {i++} from {_stepsCount} pathFinding with step = {currentStep}");
                _algorithmFactory.Reset(currentStep);
                path = _algorithm?.FindPath(_startPoint, _endPoint) ?? path;
                currentStep -= _stepTemp;
            }

            return path;
        }
    }
}
