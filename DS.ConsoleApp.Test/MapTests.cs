using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.GridMap.d2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ConsoleApp.Test
{
    internal static class MapTests
    {
        public static void RunTest1()
        {
            var minPoint = new Point3D(0,0,0);
            var maxPoint = new Point3D(9,9,0);
            var stepVector = new Vector3D(1,1,1);

            var map = new IntMap(minPoint, maxPoint, stepVector);
            Console.WriteLine("Map size is: " + map.Matrix.Length +"\n");

            Console.WriteLine("Original map:");
            map.Show();

            var startPoint = new Point3D(0,0,0);
            var endPoint = new Point3D(9,9,0);
            var path = new List<Point3D>() { startPoint, new Point3D(1,0,0), new Point3D(2,0,0), endPoint };
            var upassiblePoints = new List<Point3D>() { new Point3D(5,8,0) };
            map.Fill(startPoint, endPoint, path, upassiblePoints);

            Console.WriteLine("\nPath map:");
            map.Show();
        }
    }
}
