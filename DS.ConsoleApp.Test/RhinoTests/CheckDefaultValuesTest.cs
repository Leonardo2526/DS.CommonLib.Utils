using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class CheckDefaultValuesTest
    {
        public CheckDefaultValuesTest()
        {
            Run();
        }

        private void Run()
        {
            Vector3d vector = default;
            Console.WriteLine(vector.IsValid);

            var origin = Point3d.Origin;
            var v1 = Vector3d.XAxis;
            var v2 = Vector3d.YAxis;

            var plane = new Plane(origin, v1, v2);

            var rectangle = new Rectangle3d(plane, -10, 15);

            Rectangle3d rect = default;
            Rectangle3d rect1 = new Rectangle3d();
            Console.WriteLine(rect.Equals(default));
        }
    }
}
