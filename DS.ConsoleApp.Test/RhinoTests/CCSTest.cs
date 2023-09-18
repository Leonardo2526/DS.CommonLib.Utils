using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Basis;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class CCSTest
    {
        public CCSTest()
        {
            Run();
        }

        private void Run()
        {
            //OutputQuadrants();
            //OutputBases(css);
            //OutputOctant(css.Octants[5]);
            //OutputXYQuadrants(css);
            //OutputXZQuadrants(css);
            //OutputYZQuadrants();
        }

        private static void OutputBases()
        {
            //check basis
            for (int i = 0; i < CCS.Octants.Length; i++)
            {
                Octant o = CCS.Octants[i];
                Console.WriteLine($"O{i + 1}");
                Console.WriteLine($"X: " + o.Basis.X);
                Console.WriteLine($"Y: " + o.Basis.Y);
                Console.WriteLine($"Z: " + o.Basis.Z);
                Console.WriteLine("Is righthanded: " + o.Basis.IsRighthanded());
            }
        }


        private static void OutputOctant(Octant octant)
        {
            Console.WriteLine(octant.Box.Center);
            foreach (var q in octant.Quadrants)
            {
                Console.WriteLine("X: " + q.Plane.XAxis);
                Console.WriteLine("Y: " + q.Plane.YAxis);
                Console.WriteLine("Z: " + q.Plane.Normal);
            }
        }

        private static void OutputQuadrants()
        {
            //check quadrants
            for (int i = 0; i < CCS.Quadrants.Length; i++)
            {
                Console.WriteLine($"Q{i + 1}: " + CCS.Quadrants[i].Center);
            }
        }

        private static void OutputXYQuadrants()
        {
            //check quadrants
            foreach (var q in CCS.XYquadrants)
            {
                Console.WriteLine("X: " + q.Plane.XAxis);
                Console.WriteLine("Y: " + q.Plane.YAxis);
                Console.WriteLine("Z: " + q.Plane.Normal);
            }           
        }

        private static void OutputXZQuadrants()
        {
            //check quadrants
            foreach (var q in CCS.XZquadrants)
            {
                Console.WriteLine("X: " + q.Plane.XAxis);
                Console.WriteLine("Y: " + q.Plane.YAxis);
                Console.WriteLine("Z: " + q.Plane.Normal);
            }
        }

        private static void OutputYZQuadrants()
        {
            //check quadrants
            foreach (var q in CCS.YZquadrants)
            {
                Console.WriteLine("X: " + q.Plane.XAxis);
                Console.WriteLine("Y: " + q.Plane.YAxis);
                Console.WriteLine("Z: " + q.Plane.Normal);
            }
        }
    }
}
