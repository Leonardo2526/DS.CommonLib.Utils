using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    struct EdgesPair
    {
        public EdgesPair(Cluster parent, Cluster neighbor, ClusterEdge parentClusterEdge, ClusterEdge neighborClusterEdge)
        {
            Parent = parent;
            Neighbor = neighbor;
            ParentClusterEdge = parentClusterEdge;
            NeighborClusterEdge = neighborClusterEdge;
        }

        public Cluster Parent { get; set; }
        public Cluster Neighbor { get; set; }
        public ClusterEdge ParentClusterEdge { get; set; }
        public ClusterEdge NeighborClusterEdge { get; set; }
    }
}
