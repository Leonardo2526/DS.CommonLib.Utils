using System;

namespace DSUtils.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 20x20 with both sides stepped walls. Start and goal points by map's center.
    /// </summary>
    public class Map2d20v3 : IMap
    {
        public Location Start { get; set; } = new Location(0, 9, 0);
        public Location Goal { get; set; } = new Location(19, 9, 0);
        public int[,,] Matrix { get; set; }

        public Map2d20v3()
        {
            Matrix = new int[20, 20, 1];
       
            //Create walls
            int middleY = (int)Math.Round((double)(Matrix.GetUpperBound(1) / 2));
            int smesh = 2;

            for (int z = 0; z <= Matrix.GetUpperBound(2); z++)
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 5; x < 6; x++)
                        Matrix[x, y, z] = 1;
                    for (int x = 9; x < 10; x++)
                        Matrix[x, y, z] = 1;
                }
                   

                for (int y = 0; y < 11; y++)
                {
                    for (int x = 6; x < 7; x++)
                        Matrix[x, y, z] = 1;
                    for (int x = 8; x < 9; x++)
                        Matrix[x, y, z] = 1;
                }
                   

                for (int y = 0; y < 14; y++)
                    for (int x = 7; x < 8; x++)
                        Matrix[x, y, z] = 1;
            }
            
            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;

        }


    }
}
