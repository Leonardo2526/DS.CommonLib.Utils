using NUnit.Framework.Constraints;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils;

namespace DS.VarUtils.Test.RectangleTests
{
    internal class RectangleContainsRectangleTest
    {
        private static Rectangle3d _sourceXYRectangle =
            RectangleFactory.CreateXY(Point3d.Origin, new Point3d(10, 10, 0));

        private static readonly List<Rectangle3d> _rectanglesXYShouldPass = new()
            {
                RectangleFactory.CreateXY(Point3d.Origin, new Point3d(1, 1, 0)),
                RectangleFactory.CreateXY(new Point3d(1,1,0), new Point3d(2, 2, 0)),
                RectangleFactory.CreateXY(new Point3d(1,1,0), new Point3d(15, 15, 0)),
                RectangleFactory.CreateXY(new Point3d(-1,-1,0), new Point3d(15, 15, 0)),
                RectangleFactory.CreateXY(new Point3d(-1,-1,0), new Point3d(0, 0, 0)),
                RectangleFactory.CreateXY(new Point3d(-5,0,0), new Point3d(0, 5, 0))
            };

        [Test]
        public void Test001_XYInsideAndOverlapRectangles_ShouldPass()
        {
            Assert.That(
            _rectanglesXYShouldPass.
            TrueForAll(rect => 
            _sourceXYRectangle.Intersection(rect, false, out var intersectionRectangle)), Is.True);
        }

        [Test]
        public void Test002_XYOutsideRectangles_ShouldFail()
        {
            var rectangles = new List<Rectangle3d>()
            {
                RectangleFactory.CreateXY(new Point3d(11,11,0), new Point3d(12, 12, 0)),
                RectangleFactory.CreateXY(new Point3d(-10,-10,0), new Point3d(-5, -5, 0)),
            };
            Assert.That(
            rectangles.TrueForAll(rect => 
            !_sourceXYRectangle.Intersection(rect, false, out var intersectionRectangle)), Is.True);
        }

        [Test]
        public void Test003_XYAdjacentRectangles_ShouldFail()
        {
            var rectangles = new List<Rectangle3d>()
            {
                 RectangleFactory.CreateXY(new Point3d(-1,-1,0), new Point3d(0, 0, 0)),
                RectangleFactory.CreateXY(new Point3d(-5,0,0), new Point3d(0, 5, 0))
            };
            Assert.That(
            rectangles.TrueForAll(rect => 
            !_sourceXYRectangle.Intersection(rect, true, out var intersectionRectangle)), Is.True);
        }

        [Test]
        public void Test004_XYAdjacentRectanglesStrict_ShouldFail()
        {
            var rectangles = new List<Rectangle3d>()
            {
                RectangleFactory.CreateXY(new Point3d(-1,-1,0), new Point3d(0, 0, 0)),
                RectangleFactory.CreateXY(new Point3d(-5,0,0), new Point3d(0, 5, 0))
            };
            Assert.That(
            rectangles.TrueForAll(rect => 
            !_sourceXYRectangle.Intersection(rect, true, out var intersectionRectangle)), Is.True);
        }

        [Test]
        public void Test005_AnotherPlaneRectangles_ShouldFail()
        {
            var rectangles = new List<Rectangle3d>()
            {
                RectangleFactory.CreateXZ(new Point3d(1,0,1), new Point3d(10, 0, 10)),
                RectangleFactory.CreateYZ(new Point3d(0,1,0), new Point3d(0, 5, 5))
            };
            Assert.That(
            rectangles.TrueForAll(rect => 
            !_sourceXYRectangle.Intersection(rect, false, out var intersectionRectangle)), Is.True);
        }


        [Test]
        public void Test006_TransformToXZ_ShouldPass()
        {
            var sourceTransformed = RectangleFactory.XYToXZ(_sourceXYRectangle);
            var transformedRectangles = new List<Rectangle3d>();
            _rectanglesXYShouldPass.
                ForEach(rect => 
                transformedRectangles.Add(RectangleFactory.XYToXZ(rect)));           

            Assert.That(
            transformedRectangles.TrueForAll(rect => 
            sourceTransformed.Intersection(rect, false, out var intersectionRectangle)), Is.True);

        }


        [Test]
        [TestCase(30)]
        [TestCase(45)]
        [TestCase(90)]
        [TestCase(-90)]
        [TestCase(180)]
        public void Test007_RotatedRectangles_ShouldPass(int angle)
        {
            //transform to XZ
            var sourceTransformed = RectangleFactory.XYToXZ(_sourceXYRectangle);
            var transformedRectangles = new List<Rectangle3d>();
            _rectanglesXYShouldPass.
                ForEach(rect =>
                transformedRectangles.Add(RectangleFactory.XYToXZ(rect)));

            //rotate in XZ
            var a = angle.DegToRad();
            var rotationTransform = Transform.Rotation(a, Vector3d.ZAxis, Point3d.Origin);
            var sourceRotated = rotateRect(sourceTransformed);
            var rotatedRectangles = new List<Rectangle3d>();
            transformedRectangles.ForEach(rect =>  rotatedRectangles.Add(rotateRect(rect)));
            Rectangle3d rotateRect(Rectangle3d rect)
            {
                if (!rect.Transform(rotationTransform))
                { throw new Exception(); }
                return rect;
            }

            Assert.That(
            rotatedRectangles.TrueForAll(rect => 
            sourceRotated.Intersection(rect, false, out var intersectionRectangle)), Is.True);
        }

    }
}
