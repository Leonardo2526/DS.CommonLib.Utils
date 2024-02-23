using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    internal class NodeVectorFactory : INodeVectoryFactory
    {
        public Vector3d Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public Vector3d Create()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
