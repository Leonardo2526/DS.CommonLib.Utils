namespace DS.PathSearch.GridMap
{
    public interface IPathRequiment  
    {
        byte Clearance { get; }
        byte MinAngleDistance { get; }
    }

    public interface IDoublePathRequiment
    {
        double Clearance { get; }
        double MinAngleDistance { get; }
    }
}
