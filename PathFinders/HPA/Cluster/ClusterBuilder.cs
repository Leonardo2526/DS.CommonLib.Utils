using DS.PathSearch;
using DS.PathSearch.GridMap;
using System;
using System.Collections.Generic;

namespace HPA
{
    class ClusterBuilder
    {
        public IMap map1;

        public ClusterBuilder(IMap map1)
        {
            this.map1 = map1;
        }

        public Cluster[,,] ClustersL1 { get; set; }

        private List<Location> Obstacles;

        public Cluster[,,] GetClusters()
        {
            
            int CountX = (int)Math.Round((double)(map1.Matrix.GetUpperBound(0) + 1)/ AbstractGraph.ClusterSize);
            int CountY = (int)Math.Round((double)(map1.Matrix.GetUpperBound(1) + 1) / AbstractGraph.ClusterSize);
            int CountZ = (int)Math.Ceiling((double)(map1.Matrix.GetUpperBound(2) + 1) / AbstractGraph.ClusterSize);

            ClustersL1 = new Cluster[CountX, CountY, CountZ];

            int xSmesh, ySmesh, zSmesh;

            for (int z = 0; z < CountZ; z++)
            {
                zSmesh = z * AbstractGraph.ClusterSize;
                for (int y = 0; y < CountY; y++)
                {
                    ySmesh = y * AbstractGraph.ClusterSize;
                    for (int x = 0; x < CountX; x++)
                    {
                        this.Obstacles = new List<Location>();

                        xSmesh = x * AbstractGraph.ClusterSize;
                        Cluster newCluster = Build(xSmesh, ySmesh, zSmesh);
                        newCluster.Location = new Location(x, y, z);
                        newCluster.Obstacles = this.Obstacles;

                        ClustersL1[x, y, z] = newCluster;
                    }

                }
            }

            return ClustersL1;

        }

        private Cluster Build(int xSmesh, int YSmesh, int ZSmesh)
        {
            int[,,] ClusterMatrix = new int[AbstractGraph.ClusterSize, AbstractGraph.ClusterSize, 1];
            for (int z = 0; z <= 0; z++)
            {
                for (int y = 0; y < AbstractGraph.ClusterSize; y++)
                {
                    for (int x = 0; x < AbstractGraph.ClusterSize; x++)
                    {
                        ClusterMatrix[x, y, z] = map1.Matrix[x + xSmesh, y + YSmesh, z + ZSmesh];

                        if (ClusterMatrix[x, y, z] == 1)
                        {
                            Location location = new Location(x,y,z);
                            Obstacles.Add(location);
                        }
                    }

                }
            }

            Cluster newCluster = new Cluster(ClusterMatrix);
            return newCluster;
        }
    }
}
