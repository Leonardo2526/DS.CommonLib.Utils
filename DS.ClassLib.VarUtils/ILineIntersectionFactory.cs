using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    public interface ILineIntersectionFactory
    {
        List<Plane> FirstNodePlanes { get; set; }
        Point3d IntersectionPoint { get; }
        List<Plane> LastNodePlanes { get; set; }
        double MinLinkLength { get; set; }

        Point3d GetIntersection((Point3d point, Vector3d direction) node1, (Point3d point, Vector3d direction) node2);
        ILineIntersectionFactory WithDetector(ITraceCollisionDetector<Point3d> collisionDetector, Basis3d basis, Point3d firstNode, Point3d lastNode);
    }
}