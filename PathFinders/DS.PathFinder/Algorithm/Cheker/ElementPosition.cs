using DS.PathSearch;
using System.Collections.Generic;

namespace FrancoGustavo
{
    static class ElementPosition
    {
        private static sbyte[,] XYPlane = new sbyte[4, 3] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 1, 0 }, { -1, 0, 0 } };
        private static sbyte[,] XZPlane = new sbyte[4, 3] { { 1, 0, 0 }, { -1, 0, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };
        private static sbyte[,] YZPlane = new sbyte[4, 3] { { 0, -1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };
        private static sbyte[,] XYZPlane = new sbyte[6, 3] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 1, 0 }, { -1, 0, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };


        public static List<sbyte[,]> GetPlane(Location start, Location end)

        {
            List<sbyte[,]> searchPlanes = new List<sbyte[,]>();

            if (start.X == end.X && start.Z == end.Z)
            {
                searchPlanes.Add(YZPlane);
                searchPlanes.Add(XYPlane);
                //searchPlanes.Add(XYZPlane);
            }
            else if (start.Y == end.Y && start.Z == end.Z)
            {
                searchPlanes.Add(XZPlane);
                searchPlanes.Add(XYPlane);
                //searchPlanes.Add(XYZPlane);
            }
            else if (start.Y == end.Y && start.X == end.X)
            {
                searchPlanes.Add(YZPlane);
                searchPlanes.Add(XZPlane);
                //searchPlanes.Add(XYZPlane);
            }
            else
                searchPlanes.Add(XYZPlane);

            return searchPlanes;
        }
    }
}
