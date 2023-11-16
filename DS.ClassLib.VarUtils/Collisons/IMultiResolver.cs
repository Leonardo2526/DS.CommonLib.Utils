using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Collisions
{
    /// <summary>
    /// An object to resove tasks with set of resolvers.
    /// </summary>
    public interface IMultiResolver
    {
        /// <summary>
        /// <see cref="ICollision"/>'s solutions.
        /// </summary>
        IEnumerable<ISolution> Solutions { get; }

        /// <summary>
        /// Creator to produce collision's <see cref="IResolveTask"/>s.
        /// </summary>
        IEnumerator<IResolveTask> TaskCreator { get; }

        /// <summary>
        /// Resolvers for <see cref="IResolveTask"/>s.
        /// </summary>
        IEnumerable<ITaskResolver> TaskResolvers { get; }

        /// <summary>
        /// Try to resove <see cref="IResolveTask"/>'s produced by TaskCreator with set of TaskResolvers.
        /// </summary>
        /// <returns>
        /// Solution of <see cref="IResolveTask"/>.
        /// </returns>
        ISolution TryResolve();

        /// <summary>
        /// Try to resove <see cref="IResolveTask"/>'s produced by TaskCreator with set of TaskResolvers asynchronously.
        /// </summary>
        /// <returns>
        /// Solution of <see cref="IResolveTask"/>.
        /// </returns>
        Task<ISolution> TryResolveAsync();
    }
}
