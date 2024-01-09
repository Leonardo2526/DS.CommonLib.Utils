using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests.Rectangles
{
    internal static class ConvertToRectangleTest
    {
        private static readonly double _rotationAngle = 45.DegToRad();
        private static readonly double _at = 1.DegToRad();

        public static void Convert1()
        {
            //initiate lines
            var sourcePoints = GetPoints();
            var points = Rotate(sourcePoints).ToList();
            //var lines = GetLines(points);
            //points = ToPoints(lines).ToList();

            var sourcePolyLine = new Polyline(points);
            var sourceLines = sourcePolyLine.GetSegments();


            //convert to rectangle
            var polyLine = ToPolyLine(sourceLines);
            var lines = polyLine.GetSegments();
            if (!polyLine.ToPolylineCurve().TryGetPlane(out var plane))
            { throw new Exception(); }

            //var sourcePlane = Plane.WorldXY;
            //var plane = sourcePlane;
            //plane.Rotate(_rotationAngle, Vector3d.ZAxis);

            var box = new Box(plane, points);
            var boxCorners = box.GetCorners();
            var rectangle = new Rectangle3d(box.Plane, boxCorners[0], boxCorners[2]);

            PrintCorners(rectangle);
        }

        public static void Convert2()
        {
            //initiate lines
            var sourcePoints = GetPoints();
            var points = Rotate(sourcePoints).ToList();           
            //var lines = GetLines(points);
            //points = ToPoints(lines).ToList();

            var sourcePolyLine = new Polyline(points);
            var sourceLines = sourcePolyLine.GetSegments();

            var b = GeometryUtils.TryCreateRectangle(sourceLines, out var rectangle);
            Console.WriteLine(b);
            PrintCorners(rectangle);
        }


        private static IEnumerable<Point3d> Rotate(IEnumerable<Point3d> points)
        {
            var transformedPoints = new List<Point3d>();

            var rotationTransform = Transform.Rotation(_rotationAngle, Vector3d.ZAxis, points.ToList()[0]);
            foreach (var p in points)
            {
                p.Transform(rotationTransform);
                transformedPoints.Add(p);
            }

            return transformedPoints;
        }

        private static List<Point3d> GetPoints()
        {
            return new List<Point3d>()
            {
                new  Point3d(),
                new Point3d(1,0,0),
                new Point3d(2,0,0),
                new Point3d(2,2,0),
                new Point3d(0,2,0),
                new  Point3d()
            };
        }

        private static IEnumerable<Line> GetLines(List<Point3d> points)
        {
            var lines = new List<Line>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                var line = new Line(points[i], points[i + 1]);
                lines.Add(line);
            }
            return lines;
        }

        private static IEnumerable<Point3d> ToPoints(IEnumerable<Line> lines)
        {
            var points = new List<Point3d>();

            lines.ToList().ForEach(l => { points.Add(l.From); points.Add(l.To); });

            return points;
        }

        private static Plane TryGetPlane(IEnumerable<Line> lines)
        {
            var points = ToPoints(lines);
            var polyline = new Polyline(points);
            polyline.ToPolylineCurve().TryGetPlane(out var plane);

            return plane;
        }

        private static Polyline ToPolyLine(IEnumerable<Line> lines)
        {
            var points = ToPoints(lines);
            var polyline = new Polyline(points);           
            polyline.DeleteShortSegments(0.001);
            var removed = polyline.MergeColinearSegments(_at, true);
            return polyline;
        }


        private static void PrintCorners(Rectangle3d rectangle)
        {
            var corners = rectangle.GetCorners().ToList();

            var sb = new StringBuilder();
            corners.ForEach(c =>  sb.AppendLine(c.ToString()));

            Console.WriteLine(sb.ToString());
        }
    }
}
