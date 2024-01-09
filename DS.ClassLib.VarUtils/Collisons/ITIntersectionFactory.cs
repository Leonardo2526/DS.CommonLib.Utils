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
        /// Get intersections of <paramref name="objectToFindItersections"/> with <typeparamref name="T"/> objects.
        /// </summary>
        /// <param name="objectToFindItersections"></param>
        /// <returns>
        /// List of <typeparamref name="T"/> objects that intersect with <paramref name="objectToFindItersections"/>.
        /// </returns>
        IEnumerable<T> GetIntersections(P objectToFindItersections);
    }
}
