﻿using Rhino.Geometry;
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

            if (minStep <= 0 || minStep > maxStep)
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
            int i = 0;
            DateTime totalTime1 = DateTime.Now;
            while (currentStep >= _minStep && path.Count == 0)
            {
                DateTime stepDate1 = DateTime.Now;
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                { Debug.WriteLine("Path search iteration time is up."); return path; }

                Debug.WriteLineIf(i ==0, $"Start pathFinding with step weight = {currentStep}");
                Debug.WriteLineIf(i > 0, $"Start pathFinding with {i++} step from {_stepsCount} steps with weight = {currentStep}");

                _algorithmFactory.Reset(currentStep);
                path = _algorithm?.FindPath(_startPoint, _endPoint) ?? path;

                //try find path with hEstimate iterator.
                if(path.Count ==0)
                {
                    int hEstStep = 0;
                    while(path.Count == 0 && hEstStep < 3)
                    {
                        hEstStep++;
                        _algorithmFactory.NextHestimate();
                        path = _algorithm?.FindPath(_startPoint, _endPoint) ?? path;
                    }
                }

                currentStep -= _stepTemp;

                DateTime stepDate2 = DateTime.Now;
                TimeSpan interval = stepDate2 - stepDate1;
                Debug.WriteLine("{0} {1,0:N0} ms", "Search time is :", interval.TotalMilliseconds);
                Debug.WriteLine("");
            }
            DateTime totalTime2 = DateTime.Now;
            TimeSpan totalInterval = totalTime2 - totalTime1;
            Debug.WriteLineIf(path.Count > 0, "Path was found! Length is: " + path.Count);
            Debug.WriteLineIf(path.Count == 0, "Failed to found path.");
            Debug.WriteLine("{0} {1,0:N0} ms", "Total search time is :", totalInterval.TotalMilliseconds);
            Debug.WriteLine("Total steps count is: " + i);
            return path;
        }
    }
}
