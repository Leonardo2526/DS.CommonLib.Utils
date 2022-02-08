using System;

namespace DSUtils.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 1000x1000 with 1 wall. Start and goal points by map's angles.
    /// </summary>
    public class Map2d1000v1 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 0);
        public Location Goal { get; set; } = new Location(999, 999, 0);
        public int[,,] Matrix { get; set; }

        public Map2d1000v1()
        {
            Matrix = new int[Goal.X + 1, Goal.Y + 1, Goal.Z + 1];

            //Create walls
            int middleY = (int)Math.Round((double)(Matrix.GetUpperBound(1) / 2));
            int smesh = 2;


            for (int z = 0; z <= Matrix.GetUpperBound(2); z++)
            {
                for (int y = 0; y <= middleY + smesh; y++)
                    for (int x = 5; x <= 7; x++)
                        Matrix[x, y, z] = 1;
            }


            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;

        }


    }
}
