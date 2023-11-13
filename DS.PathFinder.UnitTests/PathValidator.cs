using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Graphs;
using DS.ClassLib.VarUtils.GridMap;
using Rhino.Geometry;
using IGraph = DS.ClassLib.VarUtils.Graphs.IGraph;

namespace DS.PathFinder.UnitTests
{
    internal class PathValidator : IPathValidator
    {
        private Vector3d _startDirection;
        private Vector3d _endDirection;
        private int _angle;
        private double _minCurveLength;
        private int _length;

        public PathValidator Config(Vector3d startDirection, Vector3d endDirection, int angle,
            double minCurveLength = double.MaxValue, int length = 0)
        {
            _startDirection = startDirection;
            _endDirection = endDirection;
            _angle = angle;
            _minCurveLength = minCurveLength;
            _length = length;

            return this;
        }

        public void ShouldPass(List<Point3d> path)
        {
            IGraph graph = new SimpleGraph(path);
            Assert.Multiple(() =>
            {
                Assert.That(path, Is.Not.Null);
                Assert.That(HasValidAngles(graph, _startDirection, _endDirection, _angle));
                Assert.That(graph.Edges.Any(l => l.Length < _minCurveLength), Is.False);
                if (_length != 0) { Assert.That(graph.Vertices, Has.Count.EqualTo(_length)); }
            });
            Assert.Pass("Path points: \n" + graph.ToString());
        }

        public void ShouldFailNotNull(List<Point3d> path) => Assert.That(path, Is.Null);

        public void ShouldFailAngles(List<Point3d> path)
        {
            IGraph graph = new SimpleGraph(path);
            Assert.That(!HasValidAngles(graph, _startDirection, _endDirection, _angle));
        }

        public void ShouldFailCount(List<Point3d> path)
        {
            IGraph graph = new SimpleGraph(path);
            Assert.That(graph.Vertices, Has.Count.Not.EqualTo(_length));
        }

        private static bool HasValidAngles(IGraph graph, Vector3d startDirection, Vector3d endDirection, int angle)
        {
            var valid = true;


            var checkPath = new List<Point3d>();
            if (startDirection != default)
            {
                var startAxiliaryPoint = graph.Vertices.First() - startDirection;
                checkPath.Add(startAxiliaryPoint);
            }
            checkPath.AddRange(graph.Vertices);

            if (endDirection != default)
            {
                var endAxiliaryPoint = graph.Vertices.Last() + endDirection;
                checkPath.Add(endAxiliaryPoint);
            }

            for (int i = 0; i < checkPath.Count - 2; i++)
            {
                var p1 = checkPath[i];
                var p2 = checkPath[i + 1];
                var p3 = checkPath[i + 2];
                var dir1 = p2 - p1;
                var dir2 = p3 - p2;

                //check angle between lines
                var a1 = (int)Vector3d.VectorAngle(dir1, dir2).RadToDeg();
                if (a1 == 0 || a1 == angle)
                { continue; }
                else { valid = false; break; }
            }

            return valid;

        }

        private static void PrintLength(List<Point3d> path) => Assert.Pass("path length is: " + path.Count.ToString());
    }
}
