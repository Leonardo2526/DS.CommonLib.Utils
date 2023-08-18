using DS.PathFinder.Algorithms.AStar;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DS.PathFinder.Algorithms.Enumeratos
{

    /// <summary>
    /// Enummerator to iterate through heuristic.
    /// </summary>
    public class HeuristicEnumerator : IEnumerator<int>
    {
        private readonly INodeBuilder _nodeBuilder;
        private int _index = -1;
        private int _minHeuristic = 100;
        private int _maxHeuristic = 150;
        private int _stepWeight = 50;
        private readonly List<int> _heuristics = new List<int>();

        /// <summary>
        /// Instansiate enummerator to iterate through heuristic.
        /// <para>
        /// If <paramref name="inverse"/> is set to <see langword="false"/> iterator will get next values from min to max step value. 
        /// </para>
        /// <para>
        /// Otherwise from max to min step value.
        /// </para>
        /// </summary>
        /// <param name="nodeBuilder"></param>
        /// <param name="inverse"></param>
        public HeuristicEnumerator(INodeBuilder nodeBuilder, bool inverse = false)
        {
            //_index = inverse ? _maxHeuristic : _minHeuristic;
            _nodeBuilder = nodeBuilder;
            var count = (int)Math.Floor((double)(_maxHeuristic - _minHeuristic) / _stepWeight);
            for (int i = 0; i <= count; i++)
            {
                if (inverse) 
                {_heuristics.Add(_maxHeuristic - i * _stepWeight); }
                else 
                { _heuristics.Add(_minHeuristic + i * _stepWeight); }
            }
        }


        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public int Current
        {
            get
            {
                if (_index == -1 || _index >= _heuristics.Count)
                { throw new ArgumentException(); }
                return _heuristics[_index];
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
            if (_index == -1) { return false; }

            if (_index < _heuristics.Count - 1)
            {
                _index++;
                _nodeBuilder.Heuristic = Current;
                Log.Information($"Start pathFinding with {_nodeBuilder.Heuristic} heuristic");
                return true;
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _index = 0;
            _nodeBuilder.Heuristic = _heuristics.FirstOrDefault();
            Log.Information("Heuristic is set to default value " + _heuristics.FirstOrDefault()); 
        }
    }
}
