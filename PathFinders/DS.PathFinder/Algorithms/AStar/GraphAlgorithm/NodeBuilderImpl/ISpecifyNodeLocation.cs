using NodeVertex = DS.GraphUtils.Entities.TaggedVertex
    <DS.PathFinder.Algorithms.AStar.GraphAlgorithm.Node>;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public interface ISpecifyNodeLocation
    {
        ISpecifyParameter TryGetLocation(NodeVertex parent);
    }
}