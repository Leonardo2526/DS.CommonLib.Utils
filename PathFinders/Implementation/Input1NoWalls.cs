using AStarAlgorythm;
using DS.PathSearch;
using System;
using System.Collections.Generic;

namespace Implementation
{
    class Input1NoWalls : IDataInput
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public Location MaxGridPoint { get; set; }
        public List<Location> Unpassablelocations { get; set; } = new List<Location>();

        public Input1NoWalls()
        {
            int goalCoords = 49;
            int maxPointCoords = 50;

            Start = new Location(0, 0, 0);
            Goal = new Location(goalCoords, goalCoords, goalCoords);
            MaxGridPoint = new Location(maxPointCoords, maxPointCoords, maxPointCoords);

            AStar.WidthClearanceRCS = 2;
            AStar.HeightClearanceRCS = 2;

        }


    }
}
