using FrancoGustavo;
using System.Collections.Generic;
using DS.PathSearch.GridMap;

namespace HPA
{
    public static class HPAAlgorythm
    {
        public static void GetPath(IMap Map)
        {

            //Create the copy of map for map with abstract nodes output
            AbstractGraph.MapMatrixL1 = (int[,,])Map.Matrix.Clone();

            int abstractMatrixSize = (Map.Matrix.GetUpperBound(0) + 1) * 10;
            //int abstractMatrixSize = Map.MaxMatrixPoint.X * Map.MaxMatrixPoint.Y;
            AbstractGraph.WeightMatrix =
            new int[abstractMatrixSize, abstractMatrixSize];


            AbstractGraph.IntraPathes = new List<AbstractNode>[abstractMatrixSize, abstractMatrixSize];
             
            ClusterBuilder clusterBuilder = new ClusterBuilder(Map);
            AbstractGraph.Clusters = clusterBuilder.GetClusters();

            StartGoalAdd startGoal = new StartGoalAdd(Map);

            // ClustersIterator clustersIterator =
            //new ClustersIterator(new SetEdgesWeight());
            ClustersIterator clustersIterato1r =
                      new ClustersIterator(new SetInterEdge());
            ClustersIterator clustersIterator2 =
                      new ClustersIterator(new SetIntraEdge());

            AbstractGraph.CutMatrix();
            AbstractGraph.SetNodesList();

            List<PathFinderNode> abstractPath = FGAlgorythm.GetPathByMap(Map, new PathRequiment1());
            //List<PathFinderNode> abstractPath = FGAlgorythm.GetPathByGraph(new Graph1(Map));
            PathConvertor pathConvertor = new PathConvertor();
            List<AbstractNode> convertedPath = pathConvertor.Convert(abstractPath);

            PathRefinement.Refine(convertedPath);

            _ = AbstractGraph.WeightMatrix;
            _ = AbstractGraph.Path;
            _ = AbstractGraph.Clusters;
            _ = AbstractGraph.Nodes;
            _ = AbstractGraph.IntraPathes;

        }
    }
}
