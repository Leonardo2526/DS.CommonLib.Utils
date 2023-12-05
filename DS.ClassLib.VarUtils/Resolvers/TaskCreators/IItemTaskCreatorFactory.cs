using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers.TaskCreators
{
    /// <summary>
    /// The interface is used to produce <see cref="IItemTaskCreator{TItem, TTask}"/>.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TTask"></typeparam>
    public interface IItemTaskCreatorFactory<TItem, TTask>
    {
        /// <summary>
        /// Create <see cref="IItemTaskCreator{TItem, TTask}"/>.
        /// </summary>
        /// <returns></returns>
        IItemTaskCreator<TItem, TTask> Create();
    }
}
