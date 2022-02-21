using System;

namespace DS.PathSearch.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 10x10 with 2 walls. Start and goal points by map's angles.
    /// </summary>
    public class MapXZ10 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 0);
        public Location Goal { get; set; } = new Location(9, 0, 9);
        public int[,,] Matrix { get; set; }

        public MapXZ10()
        {
            Matrix = new int[Goal.X + 1, Goal.Y + 1, Goal.Z + 1];

            //Create walls
            int middleZ = (int)Math.Round((double)(Matrix.GetUpperBound(2) / 2));
            int smesh = 2;

            for (int y = 0; y <= Matrix.GetUpperBound(1); y++)
            {
                for (int z = middleZ - smesh; z <= Matrix.GetUpperBound(2); z++)
                    for (int x = 6; x <= 7; x++)
                        Matrix[x, y, z] = 1;
            }
         

            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;
        }
    }
}
