using DS.GraphUtils.Entities;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static Rhino.DocObjects.DimensionStyle;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public partial class GraphNodeBuilder
    {
        private class ParametersSpecificator : ISpecifyParameter
        {
            private Node _node;
            private readonly Node _parentNode;
            private readonly Func<double> _heuristicFormula;

            public ParametersSpecificator(Node node, Node parentNode, Func<double> heuristicFormula)
            {
                _node = node;
                _parentNode = parentNode;
                _heuristicFormula = heuristicFormula;
            }

            public ISpecifyParameter WithPath()
            {
                _node.G = _parentNode.G + _parentNode.Location.DistanceTo(_node.Location);
                _node.Path += 1;
                return this;
            }

            public Node WithHeuristic()
            {
                _node.H = _heuristicFormula.Invoke();
                return _node;
            }

        }

    }

}