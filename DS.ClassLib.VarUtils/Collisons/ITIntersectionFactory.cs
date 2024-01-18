using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Collisons
{
    /// <summary>
    /// Factory to find intersection of <typeparamref name="P"/> object with <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public interface ITIntersectionFactory<T, P>
    {
        /// <summary>
        /// Get intersections of <paramref name="item"/> with <typeparamref name="T"/>s.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// List of <typeparamref name="T"/>s that intersect with <paramref name="item"/>.
        /// </returns>
        IEnumerable<T> GetIntersections(P item);
    }
}
