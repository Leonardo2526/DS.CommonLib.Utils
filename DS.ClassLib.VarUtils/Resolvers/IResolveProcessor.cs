using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <inheritdoc/>
    public interface IResolveProcessor<TResult> : IResettable
    {

        /// <summary>
        /// Propagates notification that operations should be canceled.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }

        /// <summary>
        /// Factories to resolve tasks.
        /// </summary>
        IEnumerable<IResolveFactory<TResult>> ResolveFactories { get; }


        /// <summary>
        /// Try to resove a task with specific <paramref name="resolveFactories"/>.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        TResult TryResolve(IEnumerable<IResolveFactory<TResult>> resolveFactories);

        /// <summary>
        /// Try to resove a task asynchronously with specific <paramref name="resolveFactories"/>.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        Task<TResult> TryResolveAsync(IEnumerable<IResolveFactory<TResult>> resolveFactories);
    }
}
