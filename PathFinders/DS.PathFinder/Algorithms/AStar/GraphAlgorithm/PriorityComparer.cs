using DS.PathFinder.Algorithms.AStar.GraphAlgorithm;
using System.Collections.Generic;
using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;

namespace DS.PathFinder.Algorithms.AStar
{
        internal class PriorityComparer : IComparer<NodeVertex>
        {
            #region IComparer Members
            public int Compare(NodeVertex x, NodeVertex y)
            {
                if (x.Tag.F > y.Tag.F)
                    return 1;
                else if (x.Tag.F < y.Tag.F)
                    return -1;
                return 0;
            }
            #endregion
        }
}