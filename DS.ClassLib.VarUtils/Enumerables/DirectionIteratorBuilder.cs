using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Enumerables
{
    public class DirectionIteratorBuilder : IDirectionIteratorBuilder
    {
        public IEnumerator<Vector3d> Build(List<Plane> planes, List<int> angles, Vector3d parentDir)
            => new DirectionIterator(planes, angles) { ParentDir = parentDir };
    }
}
