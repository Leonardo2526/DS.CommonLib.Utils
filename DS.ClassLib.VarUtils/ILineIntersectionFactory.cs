using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Collisions;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Interface that represents factory to find <see cref="Line"/>'s intersections.
    /// </summary>
    public interface ILineIntersectionFactory
    {
        /// <summary>
        /// <see cref="Plane"/>'s to find intersection at first node.
        /// </summary>
        List<Plane> FirstNodePlanes { get; set; }

        /// <summary>
        /// <see cref="Point3d"/> of intersection.
        /// </summary>
        Point3d IntersectionPoint { get; }

        /// <summary>
        /// <see cref="Plane"/>'s to find intersection at last node.
        /// </summary>
        List<Plane> LastNodePlanes { get; set; }

        /// <summary>
        /// Minimum length between points.
        /// </summary>
        double MinLinkLength { get; set; }

        /// <summary>
        /// Get <see cref="Line"/>'s intersection point beteween <paramref name="node1"/> and <paramref name="node2"/>.
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns>
        /// Intersection <see cref="Point3d"/>.
        /// </returns>
        Point3d GetIntersection(
            (Point3d point, Vector3d direction) node1, 
            (Point3d point, Vector3d direction) node2);

        /// <summary>
        /// Specify dectector to find collision.
        /// </summary>
        /// <param name="collisionDetector"></param>
        /// <param name="basis"></param>
        /// <param name="firstNode"></param>
        /// <param name="lastNode"></param>
        /// <returns>
        /// Detector to check collisions.
        /// </returns>
        ILineIntersectionFactory WithDetector(ITraceCollisionDetector<Point3d> collisionDetector, 
            Basis3d basis, Point3d firstNode, Point3d lastNode);
    }
}