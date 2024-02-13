using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class Rectangle3dTest
    {
        private Rectangle3d _rectangle;

        public Rectangle3dTest()
        {
            Run();
            Output();
        }

        private void Run()
        {
            var origin = Point3d.Origin;
            var v1 = Vector3d.XAxis;
            var v2 = Vector3d.YAxis;
           
            var plane = new Plane(origin, v1, v2);

            _rectangle = new Rectangle3d(plane, -10, 15);

            
        }

        private void Output()
        {
            var point = new Point3d(-5, 5, 0);

            var cp = _rectangle.Contains(point);
            var plane = _rectangle.Plane;

            var dist = plane.DistanceTo(point);
        

            Console.WriteLine(dist);
            Console.WriteLine(cp);
        }
    }
}
