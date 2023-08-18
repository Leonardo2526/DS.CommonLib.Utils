using System;

namespace HPA
{
    class ClustersDrawer
    {
        public IMatrixDrawer gridDrawer;

        public ClustersDrawer(IMatrixDrawer gridDrawer)
        {
            this.gridDrawer = gridDrawer;

            ClustersIterator clustersIterator = 
                new ClustersIterator(new DrawOption(gridDrawer));


        }
    }
}
