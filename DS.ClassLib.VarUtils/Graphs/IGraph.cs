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
        /// Links.
        /// </summary>
        List<Line> Links { get; }

        /// <summary>
        /// Nodes.
        /// </summary>
        List<Point3d> Nodes { get; }
    }
}