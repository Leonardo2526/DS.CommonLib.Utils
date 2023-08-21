namespace HPA
{
    struct MatrixIterator
    {
        private readonly Cluster cluster;
        private readonly IMatrixIterateOption option;


        public MatrixIterator(Cluster cluster, IMatrixIterateOption option)
        {
            this.cluster = cluster;
            this.option = option;

            int[,,] matrix = this.cluster.Matrix;

            for (int z = 0; z <= matrix.GetUpperBound(2); z++)
            {
                for (int y = 0; y <= matrix.GetUpperBound(1); y++)
                {
                    for (int x = 0; x <= matrix.GetUpperBound(0); x++)
                    {
                        this.option.Set(x, y, z);
                    }

                }
            }
        }
    }
}
