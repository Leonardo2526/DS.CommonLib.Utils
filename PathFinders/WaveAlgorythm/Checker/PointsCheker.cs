using System;
using System.Collections.Generic;
using DS.System;

namespace WaveAlgorythm
{
    class PointsCheker
    {
        public bool IsPointPassable(Location point)
        {
            if (PathSearch.UnpassableLocations.Count == 0)
                return true;

            if (PathSearch.UnpassableLocations.Contains(point))
                return false;

            return true;
        }
    }
}
