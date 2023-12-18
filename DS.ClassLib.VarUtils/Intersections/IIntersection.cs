namespace DS.GraphUtils.Entities.Intersections
{
    /// <summary>
    /// Represents intersection between <typeparamref name="TItem"/>s with <typeparamref name="TIntersectionResult"/>.
    /// </summary>
    public interface IIntersection<TItem, TIntersectionResult>
    {
        /// <summary>
        /// Result of intersection betweeen <see cref="Item1"/> and <see cref="Item2"/>.
        /// </summary>
        TIntersectionResult IntersectionResult { get; }

        /// <summary>
        /// First item of intersection.
        /// </summary>
        TItem Item1 { get; }


        /// <summary>
        /// Second item of intersection.
        /// </summary>
        TItem Item2 { get; }

        /// <summary>
        /// Get result of intersection betweeen <see cref="Item1"/> and <see cref="Item2"/>.
        /// </summary>
        TIntersectionResult GetIntersectionResult();
    }

}
