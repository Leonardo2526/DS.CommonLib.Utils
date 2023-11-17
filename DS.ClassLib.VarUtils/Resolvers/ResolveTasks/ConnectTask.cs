using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers.ResolveTasks
{
    public class ConnectTask<T> : IResolveTask
    {
        public T Item1 { get; set; }
        public T Item2 { get; set; }
    }
}
