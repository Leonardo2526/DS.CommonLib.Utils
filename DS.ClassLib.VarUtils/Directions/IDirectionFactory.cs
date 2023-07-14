using DS.ClassLib.VarUtils.Points;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Directions
{
    /// <summary>
    ///  An object that represents factory to get directions.
    /// </summary>
    public interface IDirectionFactory
    {

        /// <summary>
        /// List of unit vectors in all planes.
        /// </summary>
        List<Vector3D> Directions { get; }

        /// <summary>
        /// Build object that represents factory to get directions in each plane by <paramref name="basis"/> and <paramref name="angles"/>.
        /// </summary>
        /// <param name="basis"></param>
        /// <param name="angles"></param>
        /// <returns></returns>
        IDirectionFactory Build(OrthoNormBasis basis, List<int> angles = null);

        /// <summary>
        /// Get directions in each plane.
        /// </summary>
        /// <returns>
        /// Get list of unit vectors in all planes.
        /// </returns>
        List<Vector3D> GetDirections();
    }
}
