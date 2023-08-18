using System.Collections.Generic;
using DS.System;

namespace WaveAlgorythm
{
    class WaveExpander
    {
        public static Location StartStepPoint;
        public static Location EndStepPoint;

        public static Dictionary<Location, int> Grid { get; set; }
        private static List<Location> initialPriorityList;

        public static bool Expand(ISpacePointsIterator spacePointsIterator)
        {
            FillData();
            Iterate(spacePointsIterator);

            if (!Grid.ContainsKey(EndStepPoint))
                return false;

            return true;
        }

        static void FillData()
        {

            StartStepPoint = new Location(PathSearch.Start.X, PathSearch.Start.Y, PathSearch.Start.Z);
            EndStepPoint = new Location(PathSearch.Goal.X, PathSearch.Goal.Y, PathSearch.Goal.Z);

            List<Location> stepPointsList = new List<Location>();
            stepPointsList.Add(StartStepPoint);


            initialPriorityList = new List<Location>()
            {
                new Location(-1, 0, 0),
                new Location(0, 1, 0),
                new Location(1, 0, 0),
                new Location(0, -1, 0)
            };
        
        }

        static void Iterate(ISpacePointsIterator iteratorByPlane)
        {
            Grid = new Dictionary<Location, int>
            {
                { StartStepPoint, 1 }
            };
            iteratorByPlane.Iterate();
        }

        public static bool Operation(int x, int y, int z)
        {
            Location currentPoint = new Location(x, y, z);

            int currentD;
            if (!Grid.ContainsKey(currentPoint))
                return false;
            else
                currentD = Grid[currentPoint];

            NeighboursPasser neighboursPasser =
                new NeighboursPasser(currentPoint, initialPriorityList, currentD);
            neighboursPasser.Mark();

            return true;
        }
    }
}
