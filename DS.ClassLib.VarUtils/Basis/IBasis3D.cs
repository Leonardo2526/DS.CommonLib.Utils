using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// An object that represents basis of vectors.
    /// </summary>
    public interface IBasis3D
    {
        /// <summary>
        /// First vector.
        /// </summary>
        public Vector3D X { get; }

        /// <summary>
        /// Second vector.
        /// </summary>
        public Vector3D Y { get; }

        /// <summary>
        /// Third vector.
        /// </summary>
        public Vector3D Z { get; }
    }
}