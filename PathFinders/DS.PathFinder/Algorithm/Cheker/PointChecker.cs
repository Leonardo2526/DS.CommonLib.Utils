using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrancoGustavo
{
    static class PointChecker
    {
        /// <summary>
        ///Check if first angle is start point
        /// </summary>
        /// <param name="start"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static bool IsAngleStartPoint(Location start, PathFinderNode parentNode)
        {
            if (start.X == parentNode.ANX &&
            start.Y == parentNode.ANY &&
            start.Z == parentNode.ANZ)
                return true;

            return false;

        }
    }
}
