using DS.ClassLib.VarUtils;
using Rhino.Geometry;

namespace DS.ClassLib.VarUtils
{
    public interface IDirectionValidator
    {
        bool IsValid(Vector3d vector, OrthoPlane orthoPlane = 0);
        bool IsValid(Point3d point, OrthoPlane orthoPlane = 0);
    }
}