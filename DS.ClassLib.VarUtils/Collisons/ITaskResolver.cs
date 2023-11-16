using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// An object to resove to resolve <see cref="IResolveTask"/>s.
    /// </summary>
    public interface ITaskResolver
    {
        /// <summary>
        /// <see cref="IResolveTask"/>'s solutions.
        /// </summary>
        IEnumerable<ISolution> Solutions { get; }

        /// <summary>
        /// Try to resove <paramref name="task"/>.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>
        /// <paramref name="task"/>'s solution.
        /// </returns>
        ISolution TryResolve(IResolveTask task);

        /// <summary>
        /// Try to resove <paramref name="task"/> asynchronously.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>
        /// <paramref name="task"/>'s solution.
        /// </returns>
        Task<ISolution> TryResolveAsync(IResolveTask task);
    }
}
