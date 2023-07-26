using DS.PathSearch;
using System;
using System.Collections.Generic;

namespace AStarAlgorythm.CLZ
{
    class CLZByDirection : IZonePoints
    {
        readonly Location vector;

        public CLZByDirection(Location vector)
        {
            this.vector = vector;
        }

        public List<Location> Create()
        {
            
            int xs = Math.Abs(AStar.WidthClearanceRCS * vector.X);
            int ys = Math.Abs(AStar.WidthClearanceRCS * vector.Y);
            int zs = Math.Abs(AStar.HeightClearanceRCS * vector.Z);

            int x1 = AStar.WidthClearanceRCS * vector.X - (ys + zs);
            int x2 = AStar.WidthClearanceRCS * vector.X + ys + zs;
            int y1 = AStar.WidthClearanceRCS * vector.Y - (xs + zs);
            int y2 = AStar.WidthClearanceRCS * vector.Y + xs + zs;
            int z1 = AStar.HeightClearanceRCS * vector.Z - (xs + ys);
            int z2 = AStar.HeightClearanceRCS * vector.Z + (xs + ys);

            List<Location> ZonePoints = new List<Location>();

            for (int z = z1; z <= z2; z++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    for (int x = x1; x <= x2; x++)
                    {
                        if (x == 0 && y == 0 && z == 0)
                            continue;

                        Location stepPoint = new Location(x, y, z);
                        ZonePoints.Add(stepPoint);
                    }

                }
            }
            return ZonePoints;
        }

    }
}