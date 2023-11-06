using Castle.Core.Internal;
using DS.ClassLib.VarUtils.Points;
using QuickGraph;
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

            if (graph.Vertices.Count < 3) { return planes; }

            var at = 3.DegToRad();
            var origin = graph.Vertices[0];
            var xLink = new Line(origin, graph.Vertices[1]);
            var xDirection = xLink.Direction;

            for (int i = 1; i < graph.Vertices.Count; i++)
            {
                var n = graph.Vertices[i];
                var yDirection = new Line(origin, n).Direction;
                if (xDirection.IsParallelTo(yDirection, at) == 0)
                {
                    var plane = new Plane(origin, xDirection, yDirection);
                    if (plane.IsValid)
                    {
                        if (planes.Count == 0 || planes.Exists(p => p.Normal.IsParallelTo(plane.Normal, at) == 0))
                        { planes.Add(plane); }
                    }
                }
            }

            return planes;
        }

        /// <summary>
        /// Specifies if all <paramref name="graph"/>'s nodes are at same <see cref="Plane"/>.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="plane"></param>
        /// <returns>
        /// <see langword="true"/> if all <paramref name="graph"/>'s nodes are at the same <see cref="Plane"/>.
        /// <para>
        /// Otherwise returns <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsPlane(this IGraph graph, out Plane plane)
        {
            var planes = graph.GetPlanes();
            plane = planes.FirstOrDefault();
            return planes.Count == 0 || planes.Count == 1;
        }

        /// <summary>
        /// Show <paramref name="graph"/> with specified <paramref name="visualisator"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph"></param>
        /// <param name="visualisator"></param>
        public static void Show<TVertex>(this AdjacencyGraph<TVertex, Edge<TVertex>> graph, 
            IAdjacencyGraphVisulisator<TVertex> visualisator)
        {
            visualisator.Build(graph).Show();
        }
    }
}
