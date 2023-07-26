using AStarAlgorythm;
using DS.PathSearch;
using System;
using System.Collections.Generic;

namespace Implementation
{
    class Input2D : IDataInput
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public Location MaxGridPoint { get; set; }
        public List<Location> Unpassablelocations { get; set; } = new List<Location>();

        public Input2D()
        {
            int goalCoords = 9;
            int maxPointCoords = 10;

            Start = new Location(0, 0, 0);
            Goal = new Location(goalCoords, goalCoords, 0);
            MaxGridPoint = new Location(maxPointCoords, maxPointCoords, 0);

            int z = 0;
            int middleY = (int)Math.Round((double)(MaxGridPoint.Y / 2));
            int smesh = 2;

            for (var x = 3; x < 4; x++)
            {
                for (var y = 0; y < middleY + smesh; y++)
                    Unpassablelocations.Add(new Location(x, y, z));
            }

            for (var x = 7; x < 9; x++)
            {
                for (var y = middleY - smesh; y < MaxGridPoint.Y; y++)
                    Unpassablelocations.Add(new Location(x, y, z));
            }

            AStar.WidthClearanceRCS = 0;
            AStar.HeightClearanceRCS = 0;

        }


    }
}
