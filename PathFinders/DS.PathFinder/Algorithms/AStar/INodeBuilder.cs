using Rhino.Geometry;

namespace DS.PathFinder.Algorithms.AStar
{
    /// <summary>
    /// An object that represents builder for <see cref="PathNode"/>.
    /// </summary>
    public interface INodeBuilder
    {
        /// <summary>
        /// Heuristic in percentages.
        /// </summary>
        public int Heuristic { get; set; }

        /// <summary>
        /// Current step.
        /// </summary>
        public double Step { get; set; }

        /// <summary>
        /// Specifies built <see cref="PathNode"/>.
        /// </summary>
        PathNode Node { get; }

        /// <summary>
        /// Build <see cref="PathNode"/> by <paramref name="parentNode"/> with <paramref name="nodeDir"/>.
        /// </summary>
        /// <returns>
        /// <see cref="PathNode"/> with set parameters by <paramref name="parentNode"/> and <paramref name="nodeDir"/>.
        /// </returns>
        PathNode Build(PathNode parentNode, Vector3d nodeDir);


        /// <summary>
        /// Build <see cref="PathNode"/> with some parameters.
        /// </summary>
        /// <returns>
        /// <see cref="PathNode"/> with set parameters.
        /// </returns>
        PathNode BuildWithParameters();

    }
}