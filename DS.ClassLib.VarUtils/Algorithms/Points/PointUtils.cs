using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Algorithms.Points
{
    public static class PointExtenstions
    {
        public static double DistanceTo(this PointModel basePoint, PointModel point)
        {
            double xDist = Math.Pow((basePoint.X - point.X) ,2);
            double yDist = Math.Pow((basePoint.Y - point.Y) ,2);
            double zDist = Math.Pow((basePoint.Z - point.Z) ,2);

            return Math.Sqrt(xDist + yDist + zDist);
        }
    }
}
