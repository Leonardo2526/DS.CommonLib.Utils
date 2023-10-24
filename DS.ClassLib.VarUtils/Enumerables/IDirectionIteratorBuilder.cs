using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Enumerables
{
    public interface IDirectionIteratorBuilder
    {
        IEnumerator<Vector3d> Build(List<Plane> planes, List<int> angles, Vector3d parentDir);
    }
}