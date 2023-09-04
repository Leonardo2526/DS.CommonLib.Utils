using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// Extension methods for <see cref="IGraph"/>.
    /// </summary>
    public static class GraphExtensions
    {
        /// <summary>
        /// Get all planes by nodes of <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>
        /// All planes built by nodes of <paramref name="graph"/>.
        /// <para>
        /// Empty list if nodes count is less than 3 or no valid planes can be built by them.
        /// </para>
        /// </returns>
        public static List<Plane> GetPlanes(this IGraph graph)
        {
            var planes = new List<Plane>();

            if (graph.Nodes.Count < 3) { return planes; }

            var at = 3.DegToRad();
            var origin = graph.Nodes[0];
            var xLink = new Line(graph.Nodes[1], origin);
            var xDirection = xLink.Direction;

            for (int i = 1; i < graph.Nodes.Count; i++)
            {
                var n = graph.Nodes[i];
                var yDirection = new Line(n, origin).Direction;
                if (xDirection.IsParallelTo(yDirection, at) == 0)
                {
                    var plane = new Plane(origin, xDirection, yDirection);
                    if (plane.IsValid && planes.Exists(p => p.Normal.IsParallelTo(plane.Normal, at) == 0))
                    { planes.Add(plane); }
                }
            }

            return planes;
        }
        /// <summary>
        /// Specifies if all <paramref name="graph"/> nodes are coplanar.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>
        /// <see langword="true"/> if all <paramref name="graph"/> nodes are in the same plane.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsCoplanar(this IGraph graph)
        {
            var planesCount = graph.GetPlanes().Count;
            return planesCount == 0 || planesCount == 1;
        }
    }
}
