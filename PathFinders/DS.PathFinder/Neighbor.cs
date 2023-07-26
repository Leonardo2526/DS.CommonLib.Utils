using DS.PathSearch;
using DS.PathSearch.GridMap;
using System.Collections.Generic;
using System.Linq;

namespace FrancoGustavo
{
    class Neighbor
    {
        int nodeId;
        IGraph graph;

        public Neighbor(int nodeId, IGraph graph)
        {
            this.nodeId = nodeId;
            this.graph = graph;
        }

        public List<Node> GetNeighbors()
        {
            List<Node>  neighbors = new List<Node>();
            for (int id = 0; id <= graph.WeightMatrix.GetUpperBound(0); id++)
            {
                int length = graph.WeightMatrix[nodeId, id];
                if (length != 0)
                {
                    Location location = GetLocation(id);
                    Node node = new Node(id, length, location);

                    neighbors.Add(node);
                }
            };
            neighbors = neighbors.OrderBy(o => o.LengthToBase).ToList();
            return neighbors;
        }

        private Location GetLocation(int id)
        {
            Location location = new Location();

            for (int z = 0; z <= graph.Matrix.GetUpperBound(2); z++)
            {
                for (int y = 0; y <= graph.Matrix.GetUpperBound(1); y++)
                {
                    for (int x = 0; x <= graph.Matrix.GetUpperBound(0); x++)
                    {

                        if (graph.Matrix[x, y, z] == id)
                            return new Location(x, y, z);
                    }
                }
            }

            return location;
        }

        private int GetId(int x, int y, int z)
        {
            return graph.Matrix[x,y,z];
        }
    }
}
