using DS.PathSearch;
using DS.PathSearch.GridMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    class AbstractNodeWriter
    {
        readonly Cluster Parent;
        readonly Cluster Neghbor;
        readonly List<Location> ParentNodesLocations;
        readonly List<Location> NeighborNodesLocations;

        public AbstractNodeWriter(Cluster parent, Cluster neghbor, 
            List<Location> parentNodesLocations, List<Location> neighborNodesLocations)
        {
            this.Parent = parent;
            this.Neghbor = neghbor;
            this.ParentNodesLocations = parentNodesLocations;
            this.NeighborNodesLocations = neighborNodesLocations;
        }


        private Location parentNodeLocation = new Location();
        private Location neighborNodeLocation = new Location();

        public void WriteByCenter()
        {
            if (ParentNodesLocations.Count < 5)
            {
                int AbstractNodesLocation = (int)Math.Ceiling((double)ParentNodesLocations.Count / 2);
                parentNodeLocation = ParentNodesLocations[AbstractNodesLocation - 1];
                neighborNodeLocation = NeighborNodesLocations[AbstractNodesLocation - 1];

                SetPairValues(parentNodeLocation, neighborNodeLocation);

            }
            else
            {
                parentNodeLocation = ParentNodesLocations[0];
                neighborNodeLocation = NeighborNodesLocations[0];
                SetPairValues(parentNodeLocation, neighborNodeLocation);

                parentNodeLocation = ParentNodesLocations[ParentNodesLocations.Count - 1];
                neighborNodeLocation = NeighborNodesLocations[NeighborNodesLocations.Count - 1];
                SetPairValues(parentNodeLocation, neighborNodeLocation);
            }
        }

        public void WriteByEdges()
        {
                parentNodeLocation = ParentNodesLocations[0];
                neighborNodeLocation = NeighborNodesLocations[0];
                SetPairValues(parentNodeLocation, neighborNodeLocation);

                parentNodeLocation = ParentNodesLocations[ParentNodesLocations.Count - 1];
                neighborNodeLocation = NeighborNodesLocations[NeighborNodesLocations.Count - 1];
                SetPairValues(parentNodeLocation, neighborNodeLocation);
        }

        public void SetPairValues(Location parentNodeLocation, Location neighborNodeLocation)
        {
            SetValues(Parent, parentNodeLocation);
            SetValues(Neghbor, neighborNodeLocation);

            int parentId = AbstractNode.GetNodeIdByLocation(parentNodeLocation, Parent);
            int neighborId = AbstractNode.GetNodeIdByLocation(neighborNodeLocation, Neghbor);

            AbstractGraph.WeightMatrix[parentId, neighborId] = 1;
            AbstractGraph.WeightMatrix[neighborId, parentId] = 1;
        }

        private void SetValues(Cluster cluster, Location L1)
        {
            Location L0 = LocationConvertor.L1ToL0(L1, cluster);
            WriteNodeToCluster(cluster, L0, L1);

            //Mark nodes for MapMatrixL1 visualization
            AbstractGraph.MapMatrixL1[L0.X, L0.Y, L0.Z] = 5;

            //Mark nodes for ClusterMatrix visualization
            cluster.Matrix[L1.X, L1.Y, L1.Z] = 5;
        }


        private bool IfNodeExist(AbstractNode abstractNode, List<AbstractNode> nodes)
        {
            foreach (AbstractNode node in nodes)
            {
                if (node.LocationL0.X == abstractNode.LocationL0.X &&
                 node.LocationL0.Y == abstractNode.LocationL0.Y &&
                 node.LocationL0.Z == abstractNode.LocationL0.Z)
                    return true;

            }

            return false;
        }

        private void WriteNodeToCluster(Cluster cluster, Location L0, Location L1)
        {
            AbstractNode abstractNode = new AbstractNode
            {
                Id = AbstractGraph.NodesCount + 1,
                LocationL0 = L0,
                LocationL1 = L1
            };


            //write nodes to its clusters
            if (!IfNodeExist(abstractNode, cluster.AbstractNodes))
            {
                AbstractGraph.NodesCount += 1;
                cluster.AbstractNodes.Add(abstractNode);
            }
        }
    }
}
