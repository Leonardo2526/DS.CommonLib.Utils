using System;

namespace DS.PathSearch.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 20x20 with both sides stepped walls. Start and goal points by map's center.
    /// </summary>
    public class MapXZ20v3 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 9);
        public Location Goal { get; set; } = new Location(19, 0, 9);
        public int[,,] Matrix { get; set; }

        public MapXZ20v3()
        {
            Matrix = new int[20, 1, 20];       
          
            for (int y = 0; y <= Matrix.GetUpperBound(1); y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    for (int x = 5; x < 6; x++)
                        Matrix[x, y, z] = 1;
                    for (int x = 9; x < 10; x++)
                        Matrix[x, y, z] = 1;
                }
                   

                for (int z = 0; z < 11; z++)
                {
                    for (int x = 6; x < 7; x++)
                        Matrix[x, y, z] = 1;
                    for (int x = 8; x < 9; x++)
                        Matrix[x, y, z] = 1;
                }
                   

                for (int z = 0; z < 14; z++)
                    for (int x = 7; x < 8; x++)
                        Matrix[x, y, z] = 1;
            }
            
            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;

        }


    }
}
