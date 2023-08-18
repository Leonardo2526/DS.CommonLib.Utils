using Rhino.Geometry;

namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// An object that represents basis of <see cref="Vector3d"/>'s.
    /// </summary>
    public struct Basis3d : IBasis<Vector3d>
    {
        /// <summary>
        /// Instansiate an object that represents basis of <see cref="Vector3d"/>'s.
        /// </summary>
        /// <param name="basisX"></param>
        /// <param name="basisY"></param>
        /// <param name="basisZ"></param>
        public Basis3d(Vector3d basisX, Vector3d basisY, Vector3d basisZ)
        {
            X = basisX; Y = basisY; Z = basisZ;
        }

        /// <summary>
        /// Instansiate an object that represents basis of <see cref="Vector3d"/>'s.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="basisX"></param>
        /// <param name="basisY"></param>
        /// <param name="basisZ"></param>
        public Basis3d(Point3d origin, Vector3d basisX, Vector3d basisY, Vector3d basisZ)
        {
            Origin = origin; X = basisX; Y = basisY; Z = basisZ;
        }

        /// <inheritdoc/>
        public Vector3d X { get; set; }

        /// <inheritdoc/>
        public Vector3d Y { get; set; }

        /// <inheritdoc/>
        public Vector3d Z { get; set; }

        /// <summary>
        /// Basis origin point.
        /// </summary>
        public Point3d Origin { get; set; }
    }
}
