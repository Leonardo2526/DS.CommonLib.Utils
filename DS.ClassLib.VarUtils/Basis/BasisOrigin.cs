using Rhino.Geometry;

namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// Origin 3-dimensional orthonormal basis.
    /// </summary>
    public struct Basis3dOrigin : IBasis<Vector3d>
    {
        /// <inheritdoc/>
        public readonly Vector3d X => Vector3d.XAxis;

        /// <inheritdoc/>
        public readonly Vector3d Y => Vector3d.YAxis;

        /// <inheritdoc/>
        public readonly Vector3d Z => Vector3d.ZAxis;

        /// <summary>
        /// Basis origin point.
        /// </summary>
        public readonly Point3d Origin => Point3d.Origin;
    }
}
