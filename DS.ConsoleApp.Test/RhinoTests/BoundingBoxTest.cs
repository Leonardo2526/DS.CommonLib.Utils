using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class BoundingBoxTest
    {
        private BoundingBox _box;

        public BoundingBoxTest()
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

            var minPoint = new Point3d(double.MinValue, double.MinValue, double.MinValue);
            var maxPoint = new Point3d(double.MaxValue, double.MaxValue, double.MaxValue);


            _box = new BoundingBox(minPoint, maxPoint);
        }

        private void Output()
        {
            Console.WriteLine(_box.Center);
        }
    }
}
