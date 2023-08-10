using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Points
{
    /// <summary>
    /// The interface used to create objects to convert points between coordinate systems.
    /// </summary>
    public interface IPointConverter
    {
        /// <summary>
        /// Convert point to UCS1.
        /// </summary>
        /// <param name="uCS2Point"></param>
        /// <returns>Point in UCS1 coordinate system.</returns>
        Point3D ConvertToUCS1(Point3D uCS2Point);

        /// <summary>
        /// Convert point to UCS2.
        /// </summary>
        /// <param name="uCS1Point"></param>
        /// <returns>Point in UCS2 coordinate system.</returns>
        Point3D ConvertToUCS2(Point3D uCS1Point);
    }

    /// <summary>
    /// The interface used to create objects to convert points between coordinate systems.
    /// </summary>
    public interface IPoint3dConverter
    {
        /// <summary>
        /// Convert point to UCS1.
        /// </summary>
        /// <param name="uCS2Point"></param>
        /// <returns>Point in UCS1 coordinate system.</returns>
        Point3d ConvertToUCS1(Point3d uCS2Point);

        /// <summary>
        /// Convert point to UCS2.
        /// </summary>
        /// <param name="uCS1Point"></param>
        /// <returns>Point in UCS2 coordinate system.</returns>
        Point3d ConvertToUCS2(Point3d uCS1Point);

        /// <summary>
        /// Convert vector to UCS1.
        /// </summary>
        /// <param name="uCS2Vector"></param>
        /// <returns>Vector in UCS1 coordinate system.</returns>
        Vector3d ConvertToUCS1(Vector3d uCS2Vector);

        /// <summary>
        /// Convert vector to UCS2.
        /// </summary>
        /// <param name="uCS1Vector"></param>
        /// <returns>Vector in UCS2 coordinate system.</returns>
        Vector3d ConvertToUCS2(Vector3d uCS1Vector);

        /// <summary>
        /// Convert basis to UCS1.
        /// </summary>
        /// <param name="uCS2Basis"></param>
        /// <returns>Basis in UCS1 coordinate system.</returns>
        Basis3d ConvertToUCS1(Basis3d uCS2Basis);

        /// <summary>
        /// Convert basis to UCS2.
        /// </summary>
        /// <param name="uCS1Basis"></param>
        /// <returns>Basis in UCS2 coordinate system.</returns>
        Basis3d ConvertToUCS2(Basis3d uCS1Basis);
    }
}
