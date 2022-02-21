using System;

namespace DS.PathSearch.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 20x20 with 1 wall. Start and goal points by map's center.
    /// </summary>
    public class MapXZ20v1 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 9);
        public Location Goal { get; set; } = new Location(19, 0, 9);
        public int[,,] Matrix { get; set; }

        public MapXZ20v1()
        {
            Matrix = new int[20, 1, 20]; 
          
            for (int y = 0; y <= Matrix.GetUpperBound(1); y++)
            {
                for (int z = 7; z <= 15; z++)
                    for (int x = 7; x <= 10; x++)
                        Matrix[x, y, z] = 1;
            }
            
            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;

        }


    }
}
