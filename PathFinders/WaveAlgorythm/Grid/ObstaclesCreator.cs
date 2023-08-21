using DS.System;

namespace WaveAlgorythm
{
    class ObstaclesCreator : IObstaclesCreator
    {
        public IWeightedGraph<Location> Create()
        {
            var grid = new SquareGrid(PathSearch.MaxGridPoint.X, PathSearch.MaxGridPoint.Y);

            foreach (Location loc in PathSearch.UnpassableLocations)
                grid.walls.Add(new Location(loc.X, loc.Y, loc.Z));

            return grid;
        }
    }
}
