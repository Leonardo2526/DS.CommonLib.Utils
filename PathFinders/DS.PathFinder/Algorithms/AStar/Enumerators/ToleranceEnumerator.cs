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
    public class ToleranceEnumerator : IEnumerator<int>
    {
        private int _index = -1;
        private int _min = 2;
        private int _max = 10;
        private int _stepWeight = 8;
        private readonly List<int> _tolerances = new List<int>();
        private readonly IToleranceUpdater _toleranceUpdater;

        /// <summary>
        /// Instansiate enummerator to iterate through heuristic.
        /// <para>
        /// If <paramref name="inverse"/> is set to <see langword="false"/> iterator will get next values from min to max step value. 
        /// </para>
        /// <para>
        /// Otherwise from max to min step value.
        /// </para>
        /// </summary>
        /// <param name="toleranceUpdater"></param>
        /// <param name="inverse"></param>
        /// <param name="isIterationAvailable"></param>
        public ToleranceEnumerator(IToleranceUpdater toleranceUpdater, bool inverse = false, bool isIterationAvailable = true)
        {
            _toleranceUpdater = toleranceUpdater;

            var count = isIterationAvailable ? (int)Math.Floor((double)(_max - _min) / _stepWeight) : 1;
            for (int i = 0; i <= count; i++)
            {
                if (inverse) 
                {_tolerances.Add(_max - i * _stepWeight); }
                else 
                { _tolerances.Add(_min + i * _stepWeight); }
            }
        }


        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public int Current
        {
            get
            {
                if (_index == -1 || _index >= _tolerances.Count)
                { throw new ArgumentException(); }
                return _tolerances[_index];
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

            if (_index < _tolerances.Count - 1)
            {
                _index++;
                _toleranceUpdater.Update(Current);
                Log.Information($"Start pathFinding with tolerance coef {Current}.");
                return true;
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _index = 0;
            _toleranceUpdater.Update(_tolerances.FirstOrDefault());
            Log.Information("Tolerance coef is set to default value " + _tolerances.FirstOrDefault()); 
        }
    }
}
