using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Enumerables;
using DS.PathFinder.Algorithms.InterLine;
using Rhino.Geometry;

namespace DS.PathFinder.UnitTests
{

    [TestFixture]
    public class InterLinePathFindTests
    {
        private const double _minLinkLength = 1;
        private static readonly DirectionIteratorBuilder _iteratorBuilder = new();
        private static readonly PathValidator _pathValidator = new PathValidator();

        [SetUp]
        public void Setup()
        {
        }


        #region 90Degrees

        [Test]
        [Category("90 degrees")]
        public void FindPath_9001_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9002_PathIsNull()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2, 2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.ShouldFailNotNull(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9003_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(0, 1, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
               Config(d1, d2, a, _minLinkLength, 3).
               ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9004_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(0, 1, 0);
            var p2 = new Point3d(1, 0, 1);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
               Config(d1, d2, a, _minLinkLength, 3).
               ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9005_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(0, 1, 0);
            var p2 = new Point3d(0, 1, 1);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
               Config(d1, d2, a, _minLinkLength, 3).
               ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9006_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(1.23456134, -53.23945873453, 123.124908889);
            var d1 = new Vector3d(0, 1, 0);
            var p2 = new Point3d(p1.X + 10, p1.Y + 15, p1.Z);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
               Config(d1, d2, a, _minLinkLength, 3).
               ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9007_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(2, 0, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 2).
                ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9008_ShouldFail()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(2, 0, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldFailCount(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9009_ShouldFail()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, 45, _minLinkLength).
                ShouldFailAngles(path);
        }


        [Test]
        [Category("90 degrees")]
        public void FindPath_9010_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(-1, 0, 0);
            var p2 = new Point3d(-1, -1, 0);
            var d2 = new Vector3d(-1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }

        [Test]
        [Category("90 degrees")]
        public void FindPath_9011_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(-1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }



        [Test]
        [Category("90 degrees")]
        public void FindPath_9012_ShouldPass()
        {
            var a = 90;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(0, 0, 1);
            var p2 = new Point3d(10, 0, -5);
            var d2 = new Vector3d(0, 1, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }

        #endregion


        #region 45Degrees

        [Test]
        [Category("45 degrees")]
        public void FindPath_4501_ShouldPass()
        {
            var a = 45;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 2).
                ShouldPass(path);
        }

        [Test]
        [Category("45 degrees")]
        public void FindPath_4502_ShouldFail()
        {
            var a = 45;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(-1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.ShouldFailNotNull(path);
        }

        [Test]
        [Category("45 degrees")]
        public void FindPath_4503_ShouldPass()
        {
            var a = 45;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 1, 0);
            var p2 = new Point3d(10, -5, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }

        [Test]
        [Category("45 degrees")]
        public void FindPath_4504_ShouldFail()
        {
            var a = 45;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 1, 0);
            var p2 = new Point3d(10, -5, 0);
            var d2 = new Vector3d(0, 1, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.ShouldFailNotNull(path);
        }


        #endregion


        #region 30Degrees


        [Test]
        [Category("30 degrees")]
        public void FindPath_3001_ShouldFail()
        {
            var a = 30;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(1, 1, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.ShouldFailNotNull(path);
        }

        [Test]
        [Category("30 degrees")]
        public void FindPath_3002_ShouldPass()
        {
            var a = 30;

            var p1 = new Point3d(0, 0, 0);
            var d1 = new Vector3d(1, 0, 0);
            var p2 = new Point3d(10, 5, 0);
            var d2 = new Vector3d(1, 0, 0);

            var pathFinder = GetPathFinder(a, d1, d2);
            var path = pathFinder.FindPath(p1, p2);

            _pathValidator.
                Config(d1, d2, a, _minLinkLength, 3).
                ShouldPass(path);
        }

        #endregion

        private static IPathFinder<Point3d, Point3d> GetPathFinder(int angle,
            Vector3d startDirection, Vector3d endDirection,
            double minLinkLenth = _minLinkLength)
        {
            var intersectionFactory = new LineIntersectionFactory(new List<int>() { angle }, _iteratorBuilder);
            var algorithm = new InterLineAlgorithm(intersectionFactory)
            {
                MinLinkLength = minLinkLenth,
                StartDirection = startDirection,
                EndDirection = endDirection
            };
            return new PathFinderFactory<Point3d, Point3d>(algorithm);
        }

    }
}