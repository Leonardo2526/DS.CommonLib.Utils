using DS.ClassLib.VarUtils.Basis;
using System;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// An object that represents orthonormal basis of unit vectors.
    /// </summary>
    public readonly struct OrthoNormBasis : IBasis3D
    {
        private readonly int _tolerance = 5;

        /// <summary>
        /// Instantiate an object that represents orthonormal basis of unit vectors.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="tolerance"></param>
        /// <exception cref="ArgumentException"></exception>
        public OrthoNormBasis(Vector3D x, Vector3D y, Vector3D z, int tolerance = 5)
        {
            _tolerance = tolerance;

            var a1 = Math.Round(Vector3D.AngleBetween(x, y), _tolerance) ;
            var a2 = Math.Round(Vector3D.AngleBetween(x, z), _tolerance);
            var a3 = Math.Round(Vector3D.AngleBetween(y, z), _tolerance);

            if (a1 != 90 || a2 != 90 ||a3 != 90)
            { throw new ArgumentException("Angles between vectors for 'OrthoNormBasis' constructor must be 0."); }

            var l1 = Math.Round(x.Length, _tolerance);
            var l2 = Math.Round(y.Length, _tolerance);
            var l3 = Math.Round(z.Length, _tolerance);

            if ( l1 != 1 || l2 != 1 || l3 != 1)
            { throw new ArgumentException("Vectors length for 'OrthoNormBasis' constructor must be 0."); }

            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// First unit vector.
        /// </summary>
        public Vector3D X { get; }

        /// <summary>
        /// Second unit vector.
        /// </summary>
        public Vector3D Y { get; }

        /// <summary>
        /// Third unit vector.
        /// </summary>
        public Vector3D Z { get; }
    }
}
