using DS.PathFinder.Algorithms.AStar;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DS.PathFinder.Algorithms.Enumeratos
{
    /// <summary>
    /// Enumerator to iterate through steps weight.
    /// </summary>
    public class StepEnumerator : IEnumerator<double>
    {
        private readonly static double _convertCoef = 304.8;
        private readonly static int _tolerance = 2;
        private readonly double _indexWeight;
        private readonly INodeBuilder _nodeBuilder;
        private readonly List<double> _steps = new List<double>();

        private double _minStep = 50;
        private double _maxStep;
        private int _count = 5;
        private int _index = -1;


        /// <summary>
        /// Instansiate enumerator to iterate through steps weight.
        /// <para>
        /// If <paramref name="inverse"/> is set to <see langword="false"/> iterator will get next values from min to max step value. 
        /// </para>
        /// <para>
        /// Otherwise from max to min step value.
        /// </para>
        /// </summary>
        /// <param name="nodeBuilder"></param>
        /// <param name="maxModelStep"></param>
        /// <param name="maxStep"></param>
        /// <param name="inverse"></param>
        public StepEnumerator(INodeBuilder nodeBuilder, double maxModelStep, double maxStep, bool inverse = false)
        {
            _maxStep = maxStep;
            _maxStep = _maxStep > maxModelStep ? maxModelStep : _maxStep;
            _maxStep = Math.Round(_maxStep, _tolerance);
            _indexWeight = _count == 0 ? _maxStep : (_maxStep - _minStep) / _count;
            _indexWeight = Math.Round(_indexWeight, _tolerance);
            _nodeBuilder = nodeBuilder;
            for (int i = 0; i <= _count; i++)
            {
                if (inverse) { _steps.Add(_maxStep - i * _indexWeight); }
                else { _steps.Add(_minStep + i * _indexWeight); }
            }
        }

        /// <summary>
        /// Minimum step value.
        /// </summary>
        public double MinStep { get => _minStep; set => _minStep = value; }

        /// <summary>
        /// Maximum step value.
        /// </summary>
        public double MaxStep { get => _maxStep; set => _maxStep = value; }

        /// <summary>
        /// Iterator steps count from initial value.
        /// </summary>
        public int Count { get => _count; set => _count = value; }

        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public double Current
        {
            get
            {
                if (_index == -1 || _index >= _steps.Count)
                { throw new ArgumentException(); }
                return _steps[_index];
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_index < _steps.Count - 1)
            {
                _index++;
                _nodeBuilder.Step = _steps[_index] / _convertCoef;
                Log.Information($"Start pathFinding with {_index + 1} step from {_steps.Count} steps with weight = {_steps[_index]}");
                return true;
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _index = -1;
        }
    }
}
