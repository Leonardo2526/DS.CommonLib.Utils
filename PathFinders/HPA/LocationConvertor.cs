
using DS.PathSearch;

namespace HPA
{
    struct LocationConvertor
    {
        public  static Location L1ToL0(Location l1, Cluster cluster)
        {
            Location l0 = new Location(l1.X + (cluster.Location.X * AbstractGraph.ClusterSize),
                   l1.Y + (cluster.Location.Y * AbstractGraph.ClusterSize),
                   l1.Z + (cluster.Location.Z * AbstractGraph.ClusterSize));

            return l0;
        }

        public static Location L0ToL1(Location l1, Cluster cluster)
        {
            Location l0 = new Location(l1.X - (cluster.Location.X * AbstractGraph.ClusterSize),
                   l1.Y - (cluster.Location.Y * AbstractGraph.ClusterSize),
                   l1.Z - (cluster.Location.Z * AbstractGraph.ClusterSize));

            return l0;
        }
    }
}
