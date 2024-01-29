using Serilog;
using System;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Iterator over a generic collection with abilily to move forward and back.
    ///  It has ability to assign some <see cref="Predicate{T}"/> on move.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionEnumerator<T>(IEnumerable<T> items) :
        Enumerator<T>(items)
    {
        /// <summary>
        /// Action on <see cref="MoveNext"/>
        /// </summary>
        public Predicate<T> MoveAction { get; set; }

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public override bool MoveNext()
        {
            if (_index < _buffer.Count - 1)
            {
                ++_index;
                return InvokeAction();
            }

            if (_enumerator.MoveNext())
            {
                _buffer.Add(_enumerator.Current);
                ++_index;                
                return InvokeAction();
            }

            return false;

            bool InvokeAction()
            {
                var canMove = MoveAction is null || MoveAction(Current);
                if (!canMove)
                { Logger?.Error($"Failed to perform {GetType().Name} next action."); }
                return canMove;
            }
        }
    }
}
