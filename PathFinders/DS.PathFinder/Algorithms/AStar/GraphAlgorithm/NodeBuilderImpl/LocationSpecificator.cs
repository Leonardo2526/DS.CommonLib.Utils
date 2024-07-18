using System;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public partial class GraphNodeBuilder
    {
        private class LocationSpecificator : ISpecifyNodeLocation
        {
            private readonly INodeVectoryFactory _nodeVectorFactory;
            private readonly Func<double> _heuristicFormula;

            public LocationSpecificator(INodeVectoryFactory nodeVectorFactory, Func<double> heuristicFormula)
            {
                _nodeVectorFactory = nodeVectorFactory;
                _heuristicFormula = heuristicFormula;
            }

            public ISpecifyParameter TryGetLocation(NodeVertex parent)
            {
                var parentNode = parent.Tag;
                while (_nodeVectorFactory.MoveNext())
                {
                    var location = _nodeVectorFactory.Current + parentNode.Location;
                    var node = new Node(location);
                    return new ParametersSpecificator(node, parentNode, _heuristicFormula);
                }

                return null;
            }
        }

    }

}