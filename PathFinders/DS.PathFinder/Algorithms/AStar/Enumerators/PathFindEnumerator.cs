using Rhino.Geometry;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DS.PathFinder.Algorithms.Enumeratos
{
    /// <summary>
    /// Enumerator to iterate through path find values.
    /// </summary>
    public class PathFindEnumerator : IEnumerator<List<Point3d>>
    {
        private readonly static int _totalTokenTime = 200000;
        private readonly IEnumerator _stepEnumerator;
        private readonly IEnumerator _heuristicEnumerator;
        private readonly IEnumerator<int> _toleranceEnumerator;
        private readonly IPathFindAlgorithm<Point3d, Point3d> _algorithm;
        private readonly Point3d _startPoint;
        private readonly Point3d _endPoint;
        private int _index;
        private CancellationTokenSource _totalTokenSource;
        private DateTime _totalTime1;

        private List<Point3d> _path = new List<Point3d>();
        /// <summary>
        /// Instansiate enumerator to iterate through path find values.
        /// </summary>
        public PathFindEnumerator(IEnumerator stepEnumerator, 
            IEnumerator heuristicEnumerator, 
            IEnumerator<int> toleranceEnumerator,
            IPathFindAlgorithm<Point3d, Point3d> algorithmFactory, 
            Point3d startPoint, Point3d endPoint)
        {
            _stepEnumerator = stepEnumerator;
            _heuristicEnumerator = heuristicEnumerator;
            _toleranceEnumerator = toleranceEnumerator;
            _algorithm = algorithmFactory;
            _startPoint = startPoint;
            _endPoint = endPoint;
            Reset();
        }

        object IEnumerator.Current => _path;

        /// <inheritdoc/>
        public List<Point3d> Current => _path;

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Outer token.
        /// </summary>
        public CancellationTokenSource TokenSource { get; set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            _index++;

            if (_path.Count > 0)
            {
                Log.Information("Path was found! Length is " + _path.Count);
                Log.Information($"Step is {(double)_stepEnumerator.Current} mm.");
                Log.Information($"Heuristic is {(int)_heuristicEnumerator.Current} %.");
                Log.Information($"Tolerance is {(int)_toleranceEnumerator.Current}");
                TotalTimeOutput();
                return false;
            }

            if (_totalTokenSource is not null && _totalTokenSource.Token.IsCancellationRequested)
            { Log.Information("Path search iteration time is up."); return false; }
            if (TokenSource != null && TokenSource.IsCancellationRequested)
            { Log.Information("Path search iteration time is up."); return false; }

            if (!_heuristicEnumerator.MoveNext())
            {
                if (!_toleranceEnumerator.MoveNext())
                {
                    if (!_stepEnumerator.MoveNext())
                    {
                        Log.Error("No available path exis.");
                        TotalTimeOutput();
                        return false;
                    }
                    else
                    { _heuristicEnumerator.Reset(); _toleranceEnumerator.Reset(); }
                }
            }

            DateTime stepDate1 = DateTime.Now;
            _path = _algorithm.FindPath(_startPoint, _endPoint) ?? _path;
            DateTime stepDate2 = DateTime.Now;

            TimeSpan interval = stepDate2 - stepDate1;
            var logMessage = $"Iteration {_index + 1} search time is {(int)interval.TotalMilliseconds} ms";
            Logger?.Information(logMessage);
            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _index = -1;
            _totalTokenSource = new CancellationTokenSource(_totalTokenTime);
            _totalTime1 = DateTime.Now;
        }

        private void TotalTimeOutput()
        {
            DateTime totalTime2 = DateTime.Now;
            TimeSpan totalInterval = totalTime2 - _totalTime1;
            Log.Information($"Total search time is {(int)totalInterval.TotalMilliseconds} ms");
            Log.Information($"Iterations count is {_index}.");
        }
    }
}
