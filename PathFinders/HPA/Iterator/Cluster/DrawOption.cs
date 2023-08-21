using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    class DrawOption : IClustersIterateOption
    {
        public IMatrixDrawer gridDrawer;

        public DrawOption(IMatrixDrawer gridDrawer)
        {
            this.gridDrawer = gridDrawer;
        } 

        public void Set(int x, int y, int z)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"({x}, {y}, {z})");

            gridDrawer = new MatrixDrawer(AbstractGraph.Clusters[x, y, z].Matrix);
            gridDrawer.Draw();

            foreach (AbstractNode node in AbstractGraph.Clusters[x, y, z].AbstractNodes)
            {
                Console.WriteLine($"\nNode {node.Id}: ({node.LocationL0.X}, {node.LocationL0.Y}, {node.LocationL0.Z})");
                NeighborsPathPrint(node.Id, AbstractGraph.Clusters[x, y, z]);
            }


        }

        void NeighborsPathPrint(int nodeId, Cluster cluster)
        {
            Console.WriteLine("Neighbors:");
            for (int id = 0; id <= AbstractGraph.WeightMatrix.GetUpperBound(0); id++)
            {
                int length = AbstractGraph.WeightMatrix[nodeId, id];
                if (length != 0)
                {
                    AbstractNode node = AbstractNode.GetNodeById(id, cluster);
                    Console.WriteLine($"Node {id}: ({node.LocationL0.X}, {node.LocationL0.Y}, {node.LocationL0.Z}) = {length}");
                }
            };
        }

     
    }
}
