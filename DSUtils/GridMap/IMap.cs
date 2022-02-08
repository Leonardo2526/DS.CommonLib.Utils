namespace DSUtils.GridMap
{
    public interface IMap
    {
        Location Start { get; set; }
        Location Goal { get; set; }
        int[,,] Matrix { get; set; }
    }
}
