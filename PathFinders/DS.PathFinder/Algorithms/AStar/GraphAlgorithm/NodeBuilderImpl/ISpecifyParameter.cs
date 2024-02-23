using QuickGraph;
using System;
using System.Collections.Generic;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public interface ISpecifyParameter
    {
        ISpecifyParameter WithPath();
        Node WithHeuristic();

    }
}