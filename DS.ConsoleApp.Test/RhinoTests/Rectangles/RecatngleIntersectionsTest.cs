using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal static class RecatngleIntersectionsTest
    {
        public static void Run1()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 10, 0));
            var r2 = GetXYRectangle(new Point3d(1, 0, 0), new Point3d(2, 2, 0));
            var r3 = GetXYRectangle(new Point3d(-10, -10, 0), new Point3d(-5, -5, 0));
            var r4 = GetXYRectangle(new Point3d(1, 0, 0), new Point3d(2, 2, 0),
                new Plane(new Point3d(0, 0, 1), Vector3d.XAxis, Vector3d.YAxis));

            var p1 = r1.Plane;
            var p2 = r2.Plane;
            var p3 = r3.Plane;
            var p4 = r4.Plane;

            var corners1 = r1.GetCorners();
            var corners2 = r2.GetCorners();
            var corners3 = r3.GetCorners();
            var corners4 = r4.GetCorners();
            var box1 = new Box(p1, corners1);
            var box2 = new Box(p2, corners2);
            var box3 = new Box(p3, corners3);
            var box4 = new Box(p4, corners4);

            Interval intervalX = new Interval(0, r1.Width);
            Interval intervalY = new Interval(0, r1.Height);
            Interval intervalZ = new Interval(0, 1);
            var box11 = new Box(p1, intervalX, intervalY, intervalZ);
            var box21 = new Box(p2, intervalX, intervalY, intervalZ);
            var box31 = new Box(p3, intervalX, intervalY, intervalZ);


            //var b =  Intersection.PlanePlane(p1, p2, out var line);
            //Console.WriteLine(b);

            var b12 = box1.Contains(box2);
            var b13 = box1.Contains(box3);
            var b14 = box1.Contains(box4);
            Console.WriteLine(b12);
            Console.WriteLine(b13);
            Console.WriteLine(b14);
        }

        public static void Run2()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 10, 0), Plane.WorldXY);
            var r2 = GetXYRectangle(new Point3d(1, 1, 0), new Point3d(20, 20, 0), Plane.WorldXY);
            var r3 = GetXYRectangle(new Point3d(5, 0, 0), new Point3d(6, 0, 2), Plane.WorldZX);
            var r4 = GetXYRectangle(new Point3d(-10, -10, 0), new Point3d(-5, -5, 0), Plane.WorldXY);

            //var a1 = 30.DegToRad();
            //var a2 = 45.DegToRad();
            //var rotation30Transform = Transform.Rotation(a1, Vector3d.ZAxis, Point3d.Origin);
            //var rotation45Transform = Transform.Rotation(a2, Vector3d.ZAxis, Point3d.Origin);
            //if (!r1.Transform(rotation45Transform))
            //{ throw new Exception(); }
            //if (!r2.Transform(rotation45Transform))
            //{ throw new Exception(); }
            //if (!r3.Transform(rotation30Transform))
            //{ throw new Exception(); }

            var p1 = r1.Plane;
            var p2 = r2.Plane;
            var p3 = r3.Plane;

            var corners1 = r1.GetCorners();
            var corners2 = r2.GetCorners();
            var corners3 = r3.GetCorners();
            var corners4 = r4.GetCorners();
            var box1 = new BoundingBox(corners1);
            var box2 = new BoundingBox(corners2);
            var box3 = new BoundingBox(corners3);
            var box4 = new BoundingBox(corners4);

            var r12Contains = r1.Intersection(r2, false, out var intersectionRectangle1);
            var bb12 = BoundingBox.Intersection(box1, box2);
            var bb13 = BoundingBox.Intersection(box1, box3);
            var bb14 = BoundingBox.Intersection(box1, box4);
            var b12 = box1.Contains(box2);
            var b21 = box2.Contains(box1);
            Console.WriteLine(r12Contains);

            var b13 = r1.Intersection(r3, true, out var intersectionRectangle2);
            //var b13 = box1.Contains(box3);
            Console.WriteLine(b13);
        }

        public static void Run3()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 10, 0), Plane.WorldXY);
            var r2 = GetXYRectangle(new Point3d(1, 0, 0), new Point3d(2, 2, 0), Plane.WorldXY);

            var a = 45.DegToRad();
            var rotationTransform = Transform.Rotation(a, Vector3d.ZAxis, Point3d.Origin);
            if (!r1.Transform(rotationTransform) || !r2.Transform(rotationTransform))
            { throw new Exception(); }


            Box box1 = GetBox(r1, 2);
            Box box2 = r2.GetMinBox();
            //Box box2 = GetBox(r2, 1);

            var b12 = box1.Contains(box2, true);
            Console.WriteLine(b12);

            Box GetBox(Rectangle3d rectangle, double offset)
            {
                var plane = rectangle.Plane;

                var corners = rectangle.GetCorners().ToList();

                var extCorners = new List<Point3d>()
                {
                    corners[0], corners[1]
                };
                var translationVector = Vector3d.Multiply(plane.Normal, offset);
                var t1 = Transform.Translation(translationVector);
                var t2 = Transform.Translation(-translationVector);

                var ce2 = corners[2];
                var ce3 = corners[3];

                ce2.Transform(t1);
                extCorners.Add(ce2);
                ce3.Transform(t2);
                extCorners.Add(ce3);
                //if (extCorners[0].Transform(t1)) { throw new Exception(); }

                //var c0 = rectangle.Corner(0);
                //Interval intervalX = new Interval(c0.X - plane.OriginX, c0.X - plane.OriginX + rectangle.Width);
                //Interval intervalY = new Interval(c0.Y - plane.OriginY, c0.Y - plane.OriginY + rectangle.Height);
                //Interval intervalZ = new Interval(-offset,  offset);
                return new Box(plane, extCorners);
            }
        }

        public static void Run4()
        {
            var r1 = new Rectangle(0, 0, 5, 5);
            var r2 = new Rectangle(0, 0, 2, 1);
            var r3 = new Rectangle(0, 0, 10, 10);
            var r4 = new Rectangle(10, 10, 10, 10);

            Console.WriteLine(r1.IntersectsWith(r2));
            Console.WriteLine(r1.IntersectsWith(r3));
            Console.WriteLine(r1.IntersectsWith(r4));
        }

        public static void RunWithTransformToBoundingBox()
        {
            var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 0, 10), Plane.WorldZX);
            var sourceR1 = r1;
            var r2 = GetXYRectangle(new Point3d(3, 0, 3), new Point3d(10, 0, 10), Plane.WorldZX);
            var sourceR2 = r2;

            //var r1 = GetXYRectangle(new Point3d(), new Point3d(10, 10, 0), Plane.WorldXY);
            //var r2 = GetXYRectangle(new Point3d(-1, -1, 0), new Point3d(-2, -2, 0), Plane.WorldXY);

            var a = 45.DegToRad();
            var rotation45Transform = Transform.Rotation(a, Vector3d.ZAxis, Point3d.Origin);
            if (!r1.Transform(rotation45Transform))
            { throw new Exception(); }
            if (!r2.Transform(rotation45Transform))
            { throw new Exception(); }

            var targetRectangle1 = r1;
            var targetRectangle2 = r2;
            var rotationTransform1 = Transform.PlaneToPlane(r1.Plane, Plane.WorldXY);
            if (!targetRectangle1.Transform(rotationTransform1))
            { throw new Exception(); }
            var rotationTransform2 = Transform.PlaneToPlane(r2.Plane, Plane.WorldXY);
            if (!targetRectangle2.Transform(rotationTransform2))
            { throw new Exception(); }

            var corners1 = targetRectangle1.GetCorners();
            var box1 = new BoundingBox(corners1);
            var corners2 = targetRectangle2.GetCorners();
            var box2 = new BoundingBox(corners2);
            var intersectionBB = BoundingBox.Intersection(box1, box2);
            Console.WriteLine(intersectionBB.IsValid);
        }


        private static Rectangle3d GetXYRectangle(Point3d p1, Point3d p2, Plane plane = default)
        {

            if (plane == default)
            {
                var origin = Point3d.Origin;
                var v1 = Vector3d.XAxis;
                var v2 = Vector3d.YAxis;
                plane = new Plane(origin, v1, v2);
            }

            return new Rectangle3d(plane, p1, p2);
        }

        private static Rectangle3d GetXYZRectangle(Point3d p1, Point3d p2)
        {
            var origin = Point3d.Origin;
            var v1 = Vector3d.XAxis;
            var v2 = Vector3d.YAxis;
            var x = v1 + v2;
            x.Unitize();

            var plane = new Plane(origin, x, Vector3d.ZAxis);

            return new Rectangle3d(plane, p1, p2);
        }
    }
}
