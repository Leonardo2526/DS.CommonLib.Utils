using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    class Node
    {
        public static Cluster GetCluster(Location nodeLocation)
        {
            int x = (int)Math.Floor((double)nodeLocation.X/AbstractGraph.ClusterSize);
            int y = (int)Math.Floor((double)nodeLocation.Y/AbstractGraph.ClusterSize);
            int z = (int)Math.Floor((double)nodeLocation.Z/AbstractGraph.ClusterSize);

            Location locationL1 = new Location(x, y, z);

            foreach (Cluster cluster in AbstractGraph.Clusters)
            {
                if (locationL1.Equals(cluster.Location))
                    return cluster;
            }

            return null;
        }
    }
}
