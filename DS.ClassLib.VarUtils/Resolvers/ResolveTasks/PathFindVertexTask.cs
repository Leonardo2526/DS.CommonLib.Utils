using DS.GraphUtils.Entities;

namespace DS.ClassLib.VarUtils.Resolvers.ResolveTasks
{
    public struct PathFindVertexTask : IResolveTask
    {
        public PathFindVertexTask(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public IVertex Source { get; set; }
        public IVertex Target { get; set; }
    }
}
