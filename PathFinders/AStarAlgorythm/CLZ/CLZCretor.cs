using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.PathSearch;

namespace AStarAlgorythm.CLZ
{
    class CLZCretor
    {
        readonly IZonePoints ZonePoints;

        public CLZCretor(IZonePoints zonePoints)
        {
            ZonePoints = zonePoints;
            CLZPoints = new List<Location>();
            CLZPoints.AddRange(ZonePoints.Create());
        }

        public static List<Location> CLZPoints { get; set; }

    }
}
