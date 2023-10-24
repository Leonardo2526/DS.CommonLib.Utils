using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// An object that represents a simple graph.
    /// </summary>
    public class SimpleGraph : IGraph
    {
        private static readonly int _tolerance = 5;
        private List<Point3d> _nodes;
        private List<Line> _links;

        /// <summary>
        /// Instansiate an object that represents a simple graph.
        /// </summary>
        /// <param name="nodes"></param>
        public SimpleGraph(List<Point3d> nodes)
        {
            _nodes = nodes;
        }

        /// <summary>
        /// Instansiate an object that represents graph.
        /// </summary>
        /// <param name="links"></param>
        public SimpleGraph(List<Line> links)
        {
            _links = links;
        }

        /// <inheritdoc/>
        public List<Point3d> Nodes => _nodes ??= _nodes = GetNodes(_links);

        /// <inheritdoc/>
        public List<Line> Links => _links ??= _links = GetLinks(_nodes);

        public override string ToString()
        {
            var sb= new StringBuilder();
            _nodes.ForEach(n => sb.AppendLine($"({n})"));
            return sb.ToString();
        }

        private List<Line> GetLinks(List<Point3d> nodes)
        {
            var links = new List<Line>();

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var link = new Line(nodes[i], nodes[i + 1]);
                links.Add(link);
            }
            return links;
        }

        private List<Point3d> GetNodes(List<Line> links)
        {
            var nodes = new List<Point3d>();
            var ct = Math.Pow(0.1, _tolerance);

            for (int i = 0; i < links.Count; i++)
            {
                var link = links[i];
                var n1 = link.From;
                if (!nodes.Exists(n => n.DistanceTo(n1) < ct))
                { nodes.Add(n1); }

                var n2 = link.To;
                if (!nodes.Exists(n => n.DistanceTo(n2) < ct))
                { nodes.Add(n2); }
            }

            return nodes;
        }
    }
}
