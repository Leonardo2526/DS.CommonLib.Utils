using DS.PathSearch;
using System.Collections.Generic;


namespace AStarAlgorythm
{
    public interface IWeightedGraph<L>
    {
        int Cost(Location a, Location b);
        IEnumerable<Location> Neighbors(Location id);
    }
}
