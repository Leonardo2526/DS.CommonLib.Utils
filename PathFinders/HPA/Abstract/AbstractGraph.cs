using System.Collections.Generic;

namespace HPA
{
    struct AbstractGraph
    {
        public static int ClusterSize { get; } = 10;

        public static Cluster[,,] Clusters { get; set; }
        public static int[,,] MapMatrixL1 { get; set; }

        /// <summary>
        /// Matrix with length of edges between nodes. Nodes ids by columns and rows.
        /// </summary>
        public static int[,] WeightMatrix { get; set; }

        public static List<AbstractNode> Nodes { get; set; } = new List<AbstractNode>();
        public static int NodesCount { get; set; }

        public static List<AbstractNode>[,] IntraPathes { get; set; }
        public static List<AbstractNode> Path { get; set; } = new List<AbstractNode>();

        public static AbstractNode StartNode { get; set; }
        public static AbstractNode GoalNode { get; set; }

        public static Cluster StartNodeCluster { get; set; }
        public static Cluster GoalNodeCluster { get; set; }


        public static void CutMatrix()
        {
            int[,] CorrectedWeightMatrix = new int[NodesCount + 1, NodesCount + 1];
            List<AbstractNode>[,] intraPathes = new List<AbstractNode>[NodesCount + 1, NodesCount + 1];
            for (int y = 0; y <= NodesCount; y++)
            {
                for (int x = 0; x <= NodesCount; x++)
                {
                    CorrectedWeightMatrix[x, y] = WeightMatrix[x, y];
                    intraPathes[x, y] = IntraPathes[x, y];
                }
            }

            WeightMatrix = CorrectedWeightMatrix;
            IntraPathes = intraPathes;
        }

        public static void SetNodesList()
        {
            ClustersIterator clustersIterator =
             new ClustersIterator(new NodeToGraph());

        }

    }
}
