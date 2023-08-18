using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Collisons;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.GridMap.d2;
using DS.ClassLib.VarUtils.Points;
using DS.PathSearch;
using DS.PathSearch.GridMap;
using FrancoGustavo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using IMap = DS.ClassLib.VarUtils.GridMap.IMap;
using DS.PathSearch.PathRequiment;
using DS.ClassLib.VarUtils.Directions;

namespace PathFindTest
{
    internal static class FrancoGustavoFloatTest
    {
        private static Vector3D _stepVector;
        private static IntMap _map;
        private static Point3D _startPoint;
        private static Point3D _endPoint;
        private static List<Point3D> _upassiblePoints;

        public static void Run()
        {
            var minPoint = new Point3D(0, 0, 0);
            var maxPoint = new Point3D(9, 9, 0);
            _stepVector = new Vector3D(1, 1, 1);

            _map = new IntMap(minPoint, maxPoint, _stepVector);
            Console.WriteLine("Map size is: " + _map.Matrix.Length + "\n");

            Console.WriteLine("Original map:");
            _map.Show();

            _upassiblePoints = new List<Point3D>() 
            { 
                new Point3D(5, 0, 0),
                new Point3D(5, 1, 0),
                new Point3D(5, 8, 0),
                new Point3D(5, 9, 0),

            };
            _map.Fill(_startPoint, _endPoint, new List<Point3D>(), _upassiblePoints);

            var path = GetPath(_map, maxPoint, minPoint);
            ShowPath(path);
        }

        private static List<PointPathFinderNode> GetPath(IMap map, Point3D upperBound, Point3D minPoint)
        {
            //var minDistPointByte = Convert.ToByte(minDistPoint);
            var requirement = new DoubleRequirement();

            var uCS1BasePoint = minPoint;
            var uCS2BasePoint = new Point3D(0, 0, 0);
            var pointConverter = new VectorPointConverter(uCS1BasePoint, uCS2BasePoint, _stepVector);

            _startPoint = new Point3D(0, 0, 0);
            _endPoint = new Point3D(9, 9, 0);

            var collisionDetector = new PointCollisionDetector(map);

            var stepVector = new Vector3D(1, 1, 1);

            var angles = new List<int>();
            var main = new Vector3D(0, 1, 0);
            var normal = new Vector3D(1, 0, 0);
            var crossProduct = Vector3D.CrossProduct(main, normal);

            var basis = new OrthoNormBasis(main, normal, crossProduct);

            var stepBasis = new OrthoBasis(
                Vector3D.Multiply(basis.X, 1),
                Vector3D.Multiply(basis.Y, 1),
                Vector3D.Multiply(basis.Z, 1)
                );

            IDirectionFactory directionFactory = new UserDirectionFactory();
            directionFactory.Build(basis, angles);

            List<PointPathFinderNode> pathNodes = FGAlgorythm.GetFloatPathByMap(upperBound, uCS2BasePoint,
                _startPoint, _endPoint, requirement, collisionDetector, directionFactory, stepBasis);

            return pathNodes;
        }

        private static void ShowPath(List<PointPathFinderNode> path)
        {
            if (path == null)
                Debug.WriteLine("No available path exist!");
            else
            {
                var pathPoints = Convert(path);
                _map.Fill(_startPoint, _endPoint, pathPoints, _upassiblePoints);

                Console.WriteLine("\nPath map:");
                _map.Show();
            }
        }

        public static List<Point3D> Convert(List<PointPathFinderNode> path)
        {
            List<Point3D> points = new List<Point3D>();

            for (int i = 1; i < path.Count; i++)
            {
                var currentNode = path[i];
                var currentPoint = currentNode.Point;
                
                    points.Add(currentPoint);
            }

            return points;
        }
    }
}
