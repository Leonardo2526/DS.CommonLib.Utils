
using DS.PathSearch;
using DS.PathSearch.GridMap;

namespace HPA
{
    class StartGoalAdd
    {
        private readonly IMap Map;

        private Cluster cluster;
        private AbstractNode node;

        public StartGoalAdd(IMap map)
        {
            Map = map;

            AddToCluster(Map.Start);
            AbstractGraph.StartNode = node;
            AbstractGraph.StartNodeCluster = cluster;

            AddToCluster(Map.Goal);
            AbstractGraph.GoalNode = node;
            AbstractGraph.GoalNodeCluster = cluster;

        }
        void AddToCluster(Location L0)
        {
            cluster = Node.GetCluster(L0);
            Location L1 = LocationConvertor.L0ToL1(L0, cluster);

            node = new AbstractNode()
            {
                Id = AbstractGraph.NodesCount+ 1,
                LocationL0 = L0,
                LocationL1 = L1
            };
            AbstractGraph.NodesCount += 1;  
            cluster.AbstractNodes.Add(node);
        }
    }
}
