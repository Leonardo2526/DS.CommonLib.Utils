using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers.ResolveTasks
{
    public class PathFindTask<T> : IResolveTask
    {
        public PathFindTask(T source, T target)
        {
            Source = source;
            Target = target;
        }

        public T Source { get; set; }
        public T Target { get; set; }
    }
}
