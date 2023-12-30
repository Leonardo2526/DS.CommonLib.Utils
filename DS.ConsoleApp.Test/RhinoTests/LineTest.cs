using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class LineTest
    {
        public LineTest()
        {
         
        }

        public double Run()
        {
            var p1 = new Point3d(0, 0, 0);
            var p2 = new Point3d(10, 0, 0);
            var line = new Line(p1, p2);

            var testPoint = new Point3d(10, 0, 0);
            //var testPoint = new Point3d(0, 0, 1);
            return  line.DistanceTo(testPoint, true);
        }

        public void CompareLines()
        {
            var p11 = new Point3d(0, 0, 0);
            var p21 = new Point3d(10, 0, 0);
            var line1 = new Line(p11, p21);

            var p12 = new Point3d(0, 0, 0);
            var p22 = new Point3d(10, 0, 0);
            var line2 = new Line(p12, p22);

            Console.WriteLine(line1 == line2);
        }
    }
}
