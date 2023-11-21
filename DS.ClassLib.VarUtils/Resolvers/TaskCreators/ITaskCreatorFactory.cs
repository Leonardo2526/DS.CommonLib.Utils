using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers.TaskCreators
{
    /// <summary>
    /// The interface is used to produce <see cref="ITaskCreator{TItem, TTask}"/>.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TTask"></typeparam>
    public interface ITaskCreatorFactory<TItem, TTask>
    {
        /// <summary>
        /// Create <see cref="ITaskCreator{TItem, TTask}"/>.
        /// </summary>
        /// <returns></returns>
        ITaskCreator<TItem, TTask> Create();
    }
}
