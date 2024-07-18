using DS.ClassLib.VarUtils.Points;
using Rhino;
using Rhino.Geometry;
using System.Text;

namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// An object that represents basis of <see cref="Vector3d"/>'s.
    /// </summary>
    public struct Basis3d : IBasis<Vector3d>
    {
        private const double _angleTolerance = RhinoMath.DefaultAngleTolerance;
        private const double _tolerance = RhinoMath.ZeroTolerance;

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

        /// <summary>
        /// Determines whether the <paramref name="basis1"/> and <paramref name="basis2"/> 
        /// are equals.
        /// </summary>
        /// <param name="basis1"></param>
        /// <param name="basis2"></param>
        /// <returns>
        /// <see langword="true"/> if all basis have the same directions and origin points.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool operator ==(Basis3d basis1, Basis3d basis2)
        {
            var b1 = basis1.X.IsParallelTo(basis2.X, _angleTolerance) == 1;
            var b2 = basis1.Y.IsParallelTo(basis2.Y, _angleTolerance) == 1;
            var b3 = basis1.Z.IsParallelTo(basis2.Z, _angleTolerance) == 1;
            var b4 = basis1.Origin.IsAlmostEqualTo(basis2.Origin, _tolerance);
            return b1 && b2 && b3 && b4;
        }

        /// <summary>
        /// Determines whether the <paramref name="basis1"/> and <paramref name="basis2"/> 
        /// aren't equals.
        /// </summary>
        /// <param name="basis1"></param>
        /// <param name="basis2"></param>
        /// <returns>
        /// <see langword="true"/> if one of basis haven't the same directions or origin point.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool operator !=(Basis3d basis1, Basis3d basis2)
            => !(basis1 == basis2);
    }
}
