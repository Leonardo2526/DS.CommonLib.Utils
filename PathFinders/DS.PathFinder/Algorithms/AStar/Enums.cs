namespace DS.PathFinder.Algorithms.AStar
{
    /// <summary>
    /// Type of path node.
    /// </summary>
    public enum PathNodeType
    {
        /// <summary>
        /// 
        /// </summary>
        Start = 1,

        /// <summary>
        /// 
        /// </summary>
        End = 2,

        /// <summary>
        /// 
        /// </summary>
        Open = 3,

        /// <summary>
        /// 
        /// </summary>
        Close = 4,

        /// <summary>
        /// 
        /// </summary>
        Current = 5,

        /// <summary>
        /// 
        /// </summary>
        Path = 6,

        /// <summary>
        /// 
        /// </summary>
        Unpassable = 7
    }

    /// <summary>
    /// Formula used to calculate heuristic.
    /// </summary>
    public enum HeuristicFormula
    {
        /// <summary>
        /// 
        /// </summary>
        Manhattan = 1,
        /// <summary>
        /// 
        /// </summary>
        MaxDXDY = 2,

        /// <summary>
        /// 
        /// </summary>
        DiagonalShortCut = 3,

        /// <summary>
        /// 
        /// </summary>
        Euclidean = 4,
        /// <summary>
        /// 
        /// </summary>
        EuclideanNoSQR = 5
    }
}