namespace DS.PathSearch.GridMap
{
    public class PathRequiment02 : IPathRequiment
    {
        public byte Clearance { get; } = 0;
        public byte MinAngleDistance { get; } = 2;
    }
}
