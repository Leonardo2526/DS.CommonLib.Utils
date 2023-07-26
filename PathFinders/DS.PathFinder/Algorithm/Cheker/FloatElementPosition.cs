using DS.ClassLib.VarUtils;
using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace FrancoGustavo
{
    static class FloatElementPosition
    {
        private static float _tan30 = (float)Math.Sqrt(3);
        private static double _30rad = 30.DegToRad();
       
        private static float _sin30 = (float)Math.Sin(_30rad);
        private static float _cos30 = (float)Math.Cos(_30rad);

        private static double _60rad = 60.DegToRad();
        private static float _sin60 = (float)Math.Sin(_60rad);
        private static float _cos60 = (float)Math.Cos(_60rad);


        private static float[,] XYPlane = new float[4, 3] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 1, 0 }, { -1, 0, 0 } };
        private static float[,] XZPlane = new float[4, 3] { { 1, 0, 0 }, { -1, 0, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };
        //private static float[,] YZPlane = new float[6, 3] {
        //    { 0, -1, 0 }, { 0, 1, 0 }, 
        //    //{ 0, 0, -1 }, { 0, 0, 1 },
        //    { 0, -1, 1 },{ 0, -1, -1 }, { 0, 1, -1 }, { 0, 1, 1 },
        //};

        //private static sbyte[,] YZPlane = new sbyte[8, 3] {
        //    { 0, -1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 },
        //    { 0, -1, 1 },{ 0, -1, -1 }, { 0, 1, -1 }, { 0, 1, 1 },
        //};

        private static float[,] YZPlane = new float[6, 3]
     {
               { 0, -1, 0 }, { 0, 1, 0 }, 
               //{ 0, 0, -1 }, { 0, 0, 1 },
                {0, _sin30, _cos30}, {0, _sin30, - _cos30 },{0, -_sin30, _cos30 }, {0, -_sin30, -_cos30},
                //{ 0,_sin60, _cos60}, {0, _sin60, -_cos60},{0, -_sin60, _cos60 }, {0, -_sin60, -_cos60 },
     };

        //private static float[,] YZPlane = new float[6, 3] {
        //    { 0, -1, 1 },{ 0, -1, -1 }, { 0, 1, -1 }, { 0, 1, 1 },
        //    { 0, -1, 0 }, { 0, 1, 0 }, 
        //    //{ 0, 0, -1 }, { 0, 0, 1 }
        //};
        private static float[,] XYZPlane = new float[6, 3] { { 0, -1, 0 }, { 1, 0, 0 }, { 0, 1, 0 }, { -1, 0, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };


        public static List<float[,]> GetPlane(Point3D start, Point3D end)

        {
            var searchPlanes = new List<float[,]>();

            if (start.X == end.X && start.Z == end.Z)
            {
                searchPlanes.Add(YZPlane);
                //searchPlanes.Add(XYPlane);
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
