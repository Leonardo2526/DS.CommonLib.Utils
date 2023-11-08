using Castle.Core.Internal;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.Points;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Compute shortest paths  for given vertices.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="edgeCost"></param>
        /// <returns>
        /// Path edges if path was found.
        /// <para>
        /// Otherwise <see langword="null"/>.
        /// </para>
        /// </returns>
        public static IEnumerable<Edge<IVertex>> GetPath(this IVertexAndEdgeListGraph<IVertex, Edge<IVertex>> graph, 
            IVertex source, IVertex target, Func<Edge<IVertex>, double> edgeCost = null)
        {           
            edgeCost ??= edge => 1;
            var tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, source);

            return tryGetPaths(target, out IEnumerable<Edge<IVertex>> path) is true ? 
                path : 
                null;
        }

        /// <summary>
        /// Splilt <paramref name="graph"/> vertices by root branches.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="rootVertex"></param>
        /// <returns>
        /// Vertices splitted by each branch from <paramref name="rootVertex"/>.
        /// <para>
        /// If <paramref name="rootVertex"/> is not specified split each root of <paramref name="graph"/>.
        /// </para>
        /// </returns>
        public static Dictionary<IVertex, IEnumerable<IVertex>> SplitRootBranches(this AdjacencyGraph<IVertex, Edge<IVertex>> graph, 
            IVertex rootVertex = null)
        {
            var firstLevelBranches = new Dictionary<IVertex, IEnumerable<IVertex>>();

            var clonedGraph = graph.Clone();
            var initialRoots = clonedGraph.Roots().ToList();

            var rootsToRemove = rootVertex is null ? initialRoots : new List<IVertex>() { rootVertex };
            rootsToRemove.ForEach(r => clonedGraph.RemoveVertex(r));
            var rootsToSplit = clonedGraph.Roots().Where(r => !initialRoots.Contains(r)).ToList();

            var bfsClone = new BreadthFirstSearchAlgorithm<IVertex, Edge<IVertex>>(clonedGraph);
            foreach (IVertex root in rootsToSplit)
            {
                bfsClone.SetRootVertex(root);
                bfsClone.Compute();
                bfsClone.ClearRootVertex();
                var blackVertices = bfsClone.VertexColors.
                    Where(v => v.Value == GraphColor.Black).
                    Select(v => v.Key);
                var children = new List<IVertex>();
                children.AddRange(blackVertices);
                firstLevelBranches.Add(root, children);
            }

           return firstLevelBranches;
        }
    }
}
