using Rhino.Geometry;

namespace DS.PathFinder.UnitTests
{
    internal interface IPathValidator
    {
        PathValidator Config(Vector3d startDirection, Vector3d endDirection, 
            int angle, double minCurveLength = double.MaxValue, int length = 0);
        void ShouldPass(List<Point3d> path);
    }
}