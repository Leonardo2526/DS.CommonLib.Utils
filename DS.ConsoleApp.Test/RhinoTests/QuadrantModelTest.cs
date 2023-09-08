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
        private QuadrantOriginModel _model;

        public QuadrantModelTest()
        {
            Run();
            //Output();
        }

        public void Enable()
        {
            _model.EnableQuadrants();
        }

        public void EnableByVector()
        {
            var vector1 = new Vector3d(0, 0, 1);
            _model.EnableQuadrants(vector1, OrthoPlane.XZ);
            //var vector2 = new Vector3d(0, 0, 1);
            //_model.EnableQuadrants(vector2, OrthoPlane.XZ);
            //var vector2 = new Vector3d(1, 0, 0);
            //_model.EnableQuadrants(vector2);
            var activeQuadrants = _model.GetActiveQuadrants();
            Console.WriteLine(activeQuadrants.Count());
            foreach (var aq in activeQuadrants)
            {
                Console.WriteLine(aq.Item1.Center);
                Console.WriteLine(aq.Item2);
            }
        }

        public void Disable()
        {
            _model.DisableQuadrants();
        }

        public void SetPriority()
        {
            OrthoPlane orthPlane = 0;
            //OrthoPlane orthPlane = OrthoPlane.XY;
            _model.DisableQuadrants(OrthoPlane.YZ);
            //_model.DisableQuadrants(OrthoPlane.XZ);

            //Console.WriteLine("Positive");
            var v = new Vector3d(0, 0, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(1, 0, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(0, 1, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(1, 1, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(0, -1, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));

            //Console.WriteLine("Negative");
            v = new Vector3d(-1, 0, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(-1, -1, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(0, 0, 1);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            v = new Vector3d(0, 1, 1);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
        }

        public void SetPriority1()
        {
            var baseVectror = new Vector3d(1,0,0);
            OrthoPlane orthPlane = 0;
            //OrthoPlane orthPlane = OrthoPlane.XY;
            _model.DisableQuadrants();
            _model.EnableQuadrants(baseVectror);
            //_model.DisableQuadrants(OrthoPlane.XZ);

            //Console.WriteLine("Positive");
            var v = new Vector3d(0, 1, 0);
            Console.WriteLine(v + ": " +_model.TrySetPriority(v, orthPlane));
            Console.WriteLine(_model.PriorityQuadrant.Center);
            v = new Vector3d(0, -1, 0);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            Console.WriteLine(_model.PriorityQuadrant.Center);
            v = new Vector3d(0, 0, 1);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            Console.WriteLine(_model.PriorityQuadrant.Center);
            v = new Vector3d(0, 0, -1);
            Console.WriteLine(v + ": " + _model.TrySetPriority(v, orthPlane));
            Console.WriteLine(_model.PriorityQuadrant.Center);
        }

        private void Run()
        {
            _model = new QuadrantOriginModel();
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
