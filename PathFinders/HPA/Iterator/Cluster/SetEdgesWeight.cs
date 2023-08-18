namespace HPA
{
    class SetEdgesWeight : IClustersIterateOption
    {
        public void Set(int x, int y, int z)
        {
            //Set inter-edges length to WeightMatrix
            InterEdge interEdge = new InterEdge(AbstractGraph.Clusters, AbstractGraph.Clusters[x, y, z]);
            interEdge.Get();

            //Set intra-edges length to WeightMatrix
            IntraEdge intraEdge = new IntraEdge(AbstractGraph.Clusters[x, y, z]);
            intraEdge.SetWeight();
        }


    }
    struct SetInterEdge : IClustersIterateOption
    {
        public void Set(int x, int y, int z)
        {
            //Set inter-edges length to WeightMatrix
            InterEdge interEdge = new InterEdge(AbstractGraph.Clusters, AbstractGraph.Clusters[x, y, z]);
            interEdge.Get();
        }


    }
    struct SetIntraEdge : IClustersIterateOption
    {
        public void Set(int x, int y, int z)
        {
            //Set intra-edges length to WeightMatrix
            IntraEdge intraEdge = new IntraEdge(AbstractGraph.Clusters[x, y, z]);
            intraEdge.SetWeight();
        }


    }
}
