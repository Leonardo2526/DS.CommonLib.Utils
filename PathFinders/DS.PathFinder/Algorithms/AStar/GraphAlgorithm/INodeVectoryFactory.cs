using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public interface INodeVectoryFactory : IEnumerator<Vector3d>
    {
        Vector3d Create();
    }
}
