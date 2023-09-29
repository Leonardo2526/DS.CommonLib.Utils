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
            var dist = Run();
            Console.WriteLine(dist);
        }

        private double Run()
        {
            var p1 = new Point3d(0, 0, 0);
            var p2 = new Point3d(10, 0, 0);
            var line = new Line(p1, p2);

            var testPoint = new Point3d(10, 0, 0);
            //var testPoint = new Point3d(0, 0, 1);
            return  line.DistanceTo(testPoint, true);
        }
    }
}
