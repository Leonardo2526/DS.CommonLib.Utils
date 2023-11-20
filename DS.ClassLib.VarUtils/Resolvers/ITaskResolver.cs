using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used for objects to resolve tasks.
    /// </summary>
    /// <typeparam name="TTask"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ITaskResolver<TTask, TResult>
    {
        /// <summary>
        /// The results of resolving a task.
        /// </summary>
        IEnumerable<TResult> Results { get; }

        /// <summary>
        /// Try to resove <paramref name="task"/>.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>
        /// The result of resolving a <paramref name="task"/>.
        /// </returns>
        TResult TryResolve(TTask task);

        /// <summary>
        /// Try to resove <paramref name="task"/> asynchronously.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>
        /// The result of resolving a <paramref name="task"/>.
        /// </returns>
        Task<TResult> TryResolveAsync(TTask task);
    }
}
