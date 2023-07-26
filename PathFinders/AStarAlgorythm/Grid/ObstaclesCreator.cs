using AStarAlgorythm.CLZ;
using DS.PathSearch;

namespace AStarAlgorythm

{
    class ObstaclesCreator : IObstaclesCreator
    {
        public IWeightedGraph<Location> Create()
        {
            var grid = new SquareGrid(AStar.MaxGridPoint.X, AStar.MaxGridPoint.Y);
            //var grid = new Grid3D(AStar.MaxGridPoint.X, AStar.MaxGridPoint.Y, AStar.MaxGridPoint.Z);

            foreach (Location loc in AStar.Unpassablelocations)
                grid.walls.Add(new Location(loc.X, loc.Y, loc.Z));

            //CLZCretor cLZCretor = new CLZCretor(new CLZByBorders());

            return grid; 
        }
    }
}
