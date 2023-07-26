using DS.PathSearch;

namespace AStarAlgorythm

{
    interface IObstaclesCreator
    {
        IWeightedGraph<Location> Create();
    }
}