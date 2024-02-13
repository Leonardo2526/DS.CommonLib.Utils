using Rhino.Geometry;
using DS.ClassLib.VarUtils;

namespace DS.VarUtils.Test
{
    public class LineContainsPointTest
    {
        private static readonly object[] _multiplePointCases =
            {
            new double[3] { 0, 0, 0 },
            new double[3] { 1, 0, 0 },
            new double[3] { 3, 0, 0 }
        };

        private static readonly object[] _multiplePointDoubleCases =
           {
            new double[3] { 0.234523, 0, 0 },
            new double[3] { 1.3450987, 0, 0 },
            new double[3] { 4.99999, 0, 0 }
        };

        private static Line _line = new Line(new Point3d(0, 0, 0), new Point3d(5, 0, 0));

        private static double _tolerance = 0.001;

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [TestCase(2, 0, 0)]
        public void TestSinglePoint_001_ShouldPass(double x, double y, double z)
      => Assert.That(IsContains(x, y, z), Is.True);


        [Test]
        [TestCaseSource(nameof(_multiplePointCases))]
        public void TestMultiplePoints_002_ShouldPass(double x, double y, double z)
        => Assert.That(IsContains(x, y, z), Is.True);


        [Test]
        [TestCase(-1, 0, 0)]
        public void TestSinglePoint_003_ShouldFail(double x, double y, double z)
         => Assert.That(IsContains(x, y, z), Is.False);        


        [Test]
        [TestCaseSource(nameof(_multiplePointDoubleCases))]
        public void TestMultiplePoints_004_ShouldPass(double x, double y, double z)
            => Assert.That(IsContains(x, y, z), Is.True);

        [Test]
        [TestCase(0, 2, 0)]
        public void TestSinglePoint_005_ShouldFail(double x, double y, double z)
            => Assert.That(IsContains(x, y, z), Is.False);

        [Test]
        [TestCase(-0.0005, 0, 0)]
        public void TestSinglePointTolerance_006_ShouldPass(double x, double y, double z)
            => Assert.That(IsContains(x, y, z), Is.True);

        [Test]
        [TestCase(-0.01, 0, 0)]
        public void TestSinglePointTolerance_007_ShouldFail(double x, double y, double z)
            => Assert.That(IsContains(x, y, z), Is.False);

        private static bool IsContains(double x, double y, double z)
        {
            var testPoint = new Point3d(x, y, z);
            return _line.Contains(testPoint, _tolerance);
        }

    }
}