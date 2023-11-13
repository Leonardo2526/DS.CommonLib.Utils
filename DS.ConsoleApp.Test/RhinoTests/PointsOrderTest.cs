using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils.Points;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class PointsOrderTest
    {
        public PointsOrderTest OrderByDistance()
        {
            var points = new List<Point3d>()
            {  
                new Point3d(), 
                new Point3d(3,0,0),
                new Point3d(1,0,0),
                new Point3d(-2,0,0)
            };
            var ordered =  points.SortByDistance();
            Print(ordered);
            return this;
        }


        private void Print(IEnumerable<Point3d> points)
        {
            points.ToList().ForEach(p => { Console.WriteLine(p); });
        }
    }
}
