using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.BinaryOperations
{
    /// <summary>
    /// The interface is used to perform base binary operations between 
    /// two <typeparamref name="T"/> and return <typeparamref name="R"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public interface IBinaryOperations<T, R>
    {
        /// <summary>
        /// Union of <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that is member of 
        /// <paramref name="item1"/> or <paramref name="item2"/> or both.
        /// </returns>
        R Union(T item1, T item2);

        /// <summary>
        /// Intersection of <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that is member of both 
        /// <paramref name="item1"/> and <paramref name="item2"/>.
        /// </returns>
        R Intersection(T item1, T item2);

        /// <summary>
        /// Difference between <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that belong to 
        /// <paramref name="item1"/> but not <paramref name="item2"/>.
        /// </returns>
        R Difference(T item1, T item2);

        /// <summary>
        /// Symmetric difference between <paramref name="item1"/> and <paramref name="item2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that belong to 
        /// <paramref name="item1"/> or <paramref name="item2"/> but not both.
        /// </returns>
        R SymmetricDifference(T item1, T item2);
    }

    /// <inheritdoc/>
    public interface IBinaryOperations<T> : 
        IBinaryOperations<T, T>
    { }

    /// <inheritdoc/>
    public interface IBinaryOperationsCollection<T> : 
        IBinaryOperations<T, IEnumerable<T>>
    { }

}
