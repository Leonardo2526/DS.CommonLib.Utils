namespace HPA
{
    class NodeToGraph : IClustersIterateOption
    {
        public void Set(int x, int y, int z)
        {
            AbstractGraph.Nodes.AddRange(AbstractGraph.Clusters[x, y, z].AbstractNodes);
        }


    }
}
