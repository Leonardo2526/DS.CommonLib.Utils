using Rhino.Geometry;
using System.Threading;

namespace DS.PathFinder
{
    /// <summary>
    /// The interface that represents factory to create a new path find algorythm.
    /// </summary>
    public interface IAlgorithmFactory
    { 
        /// <summary>
        /// Created algorithm.
        /// </summary>
        public IPathFindAlgorithm<Point3d> Algorithm { get; }

        /// <summary>
        /// Build algorithm with <paramref name="step"/>.
        /// </summary>
        /// <param name="step"></param>
        void Reset(double step);
    }
}
