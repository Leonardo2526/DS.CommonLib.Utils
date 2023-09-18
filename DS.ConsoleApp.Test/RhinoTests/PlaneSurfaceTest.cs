using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class PlaneSurfaceTest
    {
        private PlaneSurface _planeSurface;

        public PlaneSurfaceTest()
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
            Interval xExtents = new Interval(0, 10);
            Interval yExtens = new Interval(0, 15);

            _planeSurface = new PlaneSurface(plane, xExtents, yExtens);
        }

        private void Output()
        {
            var checkPoint = new Point3d(1,2,3);
            var point = _planeSurface.PointAt(100,100);
            var v = _planeSurface.GetSpanVector(1);

            Console.WriteLine(point);
        }
    }
}
