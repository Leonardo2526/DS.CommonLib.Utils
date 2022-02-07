﻿using System;

namespace DSUtils.GridMap
{
    /// <summary>
    /// 2-dimensional grid map size 100x100 with 2 walls. Start and goal points by map's angles.
    /// </summary>
    public class Map2d100 : IMap
    {
        public Location Start { get; set; } = new Location(0, 0, 0);
        public Location Goal { get; set; } = new Location(99, 99, 0);
        public int[,,] Matrix { get; set; }

        public Map2d100()
        {
            Matrix = new int[Goal.X + 1, Goal.Y + 1, Goal.Z + 1];

            //Create walls
            int middleY = (int)Math.Round((double)(Matrix.GetUpperBound(1) / 2));
            int smesh = 2;

            for (int z = 0; z <= Matrix.GetUpperBound(2); z++)
            {
                for (int y = middleY - smesh; y <= Matrix.GetUpperBound(1); y++)
                    for (int x = 6; x <= 7; x++)
                        Matrix[x, y, z] = 1;
            }

            for (int z = 0; z <= Matrix.GetUpperBound(2); z++)
            {
                for (int y = 0; y <= middleY + smesh; y++)
                    for (int x = 3; x <= 4; x++)
                        Matrix[x, y, z] = 1;
            }


            Matrix[Start.X, Start.Y, Start.Z] = 8;
            Matrix[Goal.X, Goal.Y, Goal.Z] = 9;

        }


    }
}
