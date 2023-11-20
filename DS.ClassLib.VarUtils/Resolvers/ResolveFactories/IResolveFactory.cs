using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// The interface is used for objects to get result from some item.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IResolveFactory<TItem, TResult>
    {
        /// <summary>
        /// Resolved results.
        /// </summary>
        IEnumerable<TResult> Results { get; }

        /// <summary>
        /// Try to resove <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// The result of resolving an <paramref name="item"/>.
        /// </returns>
        TResult TryResolve(TItem item);

        /// <summary>
        /// Try to resove <paramref name="item"/> asynchronously.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// The result of resolving an <paramref name="item"/>.
        /// </returns>
        Task<TResult> TryResolveAsync(TItem item);
    }
}
