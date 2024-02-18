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
        public Node(  Point3d location)
        {
            Location = location;
        }

        public Point3d Location { get; }

        /// <summary>
        /// Path priority.
        /// </summary>
        public double F { get; set; }

        /// <summary>
        /// Distance to parent node.
        /// </summary>
        public double G { get; set; }

        /// <summary>
        /// Distance to end point.
        /// </summary>
        public double H { get; set; }
    }
}
