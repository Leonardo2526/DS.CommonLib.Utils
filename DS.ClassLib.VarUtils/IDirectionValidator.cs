using Rhino.Geometry;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents validator for base geomerty objects.
    /// </summary>
    public interface IDirectionValidator
    {
        /// <summary>
        /// Specifies if <paramref name="point"/> is valid.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool IsValid(Point3d point);

        /// <summary>
        /// Specifies if <paramref name="vector"/> is valid.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        bool IsValid(Vector3d vector);
    }
}