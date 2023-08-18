using DS.PathSearch;
using DS.PathSearch.GridMap;


namespace HPA
{
    struct AbstractNode
    {
        public Location LocationL0 { get; set; }
        public Location LocationL1 { get; set; }
        public int Id { get; set; }

        public static AbstractNode GetNodeById(int nodeId, Cluster cluster)
        {
            foreach (AbstractNode node in cluster.AbstractNodes)
            {
                if (nodeId == node.Id)
                    return node;
            }
            return new AbstractNode();
        }

        public static int GetNodeIdByLocation(Location L1, Cluster cluster)
        {
            foreach (AbstractNode node in cluster.AbstractNodes)
            {
                if (node.LocationL1.X == L1.X &&
                    node.LocationL1.Y == L1.Y &&
                    node.LocationL1.Z == L1.Z)
                    return node.Id;
            }
            return 0;
        }
    }
}
