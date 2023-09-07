using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class QuadrantModelTest
    {
        private QuadrantModel _model;

        public QuadrantModelTest()
        {
            Run();
            Output();
        }

        private void Run()
        {
          _model = new QuadrantModel();
            _model.Quadrants[0].Item2 = false;
        }

        private void Output()
        {

            foreach (var quadrant in _model.XYquadrants)
            {
                if (!quadrant.Item2) { continue; }
                var plane = quadrant.Item1.Plane;

                Console.WriteLine("Center = " + quadrant.Item1.Center);
                Console.WriteLine("Corner1 = " + quadrant.Item1.Corner(0));
                Console.WriteLine("Corner2 = " + quadrant.Item1.Corner(1));
                Console.WriteLine("Corner3 = " + quadrant.Item1.Corner(2));
                Console.WriteLine("Corner4 = " + quadrant.Item1.Corner(3));
                Console.WriteLine("X = " + plane.XAxis);
                Console.WriteLine("Y = " + plane.YAxis);
                Console.WriteLine("N = " + plane.Normal);
                Console.WriteLine();
            }
        }
    }
}
