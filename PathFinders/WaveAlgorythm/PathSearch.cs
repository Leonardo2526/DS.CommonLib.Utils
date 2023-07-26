using System.Collections.Generic;
using System.Linq;
using DS.System;

namespace WaveAlgorythm
{
    class PathSearch
    {
        public static Location Start { get; set; }
        public static Location Goal { get; set; }
        public static Location MaxGridPoint { get; set; }
        public static List<Location> UnpassableLocations { get; set; }

        public PathSearch(Location start, Location goal, Location maxGridPoint, List<Location> unpassablelocations)
        {
            Start = start;
            Goal = goal;
            MaxGridPoint = maxGridPoint;
            UnpassableLocations = unpassablelocations;
        }

        public static List<Location> Path { get; set; } = new List<Location>();

        public int Len { get; set; }
        int x, y, z, d, k;

        public List<Location> GetPath()
        {
            if (!IfPathExists())
            {

                return new List<Location>();
            }

            return Path;
        }


        bool IfPathExists()
        {
            ISpacePointsIterator spacePointsIterator = new IteratorByXYPlane();
            if (!WaveExpander.Expand(spacePointsIterator))
                return false;

            Get();

            if (x == Start.X && y == Start.Y && z == Start.Z)
            {
                return true;
            }

            return false;
        }

        void Get()
        {
            WaveExpander.Grid[WaveExpander.StartStepPoint] = 0;

            // восстановление пути
            // длина кратчайшего пути из (ax, ay) в (bx, By)
            Len = WaveExpander.Grid[WaveExpander.EndStepPoint];

            x = Goal.X;
            y = Goal.Y;
            z = Goal.Z;
            d = Len;

            List<Location> priorityList = new List<Location>()
            {
                new Location(-1, 0, 0),
                new Location(0, 1, 0),
                new Location(1, 0, 0),
                new Location(0, -1, 0)
            };

            while (d >= 0)
            {
                // записываем ячейку (x, y) в путь
                WritePathPoints(x, y, z);

                d--;

                for (k = 0; k < 4; ++k)
                {
                    int ix = x + priorityList[k].X,
                        iy = y + priorityList[k].Y,
                        iz = z + priorityList[k].Z;

                    Location nextPoint = new Location(ix, iy, iz);

                    if (!WaveExpander.Grid.ContainsKey(nextPoint))
                        continue;

                    if (ix >= 0 && ix < MaxGridPoint.X &&
                        iy >= 0 && iy < MaxGridPoint.Y &&
                         WaveExpander.Grid[nextPoint] == d)
                    {

                        // переходим в ячейку, которая на 1 ближе к старту
                        x += priorityList[k].X;
                        y += priorityList[k].Y;

                        break;
                    }
                }
            }
        }


        void WritePathPoints(int x, int y, int z)
        {
            Location point = new Location(x ,y, z);
            Path.Add(point);
        }
    }
}