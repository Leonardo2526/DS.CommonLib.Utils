using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.VarUtils.Test
{
    internal class LineBoolMultiSubstractTest
    {
        private static int _toleranceDigits = 3;
        private static double _tolerance = Math.Pow(0.1, _toleranceDigits);

        private static Line _line1 = new(new Point3d(0, 0, 0), new Point3d(10, 0, 0));
        private static Line _negativeLine1 = new(new Point3d(-10, 0, 0), new Point3d(-2, 0, 0));


        [SetUp]
        public void Setup()
        { }

        [Test]
        public void TestMultiSubstract_001_ShouldPass()
        {
            List<Line> deductionLines = new()
            {
                new Line(new Point3d(-1, 0, 0), new Point3d(1, 0, 0)),
                new Line(new Point3d(2, 0, 0), new Point3d(4, 0, 0)),
                new Line(new Point3d(8, 0, 0), new Point3d(9, 0, 0)),
            };

            var result = LineBooleanTools.Substract(_line1, deductionLines);

            Assert.That(() => result.Count, Is.EqualTo(3));
            result.ForEach(l =>
            {
                var p1 = l.From;
                var p2 = l.To;
                Assert.Multiple(() =>
                {
                    Assert.That(l.Length, Is.GreaterThan(_tolerance));
                    Assert.That(_line1.Contains(p1, _tolerance));
                    Assert.That(_line1.Contains(p2, _tolerance));

                    Assert.That(deductionLines.TrueForAll(dl => !l.Contains(p1, _tolerance, false)));
                });
            });

        }

        [Test]
        public void TestMultiSubstractOverlap_002_ShouldPass()
        {
            List<Line> deductionLines = new()
            {
                new Line(new Point3d(-1, 0, 0), new Point3d(5, 0, 0)),
                new Line(new Point3d(2, 0, 0), new Point3d(6, 0, 0)),
                new Line(new Point3d(4, 0, 0), new Point3d(9, 0, 0)),
            };

            var result = LineBooleanTools.Substract(_line1, deductionLines);

            Assert.That(() => result.Count, Is.EqualTo(1));
            result.ForEach(l =>
            {
                var p1 = l.From;
                var p2 = l.To;
                Assert.Multiple(() =>
                {
                    Assert.That(l.Length, Is.GreaterThan(_tolerance));
                    Assert.That(_line1.Contains(p1, _tolerance));
                    Assert.That(_line1.Contains(p2, _tolerance));

                    Assert.That(deductionLines.TrueForAll(dl => !l.Contains(p1, _tolerance, false)));
                });
            });

        }

        [Test]
        public void TestMultiSubstract_003_ShouldFail()
        {
            List<Line> deductionLines = new()
            {
                new Line(new Point3d(2, 0, 0), new Point3d(6, 0, 0)),
                new Line(new Point3d(15, 0, 0), new Point3d(0, 0, 0)),
                new Line(new Point3d(4, 0, 0), new Point3d(9, 0, 0)),
            };

            var result = LineBooleanTools.Substract(_line1, deductionLines);

            Assert.That(() => result.Count, Is.EqualTo(0));           
        }

        [Test]
        public void TestMultiSubstract_004_ShouldFail()
        {
            List<Line> deductionLines = new()
            {
                new Line(new Point3d(0, 0, 0), new Point3d(6, 0, 0)),
                new Line(new Point3d(6, 0, 0), new Point3d(10, 0, 0))
            };

            var result = LineBooleanTools.Substract(_line1, deductionLines);

            Assert.That(() => result.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestMultiSubstractNegativeLine_005_ShouldPass()
        {
            List<Line> deductionLines = new()
            {
                new Line(new Point3d(-8, 0, 0), new Point3d(-6, 0, 0)),
                new Line(new Point3d(-3, 0, 0), new Point3d(-4, 0, 0)),
                new Line(new Point3d(-9, 0, 0), new Point3d(-11, 0, 0)),
            };

            var result = LineBooleanTools.Substract(_negativeLine1, deductionLines);

            Assert.That(() => result.Count, Is.EqualTo(3));
            result.ForEach(l =>
            {
                var p1 = l.From;
                var p2 = l.To;
                Assert.Multiple(() =>
                {
                    Assert.That(l.Length, Is.GreaterThan(_tolerance));
                    Assert.That(_negativeLine1.Contains(p1, _tolerance));
                    Assert.That(_negativeLine1.Contains(p2, _tolerance));

                    Assert.That(deductionLines.TrueForAll(dl => !l.Contains(p1, _tolerance, false)));
                });
            });

        }
    }
}
