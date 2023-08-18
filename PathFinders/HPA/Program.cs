using System;
using DS.PathSearch;
using DS.PathSearch.GridMap;


namespace HPA
{
    class Program
    {
        static void Main(string[] args)
        {
            IMap map = new DS.PathSearch.GridMap.d2.MapXY100();
            HPAAlgorythm.GetPath(map); 
             
            Print(map);

            if (AbstractGraph.Path == null)
                Console.WriteLine("Path Not Found");
            else
                Console.WriteLine("Path length: " + (AbstractGraph.Path.Count - 1));

            Console.ReadLine();
        }

        private static void Print(IMap map) 
        {
            IMatrixDrawer matrixDrawer;

            Console.WriteLine("Initail abstract Map:");
            matrixDrawer = new AbstractMapDrawer(AbstractGraph.MapMatrixL1);
            matrixDrawer.Draw();
            Console.WriteLine("AbstractGraph nodes count:" + AbstractGraph.NodesCount + "\n");

            Console.WriteLine("Map with Path:");
            matrixDrawer = new MatrixDrawer(map.Matrix);
            matrixDrawer.Draw();

            ClustersDrawer clustersDrawer = new ClustersDrawer(matrixDrawer);

            Console.WriteLine("\n");
            Console.WriteLine("Clusters count: " + AbstractGraph.Clusters.Length);

        }
    }
}
