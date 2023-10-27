using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An interface that represnt graph
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// <see cref="Line"/>'s that connect <see cref="Vertices"/> pairs.
        /// </summary>
        List<Line> Edges { get; }

        /// <summary>
        /// Nodes.
        /// </summary>
        List<Point3d> Vertices { get; }
    }
}