using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers.TaskCreators
{
    /// <summary>
    /// The interface is used to produce <see cref="ITaskCreator{TTask}"/>.
    /// </summary>
    /// <typeparam name="TTask"></typeparam>
    public interface ITaskCreatorFactory<TTask>
    {
        /// <summary>
        /// Create <see cref="ITaskCreator{TTask}"/>.
        /// </summary>
        /// <returns></returns>
        ITaskCreator<TTask> Create();
    }
}
