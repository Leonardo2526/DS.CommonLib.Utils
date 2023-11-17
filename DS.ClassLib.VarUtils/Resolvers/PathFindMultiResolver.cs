using Castle.Core.Internal;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Resolvers.ResolveTasks;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    /// <summary>
    /// An object to resove tasks with set of resolvers.
    /// </summary>
    public class PathFindMultiResolver : PathFindMultiResolverBase, IMultiResolver<PathFindVertexTask>
    {

        /// <summary>
        /// Instansiate an object to create tasks with <paramref name="taskCreator"/> 
        /// and resolve them with <paramref name="taskResolvers"/>.
        /// </summary>
        /// <param name="taskCreator"></param>
        /// <param name="taskResolvers"></param>
        public PathFindMultiResolver(IEnumerator<PathFindVertexTask> taskCreator, 
            IEnumerable<ITaskResolver<PathFindVertexTask>> taskResolvers)
        {
            TaskCreator = taskCreator;
            TaskResolvers = taskResolvers;
        }

        /// <inheritdoc/>
        public ISolution TryResolve()
         => Resolve(false).Result;
        

        /// <inheritdoc/>
        public async Task<ISolution> TryResolveAsync() =>
           await Resolve(true);
    }
}
