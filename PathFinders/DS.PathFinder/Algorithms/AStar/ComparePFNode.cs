using System.Collections.Generic;

namespace DS.PathFinder.Algorithms.AStar
{
        internal class ComparePFNode : IComparer<PathNode>
        {
            #region IComparer Members
            public int Compare(PathNode x, PathNode y)
            {
                if (x.F > y.F)
                    return 1;
                else if (x.F < y.F)
                    return -1;
                return 0;
            }
            #endregion
        }
}