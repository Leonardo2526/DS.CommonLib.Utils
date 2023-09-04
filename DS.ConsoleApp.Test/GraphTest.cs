using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Graphs;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class GraphTest
    {
        public TraceGraph RunNodesTest()
        {
            //var nodes = new List<Point3d>()
            //{
            //    new Point3d(),
            //    new Point3d(1,0,0),
            //    new Point3d(1,1,0),
            //    new Point3d(2,1,0),
            //    new Point3d(2,0,0),
            //    new Point3d(3,0,0),
            //};

            Random rnd = new Random();
            var x1 = rnd.NextDouble();
            var nodes = new List<Point3d>()
            {
                new Point3d(),
                new Point3d(x1,0,0),
                new Point3d(x1,1,0),
                new Point3d(2,1,0),
                new Point3d(2,0,0),
                new Point3d(3,0,0),
            };

            var graph = new TraceGraph(nodes);
            foreach (var link in graph.Links)
            {
                Console.WriteLine(link.ToString());
            }

            return graph;
        }

        public TraceGraph RunLinksTest(List<Line> links)
        {
            var graph = new TraceGraph(links);
            foreach (var node in graph.Nodes)
            {
                Console.WriteLine(node.ToString());
            }
            return graph;
        }
    }
}
