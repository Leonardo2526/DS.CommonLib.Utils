using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.VarUtils.Test
{
    internal class LineBoolSubstractTest
    {
        private static double _tolerance = 0.001;

        private static Line _line1 = new Line(new Point3d(0, 0, 0), new Point3d(5, 0, 0));

        private static readonly object[] _line2PassCases1 =
       {
            new Point3d[2] { new Point3d(-2, 0, 0), new Point3d(3, 0, 0) },
            new Point3d[2] { new Point3d(0, 0, 0), new Point3d(3, 0, 0) },
            new Point3d[2] { new Point3d(1, 0, 0), new Point3d(7, 0, 0) },
        };

        private static readonly object[] _line2PassCases2 =
        {
            new Point3d[2] { new Point3d(1, 0, 0), new Point3d(4, 0, 0) },
            new Point3d[2] { new Point3d(3, 0, 0), new Point3d(2, 0, 0) },
        };

        private static readonly object[] _line2MiddleIntersectpPassCases =
       {
            new Point3d[2] { new Point3d(3, 5, 0), new Point3d(3, -5, 0) },
            new Point3d[2] { new Point3d(0, 5, 0), new Point3d(0, -5, 0) }
        };

        private static readonly object[] _line2NoIntersectPassCases =
    {
            new Point3d[2] { new Point3d(-5, 0, 0), new Point3d(-2, 0, 0) },
            new Point3d[2] { new Point3d(10, 10, 10), new Point3d(10, 10, 20) },
        };

        private static readonly object[] _line2IntersectOnOnePointPassCases =
      {
           new Point3d[2] { new Point3d(5, 0, 0), new Point3d(6, 0, 0) },
           new Point3d[2] { new Point3d(0, 0, 0), new Point3d(0, 5, 0) },
        };

        private static readonly object[] _line2MoreOrEqualToLine1FailCases =
    {
           new Point3d[2] { new Point3d(0, 0, 0), new Point3d(5, 0, 0) },
           new Point3d[2] { new Point3d(0, 0, 0), new Point3d(10, 0, 0) },
           new Point3d[2] { new Point3d(-5, 0, 0), new Point3d(10, 0, 0) },
           new Point3d[2] { new Point3d(-10, 0, 0), new Point3d(5, 0, 0) },
        };

        [SetUp]
        public void Setup()
        { }

        [TestCaseSource(nameof(_line2PassCases1))]
        public void TestSubstractOneLine_001_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);

            Assert.That(() => intersectionLines.Count, Is.EqualTo(1));
            intersectionLines.ForEach(l =>
            {
                var p1 = l.From;
                var p2 = l.To;
                Assert.That(l.Length, Is.GreaterThan(_tolerance));
                Assert.Multiple(() =>
                {
                    Assert.That(_line1.Contains(p1, _tolerance));
                    Assert.That(_line1.Contains(p2, _tolerance));
                    Assert.That(!line2.Contains(p1, _tolerance, false));
                    Assert.That(!line2.Contains(p2, _tolerance, false));
                });
            });

        }

        [TestCaseSource(nameof(_line2PassCases2))]
        public void TestSubstractTwoLines_002_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);

            Assert.That(() => intersectionLines.Count, Is.EqualTo(2));
            intersectionLines.ForEach(l =>
            {
                var p1 = l.From;
                var p2 = l.To;
                Assert.That(l.Length, Is.GreaterThan(_tolerance));
                Assert.Multiple(() =>
                {
                    Assert.That(_line1.Contains(p1, _tolerance));
                    Assert.That(_line1.Contains(p2, _tolerance));
                    Assert.That(!line2.Contains(p1, _tolerance, false));
                    Assert.That(!line2.Contains(p2, _tolerance, false));
                });
            });

        }

        [TestCaseSource(nameof(_line2MiddleIntersectpPassCases))]
        public void TestSubstract_003_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);
            Assert.Multiple(() =>
            {
                Assert.That(() => intersectionLines.Count, Is.EqualTo(1));
                Assert.That(intersectionLines[0].Length, Is.EqualTo(_line1.Length));
                Assert.That(intersectionLines[0].From == _line1.From || intersectionLines[0].From == _line1.To);
            });
        }

        [TestCaseSource(nameof(_line2NoIntersectPassCases))]
        public void TestSubstract_004_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);
            Assert.Multiple(() =>
            {
                Assert.That(() => intersectionLines.Count, Is.EqualTo(1));
                Assert.That(intersectionLines[0].Length, Is.EqualTo(_line1.Length));
                Assert.That(intersectionLines[0].From == _line1.From || intersectionLines[0].From == _line1.To);
            });
        }

        [TestCaseSource(nameof(_line2IntersectOnOnePointPassCases))]
        public void TestSubstract_005_ShouldPass(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);
            Assert.Multiple(() =>
            {
                Assert.That(() => intersectionLines.Count, Is.EqualTo(1));
            });
        }

        [TestCaseSource(nameof(_line2MoreOrEqualToLine1FailCases))]
        public void TestSubstract_006_ShouldFail(Point3d[] points)
        {
            var line2 = new Line(points[0], points[1]);
            var intersectionLines = LineBooleanTools.Substract(_line1, line2, _tolerance);
            Assert.That(() => intersectionLines.Count, Is.EqualTo(0));
        }

    }
}
