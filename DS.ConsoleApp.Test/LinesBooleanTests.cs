using Rhino.Geometry.Intersect;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils;

namespace DS.ConsoleApp.Test
{
    internal class LinesBooleanTests
    {

        public LinesBooleanTests()
        {
        }

        public LinesBooleanTests Run()
        {
            var length = 10000;

            var p1 = new Point3d(0, 0, 0);
            var dir1 = new Vector3d(1, 0, 0);
            var l1 = new Line(p1, dir1, length);

            var p2 = new Point3d(5, 5, 0);
            var dir2 = new Vector3d(0, -1, 0);
            var l2 = new Line(p2, dir2, length);


            var intersection = Intersection.LineLine(l1, l2, out double a, out double b, 0.001, true);
            Console.WriteLine(intersection);
            Console.WriteLine(l1.PointAt(a));
            Console.WriteLine(l2.PointAt(b));

            return this;
        }

        public LinesBooleanTests BoolIntersect()
        {
            //var line1 = new Line(new Point3d(5,0,0), new Point3d(0, 0, 0));
            //var line2 = new Line(new Point3d(2, 0, 0), new Point3d(-1, 0, 0));
            var line1 = new Line(new Point3d(0, 0, 0), new Point3d(5, 0, 0));
            var line2 = new Line(new Point3d(-1, 0, 0), new Point3d(2, 0, 0));

            var intersectionLine = LineBooleanTools.Intersect(line1, line2);
            Console.WriteLine(intersectionLine.ToString());
            return this;
        }

        public LinesBooleanTests BoolSubstract()
        {
            var line1 = new Line(new Point3d(4, 0, 0), new Point3d(5, 0, 0));
            var line2 = new Line(new Point3d(3, 0, 0), new Point3d(5, 0, 0));

            var intersectionLines = LineBooleanTools.Substract(line1, line2);
            intersectionLines.ForEach(l => Console.WriteLine(l.ToString()));
            return this;
        }

        public LinesBooleanTests BoolSubstractMultiple()
        {
            var minuendLine = new Line(new Point3d(0, 0, 0), new Point3d(10, 0, 0));
            List<Line> deductionLines = new List<Line>()
            {
                new Line(new Point3d(8, 0, 0), new Point3d(9, 0, 0)),
                new Line(new Point3d(2, 0, 0), new Point3d(4, 0, 0)),
                new Line(new Point3d(-1, 0, 0), new Point3d(3, 0, 0)),
            };

            //List<Line> deductionLines = new List<Line>()
            //{
            //    new Line(new Point3d(1, 0, 0), new Point3d(4, 0, 0)),
            //    new Line(new Point3d(3, 0, 0), new Point3d(5, 0, 0))
            //};


            var intersectionLines = LineBooleanTools.Substract(minuendLine, deductionLines);
            intersectionLines.ForEach(l => Console.WriteLine(l.ToString()));
            return this;
        }
    }
}
