using Castle.Core.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Iterator over a generic collection with abilily to move forward and back.
    ///  It has ability to assign some <see cref="Predicate{T}"/> on move.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionTwoWayEnumerator<T> : ITwoWayEnumerator<T>
    {
        private readonly ITwoWayEnumerator<T> _twoWayEnumerator;
        private int _index;
        private List<T> _buffer;
        private Predicate<T> _nextAction;
        private Predicate<T> _backAction;
        private Predicate<T> _previousAction;

        /// <summary>
        /// Instansiate action iterator over a generic collection with abilily to move forward and back 
        /// with <paramref name="twoWayEnumerator"/>.
        /// </summary>
        /// <param name="twoWayEnumerator"></param>
        public ActionTwoWayEnumerator(
            ITwoWayEnumerator<T> twoWayEnumerator)
        {
            _twoWayEnumerator = twoWayEnumerator;
            _buffer = new List<T>();
            _index = -1;
        }

        /// <summary>
        /// Instansiate iterator over a generic collection with abilily to move forward and back
        /// through <paramref name="items"/> with default <see cref="TwoWayEnumerator{T}"/>.
        /// </summary>
        /// <param name="items"></param>
        public ActionTwoWayEnumerator(IEnumerable<T> items)
        {
            _twoWayEnumerator = new TwoWayEnumerator<T>(items);
            _buffer = new List<T>();
            _index = -1;
        }

        /// <summary>
        /// Current index.
        /// </summary>
        public int Index => _index;

        /// <summary>
        /// Action on <see cref="MoveNext"/>
        /// </summary>
        public Predicate<T> NextAction
        { get => _nextAction; set => _nextAction = value; }

        /// <summary>
        /// Action on <see cref="MoveBack"/>
        /// </summary>
        public Predicate<T> BackAction
        { get => _backAction; set => _backAction = value; }

        /// <summary>
        /// Action with item that previous to <see cref="Current"/>.
        /// </summary>
        public Predicate<T> PreviousAction
        { get => _previousAction; set => _previousAction = value; }

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public T Current => _twoWayEnumerator.Current;

        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public void Dispose() => _twoWayEnumerator.Dispose();

        /// <inheritdoc/>
        public bool MoveBack()
        {
            if (!_twoWayEnumerator.MoveBack())
            { return false; }

            _index--;

            if (_previousAction is not null && !_previousAction(_buffer[_index + 1]))
            { Logger?.Error($"Failed to perform {GetType().Name} previous action."); }

            if (_backAction is not null && !_backAction(Current))
            { Logger?.Error($"Failed to perform {GetType().Name} next action."); }

            return true;
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (!_twoWayEnumerator.MoveNext())
            { return false; }

            _index++;
            _buffer.Add(Current);

            if (_index > 0 && _previousAction is not null && !_previousAction(_buffer[_index - 1]))
            { Logger?.Error($"Failed to perform {GetType().Name} previous action."); }

            if (_nextAction is not null && !_nextAction(Current))
            { Logger?.Error($"Failed to perform {GetType().Name} next action."); }

            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _buffer.Clear();
            _index = -1;
            _twoWayEnumerator.Reset();
        }
    }
}
