using DS.PathSearch;
using DS.PathSearch.GridMap;
using System.Collections.Generic;

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

            Matrix = new int[map.Matrix.GetUpperBound(0) + 1, map.Matrix.GetUpperBound(1) + 1, map.Matrix.GetUpperBound(2) + 1];
            foreach (AbstractNode node in AbstractGraph.Nodes)
                Matrix[node.LocationL0.X, node.LocationL0.Y, node.LocationL0.Z] = node.Id;
        }


    }
}
