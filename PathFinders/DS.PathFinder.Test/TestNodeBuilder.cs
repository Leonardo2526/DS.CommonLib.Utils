using DS.PathFinder.Algorithms.AStar;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.PathFindTest
{
    internal class TestNodeBuilder : INodeBuilder
    {
        public int Heuristic { get; set; }
        public double Step { get; set; }

        public PathNode Node => throw new NotImplementedException();

        public PathNode Build(PathNode parentNode, Vector3d nodeDir)
        {
            throw new NotImplementedException();
        }

        public PathNode BuildWithParameters()
        {
            throw new NotImplementedException();
        }
    }
}
