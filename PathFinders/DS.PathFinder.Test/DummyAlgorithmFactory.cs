using DS.PathFinder;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.PathFindTest
{
    internal class DummyAlgorithmFactory : IAlgorithmFactory
    {
        public IPathFindAlgorithm<Point3d> Algorithm { get; set; }

        public List<Point3d> FindPath()
        {
            return new List<Point3d>();
        }

        public void Update(int tolerance)
        {
           
        }
    }
}
