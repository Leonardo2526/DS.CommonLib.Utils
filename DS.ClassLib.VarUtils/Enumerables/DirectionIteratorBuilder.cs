using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Enumerables
{
    /// <inheritdoc/>
    public class DirectionIteratorBuilder : IDirectionIteratorBuilder
    {
        /// <inheritdoc/>
        public IEnumerator<Vector3d> Build(List<Plane> planes, List<int> angles, Vector3d parentDir)
            => new DirectionIterator(planes, angles) { ParentDir = parentDir };
    }
}
