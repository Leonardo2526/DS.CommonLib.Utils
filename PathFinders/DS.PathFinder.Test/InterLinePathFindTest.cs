using DS.ClassLib.VarUtils.Enumerables;
using DS.ClassLib.VarUtils;
using DS.PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.PathFinder.Algorithms.InterLine;
using Rhino.Geometry;
using DS.ClassLib.VarUtils.Graphs;
using System.IO;

namespace ConsoleApp.PathFindTest
{
    internal class InterLinePathFindTest
    {
        private static double _minLinkLength = 1;
        private readonly DirectionIteratorBuilder _iteratorBuilder;
        private PathFinderFactory<Point3d, Point3d> _pathFinder;
        private InterLineAlgorithm _algorithm;

        public InterLinePathFindTest()
        {
            _iteratorBuilder = new DirectionIteratorBuilder();          

            var path = Run3();

            if(path != null)
            {
                var graph = new SimpleGraph(path);
                Console.WriteLine(graph.ToString());
            }
            else { Console.WriteLine("Failed to find path."); }
        }

        public List<Point3d> Run1()
        {
            var angle = 90;
            BuildAlgorithm(angle);

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            _algorithm.StartDirection = d1;
            _algorithm.EndDirection = d2;

            return _pathFinder.FindPath(p1, p2);
        }


        public List<Point3d> Run2()
        {
            var angle = 15;
            BuildAlgorithm(angle);

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(10, 10, 0);
            var d2 = new Vector3d(1, 0, 0);

            _algorithm.StartDirection = d1;
            _algorithm.EndDirection = d2;

            return _pathFinder.FindPath(p1, p2);
        }

        public List<Point3d> Run3()
        {
            var angle = 30;
            BuildAlgorithm(angle);

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(20, 10, 0);
            var d2 = new Vector3d(1, 0, 0);

            _algorithm.StartDirection = d1;
            _algorithm.EndDirection = d2;

            return _pathFinder.FindPath(p1, p2);
        }



        private void BuildAlgorithm(int angle)
        {
            var intersectionFactory = new LineIntersectionFactory(new List<int>() { angle }, _iteratorBuilder);
            _algorithm = new InterLineAlgorithm(intersectionFactory)
            {
                MinLinkLength = _minLinkLength
            };
            _pathFinder = new PathFinderFactory<Point3d, Point3d>(_algorithm);
        }
    }
}
