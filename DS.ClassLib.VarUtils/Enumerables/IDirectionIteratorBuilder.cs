using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Enumerables
{
    /// <summary>
    /// The interface is used to create builder for direction iterator.
    /// </summary>
    public interface IDirectionIteratorBuilder
    {
        /// <summary>
        /// Build iterator.
        /// </summary>
        /// <param name="planes"></param>
        /// <param name="angles"></param>
        /// <param name="parentDir"></param>
        /// <returns>
        /// An object to iterate through directions.
        /// </returns>
        IEnumerator<Vector3d> Build(List<Plane> planes, List<int> angles, Vector3d parentDir);
    }
}