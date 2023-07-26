using AStarAlgorythm;
using DS.PathSearch;
using System;
using System.Collections.Generic;

namespace Implementation
{
    class ModelPlaneInput1 : IDataInput
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public Location MaxGridPoint { get; set; }
        public List<Location> Unpassablelocations { get; set; } = new List<Location>();

        public ModelPlaneInput1()
        {
            Start = new Location(4, 20, 20);
            Goal = new Location(41, 20, 20);
            MaxGridPoint = new Location(44, 40, 40);

            for (var z = 0; z < MaxGridPoint.Z; z++)
            {
                for (var x = 15; x < 20; x++)
                {
                    for (var y = 10; y < 20; y++)
                        Unpassablelocations.Add(new Location(x, y, z));

                }
            }

            AStar.WidthClearanceRCS = 4;
            AStar.HeightClearanceRCS = 4;

        }


    }
}
