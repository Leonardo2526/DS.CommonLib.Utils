namespace DSUtils.GridMap
{
    public interface IGraph
    {
        Location Start { get; set; }
        Location Goal { get; set; }
        int[,] WeightMatrix { get; set; }
        int[,,] Matrix { get; set; }
    }
}
