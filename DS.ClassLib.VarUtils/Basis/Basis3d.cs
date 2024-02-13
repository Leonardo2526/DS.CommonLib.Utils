using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System.Text;

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

        /// <summary>
        /// Round each item of basis to specified <paramref name="tolerance"/>.
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public Basis3d Round(int tolerance = 3) =>
          new Basis3d(
              Origin.Round(tolerance),
              X.Round(tolerance), Y.Round(tolerance), Z.Round(tolerance));

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Origin: ({Origin})");
            sb.AppendLine($"basisX: ({X})");
            sb.AppendLine($"basisY: ({Y})");
            sb.AppendLine($"basisZ: ({Z})");

            return sb.ToString();
        }
    }
}
