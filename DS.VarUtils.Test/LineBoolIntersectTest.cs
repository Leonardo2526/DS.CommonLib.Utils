using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.VarUtils.Test
{
    internal class LineBoolIntersectTest
    {
        private static double _tolerance = 0.001;

        private static Line _line1 = new Line(new Point3d(0, 0, 0), new Point3d(5, 0, 0));


        private static readonly object[] _line2PassCases =
        {
            new Point3d[2] { new Point3d(-2, 0, 0), new Point3d(3, 0, 0) },
            new Point3d[2] { new Point3d(2, 0, 0), new Point3d(7, 0, 0) },
            new Point3d[2] { new Point3d(0, 0, 0), new Point3d(5, 0, 0) },
            new Point3d[2] { new Point3d(0, 0, 0), new Point3d(3, 0, 0) },
            new Point3d[2] { new Point3d(-2, 0, 0), new Point3d(5, 0, 0) },
            new Point3d[2] { new Point3d(1, 0, 0), new Point3d(4, 0, 0) },
            new Point3d[2] { new Point3d(-5, 0, 0), new Point3d(10, 0, 0) },
        };

        private static readonly object[] _line2MiddleIntersectFailCases =
       {
            new Point3d[2] { new Point3d(3, 5, 0), new Point3d(3, -5, 0) },
            new Point3d[2] { new Point3d(0, 5, 0), new Point3d(0, -5, 0) }
        };

        private static readonly object[] _line2NoIntersectFailCases =
    {
            new Point3d[2] { new Point3d(-5, 0, 0), new Point3d(-2, 0, 0) },
            new Point3d[2] { new Point3d(10, 10, 10), new Point3d(10, 10, 20) },
        };

        private static readonly object[] _line2IntersectOnOnePointFailCases =
      {
           new Point3d[2] { new Point3d(5, 0, 0), new Point3d(6, 0, 0) },
           new Point3d[2] { new Point3d(0, 0, 0), new Point3d(0, 5, 0) },
        };

        [SetUp]
        public void Setup()
        { }

        [TestCaseSource(nameof(_line2PassCases))]
        public void TestIntersections_001_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLine = LineBoolTools.Intersect(_line1, line2, _tolerance);
            var p1 = intersectionLine.From;
            var p2 = intersectionLine.To;

            Assert.That(intersectionLine.Length, Is.GreaterThan(_tolerance));
            Assert.Multiple(() =>
            {
                Assert.That(_line1.Contains(p1, _tolerance));
                Assert.That(_line1.Contains(p2, _tolerance));
                Assert.That(line2.Contains(p1, _tolerance));
                Assert.That(line2.Contains(p2, _tolerance));
            });
        }

        [TestCaseSource(nameof(_line2MiddleIntersectFailCases))]
        public void TestIntersections_002_ShouldFail(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLine = LineBoolTools.Intersect(_line1, line2, _tolerance);
            var p1 = intersectionLine.From;
            var p2 = intersectionLine.To;

            Assert.That(intersectionLine.Length, Is.LessThan(_tolerance));        
        }

        [TestCaseSource(nameof(_line2NoIntersectFailCases))]
        public void TestIntersections_003_ShouldFail(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLine = LineBoolTools.Intersect(_line1, line2, _tolerance);
            var p1 = intersectionLine.From;
            var p2 = intersectionLine.To;

            Assert.That(intersectionLine.Length, Is.LessThan(_tolerance));           
        }

        [TestCaseSource(nameof(_line2IntersectOnOnePointFailCases))]
        public void TestIntersections_004_ShouldFail(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLine = LineBoolTools.Intersect(_line1, line2, _tolerance);
            var p1 = intersectionLine.From;
            var p2 = intersectionLine.To;

            Assert.That(intersectionLine.Length, Is.LessThan(_tolerance));       
        }

    }
}
