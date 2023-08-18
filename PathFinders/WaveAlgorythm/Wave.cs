using System;
using System.Collections.Generic;
using DS.System;

namespace WaveAlgorythm
{
    public class Wave 
    {
        public static Location Start { get; set; } 
        public static Location Goal { get; set; }
        public static Location MaxGridPoint { get; set; }
        public static List<Location> Unpassablelocations { get; set; }

        public Wave(Location start, Location goal, Location maxGridPoint, List<Location> unpassablelocations)
        {
            Start = start;
            Goal = goal;
            MaxGridPoint = maxGridPoint;
            Unpassablelocations = unpassablelocations;
        }

        public List<Location> Path { get; set; } 

         

        public List<Location> GetPath()
        {
            PathSearch pathSearch = new PathSearch(Start, Goal, MaxGridPoint, Unpassablelocations);
            Path = pathSearch.GetPath();

            return Path;
        }


        public List<Location> GetPathWithVisualizition()
        {

            GetPath();

            var unpass = new ObstaclesCreator().Create();

            Console.WriteLine("WaveAlgorythm: \n");
            IGridDrawer gridDrawer = new GridDrawer(Start, Goal);
            gridDrawer.DrawSquare(unpass as SquareGrid);

            return Path;
        }
    }
}
