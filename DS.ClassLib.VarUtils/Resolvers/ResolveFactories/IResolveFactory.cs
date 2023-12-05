using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used for objects to resolve a task.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IResolveFactory<TResult>
    {
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
