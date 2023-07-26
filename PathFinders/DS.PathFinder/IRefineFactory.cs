using FrancoGustavo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder
{
    /// <summary>
    /// The interface that represents factory to refine path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRefineFactory<T>
    {
        /// <summary>
        /// Restore and refine path from <paramref name="pathNodes"/>.
        /// </summary>
        /// <param name="pathNodes"></param>
        /// <returns>
        /// Path of <typeparamref name="T"/> points.
        /// </returns>
        List<T> Refine(List<PointPathFinderNode> pathNodes);
    }
}
