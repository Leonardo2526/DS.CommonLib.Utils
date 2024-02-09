using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Intersections
{
    /// <summary>
    /// Factory to find intersection with <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIntersectionFactory<T>
    {
        /// <summary>
        /// List of intersections.
        /// </summary>
        List<T> Intersections { get; }
    }

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

    /// <summary>
    /// A factory to find intersection between 
    /// <typeparamref name="TItem1"/> and <typeparamref name="TItem2"/>
    /// as list of <typeparamref name="TResult"/>s.
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TItem2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ITIntersectionFactory<TItem1, TItem2, TResult>
    {
        /// <summary>
        /// List of intersections.
        /// </summary>
        IEnumerable<TResult> Intersections { get; }

        /// <summary>
        /// Get intersections betweeen <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// List of <typeparamref name="TResult"/>s.
        /// </returns>
        IEnumerable<TResult> GetIntersections(TItem1 item1, TItem2 item2);
    }

    /// <summary>
    /// Factory to find intersection as <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITIntersectionFactory<T>
    {
        /// <summary>
        /// List of intersections.
        /// </summary>
        IEnumerable<T> Intersections { get; }

        /// <summary>
        /// Get intersections as <typeparamref name="T"/>s.
        /// </summary>
        /// <returns>
        /// List of <typeparamref name="T"/> intersections.
        /// </returns>
        IEnumerable<T> GetIntersections();
    }
}
