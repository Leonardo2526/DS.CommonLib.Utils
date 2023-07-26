namespace FrancoGustavo.CLZ
{
    class CLZChecker
    {
        private int[,,] MGrid;
        private sbyte[,] CLZPoints;

        public CLZChecker(int[,,] mGrid, sbyte[,] cLZPoints)
        {
            CLZPoints = cLZPoints;
            MGrid = mGrid;
        }

        /// <summary>
        /// Check if clerance zone is available for base node.
        /// Return true if it is availavle and false if not.
        /// </summary>
        /// <param name="baseNode">base node</param>
        /// <returns></returns>
        public bool CheckNode(int baseNodeX, int baseNodeY, int baseNodeZ,
            int gridX, int gridY, int gridZ)
        {
            bool checker = true;

            for (int i = 0; i <= CLZPoints.GetUpperBound(0); i++)
            {
                int mgridX = CLZPoints[i, 0] + baseNodeX;
                int mgridY = CLZPoints[i, 1] + baseNodeY;
                int mgridZ = CLZPoints[i, 2] + baseNodeZ;

                if (mgridX < 0 || mgridY < 0 || mgridZ < 0 || mgridX >= gridX || mgridY >= gridY || mgridZ >= gridZ)
                    continue;

                if (MGrid[mgridX, mgridY, mgridZ] == 1)
                    return false;
            }

            return checker;
        }

    }
}
