using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Basis
{
    /// <summary>
    /// An object that represents extension methods for some basis.
    /// </summary>
    public static class BasisExtensions
    {
        /// <summary>
        /// Test whether <paramref name="basis"/> three vectors describe a right-handed, orthogonal, unit axis system. 
        /// The vectors must be orthonormal and follow the right-hand ordering; index-finger=x, middle-finger=y, thumb=z.
        /// </summary>
        /// <param name="basis"></param>
        /// <returns>
        /// True if all <paramref name="basis"/> vectors are non-zero and mutually perpendicular.
        /// </returns>
        public static bool IsRighthanded(this Basis3d basis) => 
            Vector3d.AreRighthanded(basis.X, basis.Y, basis.Z);

        /// <summary>
        /// Test whether <paramref name="basis"/> three vectors describe an orthogonal, unit axis system. 
        /// All vectors must be mutually perpendicular and have unit length for this to be the case.
        /// </summary>
        /// <param name="basis"></param>
        /// <returns>
        /// True if all <paramref name="basis"/> vectors are non-zero and mutually perpendicular.
        /// </returns>
        public static bool IsOrthonormal(this Basis3d basis) =>
          Vector3d.AreOrthonormal(basis.X, basis.Y, basis.Z);

        /// <summary>
        /// Normilze <paramref name="basis"/> that indicates that the length of each basis coordinates is equals one (a unit vector).
        /// </summary>
        /// <param name="basis"></param>
        /// <returns>
        /// Returns a new <see cref="Basis3d"/> whose coordinates are the normalized values and origin point as <paramref name="basis"/> origin point.
        /// </returns>
        public static Basis3d Normalize(this Basis3d basis)
        {
            Basis3d nBasis = basis;

            //nBasis.X.Unitize();
            //nBasis.Y.Unitize();
            //nBasis.Z.Unitize();

            nBasis.X = Vector3d.Divide(basis.X, basis.X.Length);
            nBasis.Y = Vector3d.Divide(basis.Y, basis.Y.Length);
            nBasis.Z = Vector3d.Divide(basis.Z, basis.Z.Length);


            return nBasis;
        }

        /// <summary>
        /// Get <see cref="Basis3d"/> by X direction as <paramref name="targetBasisX"/> 
        /// and Y direction as <see cref="Vector3d"/> at XY or XZ plane of <paramref name="sourceBasis"/>.
        /// <para>
        /// That means that <paramref name="sourceBasis"/> to <paramref name="targetBasisX"/> transformation can be performed by 
        /// one rotation around Y or Z basis of <paramref name="sourceBasis"/>.
        /// </para>
        /// </summary>
        /// <param name="sourceBasis"></param>
        /// <param name="targetBasisX"></param>
        /// <param name="aTolerance"></param>
        /// <returns>
        /// New righthanded orthonormal <see cref="Basis3d"/> with origin point as <paramref name="sourceBasis"/> orgin point.
        /// </returns>
        public static Basis3d GetBasis(
            this Basis3d sourceBasis, Vector3d targetBasisX,  double aTolerance = 0.0174533)
        {
            Vector3d basisX = targetBasisX;

            Vector3d basisY;
            var xCross = Vector3d.CrossProduct(basisX, sourceBasis.X);
            if (basisX.IsPerpendicularTo(sourceBasis.Y, aTolerance)) { basisY = sourceBasis.Y; }
            else if (basisX.IsPerpendicularTo(sourceBasis.Z, aTolerance)) { basisY = Vector3d.CrossProduct(sourceBasis.Z, basisX); ; }
            else if (basisX.IsPerpendicularTo(sourceBasis.X, aTolerance)) { basisY = sourceBasis.X; }
            else { basisY = xCross; }

            Vector3d basisZ = Vector3d.CrossProduct(basisX, basisY);

            var nBasis = new Basis3d(sourceBasis.Origin, basisX, basisY, basisZ);
            return nBasis.Normalize();
        }

        /// <summary>
        /// Computes a change of basis transformation. A basis change is essentially a remapping of geometry from one coordinate system to another.
        /// </summary>
        /// <param name="sourceBasis"></param>
        /// <param name="targetBasis"></param>
        /// <returns>
        /// <see cref="RG.Transform"/> to change <paramref name="sourceBasis"/> to <paramref name="targetBasis"/>.
        /// </returns>
        public static Transform GetTransform(this Basis3d sourceBasis, Basis3d targetBasis) =>
            RG.Transform.ChangeBasis(sourceBasis.X, sourceBasis.Y, sourceBasis.Z, targetBasis.X, targetBasis.Y, targetBasis.Z);

        /// <summary>
        /// Convert <paramref name="sourceBasis"/> to rightanded and orthonormal <see cref="Basis3d"/>..
        /// </summary>
        /// <param name="sourceBasis"></param>
        /// <returns>
        /// A new rightanded and orthonormal <see cref="Basis3d"/>.
        /// </returns>
        public static Basis3d ToRightandedOrthonormal(this Basis3d sourceBasis)
        {
            if(sourceBasis.IsOrthonormal() && sourceBasis.IsRighthanded()) 
            { return sourceBasis; }

            var basisX = sourceBasis.X;
            basisX.Unitize();
            var basisZ = Vector3d.CrossProduct(sourceBasis.X, sourceBasis.Y);
            basisZ.Unitize();
            var basisY = Vector3d.CrossProduct(basisZ, sourceBasis.X);
            basisY.Unitize();

            return new Basis3d(sourceBasis.Origin, basisX, basisY, basisZ);  
        }

        /// <summary>
        /// Perform <paramref name="transform"/> to <paramref name="basis"/>.
        /// </summary>
        /// <param name="basis"></param>
        /// <param name="transform"></param>
        /// <returns>
        /// The result <see cref="Basis3d"/> of transformation.
        /// </returns>
        public static Basis3d Transform(this Basis3d basis, Transform transform)
        {
            var origin = new Point3d(basis.Origin);
            var x = new Vector3d(basis.X);
            var y = new Vector3d(basis.Y);  
            var z = new Vector3d(basis.Z);

            origin.Transform(transform);
            x.Transform(transform);
            y.Transform(transform);
            z.Transform(transform);

            return new Basis3d(origin, x, y, z);
        }
    }
}
