using Rhino.Geometry;

namespace DS.PathFinder
{
    /// <summary>
    /// The interface that represents factory to create a new path find algorythm.
    /// </summary>
    public interface IAlgorithmBuilder
    {
        /// <summary>
        /// Algorithm to find path.
        /// </summary>
        public IPathFindAlgorithm<Point3d, Point3d> Algorithm { get; }
    }
}
