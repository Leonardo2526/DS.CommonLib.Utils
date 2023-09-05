using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Enumerables;
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
        private static readonly double _sin30 = Math.Sin(30.DegToRad());
        private static readonly double _cos30 = Math.Cos(30.DegToRad());


        readonly List<Point3d> _planarNodes90 = new List<Point3d>()
        {
            new Point3d(),
            new Point3d(1,0,0),
            new Point3d(1,1,0),
            new Point3d(2,1,0),
            new Point3d(2,0,0),
            new Point3d(3,0,0),
        };

        readonly List<Point3d> _planarNodes45 = new List<Point3d>()
        {
            new Point3d(),
            new Point3d(1,0,0),
            new Point3d(2,1,0),
            new Point3d(3,1,0),
            new Point3d(4,0,0),
            new Point3d(5,0,0),
        };

        readonly List<Point3d> _planarNodesMinus45 = new List<Point3d>()
        {
            new Point3d(),
            new Point3d(1,0,0),
            new Point3d(2,-1,0),
            new Point3d(3,-1,0),
            new Point3d(4,0,0),
            new Point3d(5,0,0),
        };

        readonly List<Point3d> _planarNodes30 = new List<Point3d>()
        {
            new Point3d(),
            new Point3d(1,0,0),
            new Point3d(1 + _cos30,_sin30,0),
            new Point3d(_cos30 + 2,_sin30,0),
            new Point3d(2 * _cos30 + 2,0,0),
            new Point3d(2 * _cos30 + 3,0,0),
        };

        readonly List<Point3d> _notPlanarNodes_1 = new List<Point3d>()
        {
            new Point3d(),
            new Point3d(1,0,0),
            new Point3d(1,1,0),
            new Point3d(2,1,0),
            new Point3d(2,0,0),
            new Point3d(2,0,2),
        };

        public SimpleGraph CreatePlanarGraph90()=>new SimpleGraph(_planarNodes90);
        public SimpleGraph CreatePlanarGraph45()=>new SimpleGraph(_planarNodes45);
        public SimpleGraph CreatePlanarGraphMinus45()=>new SimpleGraph(_planarNodesMinus45);
        public SimpleGraph CreatePlanarGraph30() => new SimpleGraph(_planarNodes30);


        public SimpleGraph CreateNotPlanarGraph() => new SimpleGraph(_notPlanarNodes_1);

        public SimpleGraph RunNodesTest()
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

            var graph = new SimpleGraph(nodes);
            foreach (var link in graph.Links)
            {
                Console.WriteLine(link.ToString());
            }

            return graph;
        }

        public SimpleGraph RunLinksTest(List<Line> links)
        {
            var graph = new SimpleGraph(links);
            foreach (var node in graph.Nodes)
            {
                Console.WriteLine(node.ToString());
            }
            return graph;
        }

        public SimpleGraph MinimizeNodes(SimpleGraph graph)
        {
            var angles = new List<int>()
            {
              90
            };

            ITraceCollisionDetector<Point3d> collisionDetector = null;

            var nm = new NodesMinimizator(angles, collisionDetector);
            nm.MinLinkLength = 0;
            nm.MaxLinkLength = 5;

            return nm.ReduceNodes(graph);
        }
    }
}
