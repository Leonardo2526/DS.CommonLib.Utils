namespace DS.ClassLib.VarUtils.Intersections
{
    /// <summary>
    /// A builder for <see cref="IIntersectionFactory{T}"/>
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TItem2"></typeparam>
    public interface IIntersectionFactoryBuilder<TItem1, TItem2>
    {
        /// <summary>
        /// Create intersection factory.
        /// </summary>
        /// <returns>
        /// Factory to get intersections.
        /// </returns>
        ITIntersectionFactory<(TItem1, TItem2)> Create();
    }
}