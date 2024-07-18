using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests.Rectangles
{
    public static class CreateRectangleTest
    {
        public static Rectangle3d Create()
        {
            var p1 = new Point3d(0, 0, 0);
            var p2 = new Point3d(2, 2, 0);
            var p3 = new Point3d(2, 0, 0);

            var p4 = new Point3d(0, 2, 0);


            var plane = new Plane(p1, p3, p4);

            return new Rectangle3d(plane, p1, p2);
        }
    }
}
