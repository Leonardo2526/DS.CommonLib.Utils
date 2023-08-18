using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarAlgorythm.CLZ
{
    class CLZByBorders : IZonePoints
    {
        readonly List<Location> ZonePoints = new List<Location>();


        public List<Location> Create()
        {
            int z=0;
            if (AStar.WidthClearanceRCS == 0)
                return ZonePoints;

            if (AStar.HeightClearanceRCS !=0)
            {
                for (z = -AStar.HeightClearanceRCS; z <= AStar.HeightClearanceRCS; z += 2 * AStar.HeightClearanceRCS)
                {
                    XYFor(z);
                }               
            }
            else
            {
                XYFor(z);
            }
           

            return ZonePoints;
        }

        void AddPoint(int x, int y, int z)
        {
            Location stepPoint = new Location(x, y, z);

            if (!ZonePoints.Contains(stepPoint))
            ZonePoints.Add(stepPoint);
        }

        void XYFor(int z)
        {
            for (int y = -AStar.WidthClearanceRCS; y <= AStar.WidthClearanceRCS; y += 2 * AStar.WidthClearanceRCS)
            {
                for (int x = -AStar.WidthClearanceRCS; x <= AStar.WidthClearanceRCS; x++)
                {
                    AddPoint(x, y, z);
                    AddPoint(y, x, z);
                    AddPoint(z, y, x);
                }
            }
        }
    }
}