using Rhino.Geometry;
using System.Collections.Generic;
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
        /// Update <see cref="Algorithm"/> with new tolerance.
        /// </summary>
        /// <param name="tolerance"></param>
        void Update(int tolerance);

        /// <summary>
        /// Find path with <see cref="Algorithm"/>.
        /// </summary>
        /// <returns>
        /// Path.
        /// </returns>
        List<Point3d> FindPath();

        //void NextHestimate();
    }
}
