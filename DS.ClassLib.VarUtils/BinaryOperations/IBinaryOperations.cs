namespace DS.ClassLib.VarUtils.BinaryOperations
{
    /// <summary>
    /// The interface is used to perform base binary operations between two sets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBinaryOperations<T>
    {
        /// <summary>
        /// Union of <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The set of all things that are members of 
        /// <paramref name="item1"/> or <paramref name="item2"/> or both.
        /// </returns>
        T Union(T item1, T item2);

        /// <summary>
        /// Intersection of <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The set of all things that are members of both 
        /// <paramref name="item1"/> and <paramref name="item2"/>.
        /// </returns>
        T Intersection(T item1, T item2);

        /// <summary>
        /// Difference between <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The set of all things that belong to 
        /// <paramref name="item1"/> but not <paramref name="item2"/>.
        /// </returns>
        T Difference(T item1, T item2);

        /// <summary>
        /// Symmetric difference between <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The set of all things that belong to 
        /// <paramref name="item1"/> or <paramref name="item2"/> but not both.
        /// </returns>
        T SymmetricDifference(T item1, T item2);
    }
}
