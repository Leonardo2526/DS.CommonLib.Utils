using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Points
{
    public static class PointsUtils
    {
        public static PointModel GetAverage(List<PointModel> points)
        {
            int x = (int)points.Select(p => p.X).Average();
            int y = (int)points.Select(p => p.Y).Average();
            int z = (int)points.Select(p => p.Z).Average();

            return new PointModel(x, y, z);
        }
    }
   
}
