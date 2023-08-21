using System;
using System.Collections.Generic;
using DS.PathSearch;

namespace AStarAlgorythm
{
    public class AStar
    {
        public static Location Start { get; set; }  
        public static Location Goal { get; set; }  
        public static Location MaxGridPoint { get; set; }
        public static List<Location> Unpassablelocations { get; set; }

        /// <summary>
        /// Clerance between elements in reference coordinate system by width
        /// </summary>
        public static int WidthClearanceRCS { get; set; } = 0;

        /// <summary>
        /// Clerance between elements in reference coordinate system by height
        /// </summary>
        public static int HeightClearanceRCS { get; set; } = 0;

        public AStar(Location start, Location goal, Location maxGridPoint, List<Location> unpassablelocations)
        {
            Start = start;
            Goal = goal;
            MaxGridPoint = maxGridPoint;
            Unpassablelocations = unpassablelocations;
        }

        public List<Location> Path { get; set; } 

        private PathSearch pathSearch; 
        private IWeightedGraph<Location> unpass; 

        public List<Location> GetPath() 
        {
            //Create unpassable locations
            unpass = new ObstaclesCreator().Create();

            // Выполнение A*
            pathSearch = new PathSearch(unpass, Start, Goal);
            return Path = pathSearch.Path; 
        }

        public List<Location> GetPathWithVisualizition()
        {
            GetPath();

            Console.WriteLine("AStarAlgorythm: \n");

            IGridDrawer gridDrawer = new GridDrawer(pathSearch, Start, Goal);
            gridDrawer.DrawSquare(unpass as SquareGrid);

            Console.WriteLine("\n");

            return Path;
        }
    }
}
