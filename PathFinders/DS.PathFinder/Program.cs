using System;
using System.Collections.Generic;
using DS.PathSearch.GridMap;

namespace FrancoGustavo
{
    class Program
    {
        static void Main(string[] args) 
        {
            //XY maps
            IMap MapXY20 = new DS.PathSearch.GridMap.d2.MapXY20();
            IMap MapXY20v1 = new DS.PathSearch.GridMap.d2.MapXY20v1();
            IMap MapXY20v3 = new DS.PathSearch.GridMap.d2.MapXY20v3();
            //IMap Map = new DS.PathSearch.GridMap.d2.MapXY10();
            //IMap Map = new DS.PathSearch.GridMap.d2.MapXY1000(); 
            //IMap Map = new DS.PathSearch.GridMap.d2.MapXY1000v1();
            //IMap Map = new DS.PathSearch.GridMap.d3.Map3d100v1();
            //IMap Map = new DS.PathSearch.GridMap.d3.Map3d100();
            //IMap Map = new DS.PathSearch.GridMap.d3.Map3d20();

            //XZ maps
            IMap MapXZ20v1 = new DS.PathSearch.GridMap.d2.MapXZ20v1();
            IMap MapXZ20v3 = new DS.PathSearch.GridMap.d2.MapXZ20v3();
            IMap MapXZ100 = new DS.PathSearch.GridMap.d2.MapXZ100();



            //ResolveMap(MapXY20v3);
            //ResolveMap(MapXY20v1);
            //ResolveMap(MapXY20v3);
            //ResolveMap(MapXZ20v1);
            //ResolveMap(MapXZ20v3);
            //ResolveMap(MapXZ100);

            Console.ReadLine();
        }

        //static void ResolveMap(IMap map)
        //{
        //    List<PathFinderNode> Path = FGAlgorythm.GetPathByMap(map, new PathRequiment02());
        //    ShowResult(map, Path);
        //    ShowPathInfo(Path);
        //}

        static void ShowResult(IMap Map, List<PathFinderNode> path)
        {
            Console.WriteLine(Map.GetType().Name);

            IGridDrawer gridDrawer = new GridDrawer(Map.Matrix, path);
            gridDrawer.DrawSquare();

            Console.WriteLine();
        }

        static void ShowPathInfo(List<PathFinderNode> Path)
        {
            if (Path == null)
                Console.WriteLine("Path Not Found");
            else
                Console.WriteLine("Path length: " + (Path.Count - 1));
            Console.WriteLine("\n");
        }

    }
}
