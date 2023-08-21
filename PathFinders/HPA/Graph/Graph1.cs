using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.System;
using FrancoGustavo;

namespace HPA
{
    class Graph1 : IGraph
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public int[,] WeightMatrix { get; set; }
        public int[,,] Matrix { get; set; }

        /// <summary>
        /// Abstract nodes locations and its ids
        /// </summary>
        public Dictionary<Location, int> Nodes { get; set; }

        public Graph1(IMap map)
        {
            Start = map.Start;
            Goal = map.Goal;
            WeightMatrix = AbstractGraph.WeightMatrix;

            Matrix =  new int[map.MaxMatrixPoint.X, map.MaxMatrixPoint.Y, map.MaxMatrixPoint.Z];
            foreach (AbstractNode node in AbstractGraph.Nodes)
                Matrix[node.LocationL0.X, node.LocationL0.Y, node.LocationL0.Z] = node.Id;
        }


    }
}
