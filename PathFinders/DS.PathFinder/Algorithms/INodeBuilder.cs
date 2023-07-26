using Rhino.Geometry;
using System.Windows.Media.Media3D;

namespace FrancoGustavo.Algorithm
{
    public interface INodeBuilder
    {
        PointPathFinderNode Node { get; }

        PointPathFinderNode BuildWithParameters();
        PointPathFinderNode BuildWithPoint(PointPathFinderNode parentNode, Vector3d nodeDir);
    }
}