using System;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// An object that represents orthogonal basis of vectors.
    /// </summary>
    public readonly struct OrthoBasis : IBasis
    {
        private readonly int _tolerance = 5;
        private readonly Vector3D _result;

        /// <summary>
        /// Instantiate an object that represents orthogonal basis of vectors.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="tolerance"></param>
        /// <exception cref="ArgumentException"></exception>
        public OrthoBasis(Vector3D x, Vector3D y, Vector3D z, int tolerance = 5)
        {
            _tolerance = tolerance;

            var a1 = Math.Round(Vector3D.AngleBetween(x, y), _tolerance);
            var a2 = Math.Round(Vector3D.AngleBetween(x, z), _tolerance);
            var a3 = Math.Round(Vector3D.AngleBetween(y, z), _tolerance);

            if (a1 != 90 || a2 != 90 || a3 != 90)
            { throw new ArgumentException("Angles between vectors for 'OrthoNormBasis' constructor must be 0."); }

            X = x;
            Y = y;
            Z = z;
            _result = x + y + z; 
        }

        /// <inheritdoc/>
        public Vector3D X { get; }

        /// <inheritdoc/>
        public Vector3D Y { get; }

        /// <inheritdoc/>
        public Vector3D Z { get; }

        /// <summary>
        /// Sum of X, Y and Z.
        /// </summary>
        public Vector3D Result => _result;

        /// <summary>
        /// Get negate basis.
        /// </summary>
        /// <returns></returns>
        public OrthoBasis Negate()
        {
            var nx = X.DeepCopy();
            nx.Negate();
            var ny = Y.DeepCopy();
            ny.Negate();
            var nz = Z.DeepCopy();
            nz.Negate();
            return new OrthoBasis(nx, ny, nz);
        }
    }
}
