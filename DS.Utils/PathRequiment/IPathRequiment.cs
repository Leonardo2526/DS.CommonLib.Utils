namespace DS.PathSearch.GridMap
{
    public interface IPathRequiment  
    {
        byte Clearance { get; }
        byte MinAngleDistance { get; }
    }
}
