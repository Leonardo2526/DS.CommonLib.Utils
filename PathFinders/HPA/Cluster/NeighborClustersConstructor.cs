using DS.PathSearch;
using DS.PathSearch.GridMap;
using System.Collections.Generic;

namespace HPA
{
    class NeighborClustersConstructor
    {
        Cluster[,,] ClustersL1;

        private sbyte[,]  direction = 
            new sbyte[6, 3] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 1, 0 }, { -1, 0, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };

        public NeighborClustersConstructor(Cluster[,,] clustersL1)
        {
            ClustersL1 = clustersL1;
        }

        public List<Cluster> GetNeighbors(Cluster parentCluster)
        {
            List<Cluster> neighbors = new List<Cluster>();
            Location PCL = parentCluster.Location;

            for (int i = 0; i < 4; i++)
            {
                Location neighborLocation = 
                    new Location(PCL.X + direction[i, 0], PCL.Y + direction[i, 1], PCL.Z + direction[i, 2]);

                Cluster neighborCluster = GetClusterByLocation(neighborLocation);
                if (neighborCluster != null)
                    neighbors.Add(neighborCluster);
            }
                return neighbors;
        }

        private Cluster GetClusterByLocation(Location location)
        {
            foreach (Cluster item in ClustersL1)
            {
                if (item.Location.X == location.X &&
                    item.Location.Y == location.Y &&
                    item.Location.Z == location.Z)
                    return item;
            }

            return null;
        }
    }
}
