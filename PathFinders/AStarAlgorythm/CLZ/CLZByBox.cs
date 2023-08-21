using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.PathSearch;

namespace AStarAlgorythm.CLZ
{
    class CLZByBox : IZonePoints
    {
   

        public List<Location> Create()
        {
            List<Location> ZonePoints = new List<Location>();

            for (int z = -AStar.HeightClearanceRCS; z <= AStar.HeightClearanceRCS; z++)
            {
                for (int y = -AStar.WidthClearanceRCS; y <= AStar.WidthClearanceRCS; y++)
                {
                    for (int x = -AStar.WidthClearanceRCS; x <= AStar.WidthClearanceRCS; x++)
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