using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;
using DS.ClassLib.VarUtils.Points;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    internal class VertexLocationComparer : IEqualityComparer<NodeVertex>
    {
        private readonly int _tolerance;

        public VertexLocationComparer(int tolerance)
        {
            _tolerance = tolerance;
        }

        public bool Equals(NodeVertex x, NodeVertex y)
            => x.Tag.Location.Round(_tolerance) == y.Tag.Location.Round(_tolerance);

        public int GetHashCode(NodeVertex obj)
            => obj.Tag.Location.GetHashCode();
    }
}
