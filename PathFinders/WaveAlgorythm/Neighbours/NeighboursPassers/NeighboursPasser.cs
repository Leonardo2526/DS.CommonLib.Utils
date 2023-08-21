using System.Collections.Generic;
using DS.System;

namespace WaveAlgorythm
{
    class NeighboursPasser
    {
        public NeighboursPasser(Location currentPoint, List<Location> initialPriorityList,
            int currentD)
        {
            CurrentPoint = currentPoint;
            InitialPriorityList = initialPriorityList;
            CurrentD = currentD;
        }

        public static Location CurrentPoint { get; set; }
        public static List<Location> InitialPriorityList { get; set; }
        public static int CurrentD { get; set; }
        public static int D { get; set; }
        public static int A { get; set; }


        public void Mark()
        {
            int k;
            // проходим по всем непомеченным соседям
            for (k = 0; k < 4; ++k)
            {
                int ix = CurrentPoint.X + InitialPriorityList[k].X,
                    iy = CurrentPoint.Y + InitialPriorityList[k].Y,
                    iz = CurrentPoint.Z + InitialPriorityList[k].Z;

                if (ix >= 0 && ix < PathSearch.MaxGridPoint.X &&
                    iy >= 0 && iy < PathSearch.MaxGridPoint.Y &&
                    iz >= 0 && iz < PathSearch.MaxGridPoint.Z)
                {
                    Location nextPoint = new Location(ix, iy, iz);

                    GridPointChecker gridPointChecker =
                        new GridPointChecker(nextPoint);
                    gridPointChecker.Check();
                }
            }
        }
    }
}
