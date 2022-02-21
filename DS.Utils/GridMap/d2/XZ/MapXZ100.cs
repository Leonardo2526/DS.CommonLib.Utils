using System;

namespace DS.PathSearch.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 100x100 with both sides stepped walls. Start and goal points by map's center.
    /// </summary>
    public class MapXZ100 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 49);
        public Location Goal { get; set; } = new Location(99, 0, 49);
        public int[,,] Matrix { get; set; }

        public MapXZ100()
        {
            Matrix = new int[100, 1, 100];

            //Create walls
            int middleZ = (int)Math.Round((double)(Matrix.GetUpperBound(2) / 2));
            int smesh = 2;

            for (int y = 0; y <= Matrix.GetUpperBound(1); y++)
            {
                for (int z = middleZ - smesh; z <= Matrix.GetUpperBound(2); z++)
                {
                    for (int x = 40; x <= 50; x++)
                        Matrix[x, y, z] = 1;
                }
                for (int z = 0; z <= middleZ + 3*smesh; z++)
                {
                    for (int x = 60; x <= 70; x++)
                        Matrix[x, y, z] = 1;
                }

            }


            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;
        }


    }
}
