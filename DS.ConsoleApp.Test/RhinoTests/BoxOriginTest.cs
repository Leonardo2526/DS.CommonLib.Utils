using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class BoxOriginTest
    {
        private Rectangle3d[] _quads;
        private BoundingBox[] _octs;

        public BoxOriginTest()
        {
            Run();
            Output();
        }

        private void Run()
        {
            _quads = BoxOrigin.Quadrants;
            _octs = BoxOrigin.Octants;
        }

        private void Output()
        {
            Console.WriteLine(_octs.Count());
            foreach (var q in _quads)
            {
                Console.WriteLine(q.Plane.Normal);
            }

            Console.WriteLine(_quads.Count());
            for (int i = 0; i < _octs.Length; i++)
            {
                BoundingBox o = _octs[i];
                int c = i + 1;
                Console.WriteLine("Octant " + c);
                Console.WriteLine("MinPoint: " + o.Min);
                Console.WriteLine("MaxPoint: " + o.Max);
                Console.WriteLine();
            }
        }
    }
}
