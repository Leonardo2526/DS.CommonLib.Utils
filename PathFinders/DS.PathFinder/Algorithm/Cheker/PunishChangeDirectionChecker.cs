using DS.PathSearch;
using FrancoGustavo.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrancoGustavo
{
    static class PunishChangeDirectionChecker 
    {
        public static int GetNewG(int newNodeX, int newNodeY, int newNodeZ, PathFinderNode parentNode, Location start, int NodeG)
        {
            if (PointChecker.IsAngleStartPoint(start, parentNode))
                return NodeG;

            //Check if direction was changed 
            int k = 0;
            if ((newNodeX - parentNode.PX) != 0)
                k++;
            if ((newNodeY - parentNode.PY) != 0)
                k++;
            if ((newNodeZ - parentNode.PZ) != 0)
                k++;

            if (k >= 2)
            {
                NodeG += 5;

                //Check distance from parent node to angle
                //int maxPointsToAngle = 8;
                //int dist = Math.Abs(parentNode.X - parentNode.ANX) +
                //  Math.Abs(parentNode.Y - parentNode.ANY) +
                //  Math.Abs(parentNode.Z - parentNode.ANZ);

                //if (dist < maxPointsToAngle)
                //{
                //    //Hard punish
                //    NodeG += 30;

                //}
                //else
                //{
                //    NodeG += 5;
                //}
            }

            return NodeG;
        }
    }
}
