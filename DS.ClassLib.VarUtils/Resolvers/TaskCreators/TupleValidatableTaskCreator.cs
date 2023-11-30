using DS.RevitLib.Utils.Various;
using Serilog;
using System;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to create validatable <see cref="ValueTuple{T1, T2}"/> tasks.
    /// </summary>
    /// <typeparam name="Titem"></typeparam>
    /// <typeparam name="Ttask"></typeparam>
    public class TupleValidatableTaskCreator<Titem, Ttask> : ITaskCreator<Titem, (Ttask, Ttask)>
    {
        private readonly IValidatableSelector<Ttask> _selector;


        /// <summary>
        /// Instansiate an object to create <see cref="ValueTuple{T, T}"/> tasks with <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector"></param>
        public TupleValidatableTaskCreator(IValidatableSelector<Ttask> selector)
        {
            _selector = selector;
        }

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Create <typeparamref name="Ttask"/> task.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual (Ttask, Ttask) CreateTask(Titem item = default)
        {
            var v1 = _selector.Select();
            if (v1 == null) { return default; }

            var v2 = _selector.Select();
            if (v2 == null) { return default; }

            return (Convert(v1), Convert(v2));
        }

        /// <summary>
        /// Convert <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Converted <paramref name="item"/>.
        /// </returns>
        protected virtual Ttask Convert(Ttask item) { return item; }
    }
}
