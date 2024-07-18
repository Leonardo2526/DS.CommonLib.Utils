using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public struct Node
    {
        public Node(Point3d location)
        {
            Location = location;
        }

        public Point3d Location { get; }

        /// <summary>
        /// Full distance.
        /// </summary>
        public readonly double F => G + H;

        /// <summary>
        /// Distance to start node.
        /// </summary>
        public double G { get; set; }

        /// <summary>
        /// Distance to end node.
        /// </summary>
        public double H { get; set; }


        /// <summary>
        /// Path from start node.
        /// </summary>
        public int Path { get; set; }
    }
}
