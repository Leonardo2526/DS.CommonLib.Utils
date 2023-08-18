using DS.PathSearch;
using System;
using System.Collections.Generic;

namespace HPA
{
    class Cluster
    {
        public Cluster(int[,,] clusterMatrix)
        {
            Matrix = clusterMatrix;

            int z = 0;
            for (int y = 0; y <= Matrix.GetUpperBound(1); y += Matrix.GetUpperBound(1))
            {
                ClusterEdge clusterEdge1 = new ClusterEdge();
                clusterEdge1.NodesLocations = new List<Location>();

                ClusterEdge clusterEdge2 = new ClusterEdge();
                clusterEdge2.NodesLocations = new List<Location>();

                for (int x = 0; x <= Matrix.GetUpperBound(0); x++)
                {
                    Location nodeLocation1 = new Location(x, y, z);
                    Location nodeLocation2 = new Location(y, x, z);

                    clusterEdge1.NodesLocations.Add(nodeLocation1);
                    clusterEdge2.NodesLocations.Add(nodeLocation2);
                }
                ClusterEdges.Add(clusterEdge1);
                ClusterEdges.Add(clusterEdge2);
            }
        }

        public int[,,] Matrix { get; set; }
        public List<Location> Obstacles { get; set; } = new List<Location>();
        public List<ClusterEdge> ClusterEdges { get; } = new List<ClusterEdge>();

        public ClusterEdge GetEdge(ClusterEdge.EdgeSide edgeSide)
        {

            ClusterEdge clusterEdge = edgeSide switch
            {
                ClusterEdge.EdgeSide.Left => ClusterEdges[1],
                ClusterEdge.EdgeSide.Right => ClusterEdges[3],
                ClusterEdge.EdgeSide.Down => ClusterEdges[2],
                ClusterEdge.EdgeSide.Up => ClusterEdges[0],
                _ => ClusterEdges[0]
            };

        return clusterEdge;

        }

        public Location Location { get; set; }

        public List<AbstractNode> AbstractNodes { get; set; } = new List<AbstractNode>();

    }
}
