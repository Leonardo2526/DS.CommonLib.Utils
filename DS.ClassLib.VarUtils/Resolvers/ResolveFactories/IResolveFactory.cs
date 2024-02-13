using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used for objects to resolve a task.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IResolveFactory<TResult> : IResettable
    {
        /// <summary>
        /// Factory id.
        /// </summary>
        int Id { get;}

        /// <summary>
        /// Propagates notification that operations should be canceled.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; set; }

        /// <summary>
        /// Resolved results.
        /// </summary>
        IEnumerable<TResult> Results { get; }

        /// <summary>
        /// Try to resove a task.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        TResult TryResolve();

        /// <summary>
        /// Try to resove a task asynchronously.
        /// </summary>
        /// <returns>
        /// The result of resolving a task.
        /// </returns>
        Task<TResult> TryResolveAsync();
    }
}
