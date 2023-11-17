using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to resove tasks with set of resolvers.
    /// </summary>
    public interface IMultiResolver<TTask> where TTask : IResolveTask
    {
        /// <summary>
        /// Solutions.
        /// </summary>
        IEnumerable<ISolution> Solutions { get; }

        /// <summary>
        /// Creator to produce collision's <typeparamref name="TTask"/>
        /// </summary>
        IEnumerator<TTask> TaskCreator { get; }

        /// <summary>
        /// Resolvers for <see cref="IResolveTask"/>s.
        /// </summary>
        IEnumerable<ITaskResolver<TTask>> TaskResolvers { get; }

        /// <summary>
        /// Try to resove <see cref="IResolveTask"/>'s produced by TaskCreator with set of TaskResolvers.
        /// </summary>
        /// <returns>
        /// Solution of <see cref="IResolveTask"/>.
        /// </returns>
        ISolution TryResolve();

        /// <summary>
        /// Try to resove <typeparamref name="TTask"/>'s produced by TaskCreator with set of TaskResolvers asynchronously.
        /// </summary>
        /// <returns>
        /// Solution of <typeparamref name="TTask"/>.
        /// </returns>
        Task<ISolution> TryResolveAsync();
    }
}
