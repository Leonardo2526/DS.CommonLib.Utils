using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
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
        List<Vector3d> Directions { get; }

        /// <summary>
        /// Build object that represents factory to get directions in each plane by basis and <paramref name="angles"/>.
        /// </summary>
        /// <param name="basisX"></param>
        /// <param name="basisY"></param>
        /// <param name="basisZ"></param>    
        /// <param name="angles"></param>
        /// <returns></returns>
        IDirectionFactory Build(Vector3d basisX, Vector3d basisY, Vector3d basisZ, List<int> angles = null);

        /// <summary>
        /// Get directions in each plane.
        /// </summary>
        /// <returns>
        /// Get list of unit vectors in all planes.
        /// </returns>
        List<Vector3d> GetDirections();
    }
}
