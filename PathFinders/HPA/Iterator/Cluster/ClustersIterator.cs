namespace HPA
{
    struct ClustersIterator
    {
        public IClustersIterateOption option;

        public ClustersIterator(IClustersIterateOption option)
        {
            this.option = option;

            for (int z = 0; z <= AbstractGraph.Clusters.GetUpperBound(2); z++)
            {
                for (int y = 0; y <= AbstractGraph.Clusters.GetUpperBound(1); y++)
                {
                    for (int x = 0; x <= AbstractGraph.Clusters.GetUpperBound(0); x++)
                    {
                        option.Set(x, y, z);
                    }

                }
            }
        }
    }
}
