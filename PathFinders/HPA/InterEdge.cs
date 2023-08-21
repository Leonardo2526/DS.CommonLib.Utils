using DS.PathSearch;
using DS.PathSearch.GridMap;
using System.Collections.Generic;

namespace HPA
{
    class InterEdge
    {
        readonly Cluster[,,] ClustersL1;
        readonly Cluster Parent;

        public InterEdge(Cluster[,,] clustersL1, Cluster parent)
        {
            ClustersL1 = clustersL1;
            Parent = parent;
        }

        List<Location> PEntrancesNodes = new List<Location>();
        List<Location> NEntrancesNodes = new List<Location>();
        public static List<ClusterEdge> VisitedClusterEdges { get; set; } = new List<ClusterEdge>();

        public void Get()
        {
            NeighborClustersConstructor neighborClusters = new NeighborClustersConstructor(ClustersL1);
            List<Cluster> neighbors = neighborClusters.GetNeighbors(Parent);

            EdgesPairsBuilder edgesPairsBuilder = new EdgesPairsBuilder(Parent, neighbors);
            List<EdgesPair> edgesPairs = edgesPairsBuilder.Build();

            foreach (EdgesPair edgesPair in edgesPairs)
            {
                if (VisitedClusterEdges.Contains(edgesPair.NeighborClusterEdge))
                    continue;

                int edgeLength = edgesPair.ParentClusterEdge.NodesLocations.Count;
                PEntrancesNodes = new List<Location>();
                NEntrancesNodes = new List<Location>();

                ParsePair(edgesPair, edgeLength);
            }

        }

        private void RunWriter(EdgesPair edgesPair)
        {
            AbstractNodeWriter nodeWtiter = 
                new AbstractNodeWriter(edgesPair.Parent, edgesPair.Neighbor, PEntrancesNodes, NEntrancesNodes);
            //nodeWtiter.WriteByCenter();
            nodeWtiter.WriteByEdges();

            VisitedClusterEdges.Add(edgesPair.ParentClusterEdge);
            VisitedClusterEdges.Add(edgesPair.NeighborClusterEdge);
        }

        private void ParsePair(EdgesPair edgesPair, int edgeLength)
        {
            //Get nodes from each edge's pair
            for (int i = 0; i < edgeLength; i++)
            {
                Location Pnode = edgesPair.ParentClusterEdge.NodesLocations[i];
                Location Nnode = edgesPair.NeighborClusterEdge.NodesLocations[i];

                //Check obstacles
                if (edgesPair.Parent.Matrix[Pnode.X, Pnode.Y, Pnode.Z] != 1 &&
                    edgesPair.Neighbor.Matrix[Nnode.X, Nnode.Y, Nnode.Z] != 1)
                {
                    PEntrancesNodes.Add(Pnode);
                    NEntrancesNodes.Add(Nnode);
                }
                else if (PEntrancesNodes.Count == 0)
                    continue;
                else //reach obstacle
                {
                    RunWriter(edgesPair);

                    PEntrancesNodes = new List<Location>();
                    NEntrancesNodes = new List<Location>();
                }
            }

            if (PEntrancesNodes.Count > 0)
                RunWriter(edgesPair);
        }

       
    }

}

