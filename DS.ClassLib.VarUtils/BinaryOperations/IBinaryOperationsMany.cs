using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.BinaryOperations
{
    /// <summary>
    /// The interface is used to perform base binary operations between 
    /// <typeparamref name="T"/> and collection of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public interface IBinaryOperationsMany<T, R>
    {
        /// <summary>
        /// Union of <paramref name="item1"/> and <paramref name="items2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="items2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that is member of 
        /// <paramref name="item1"/> or <paramref name="items2"/> or both.
        /// </returns>
        R Union(T item1, IEnumerable<T> items2);

        /// <summary>
        /// Intersection of <paramref name="item1"/> and <paramref name="items2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="items2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that is member of both 
        /// <paramref name="item1"/> and <paramref name="items2"/>.
        /// </returns>
        R Intersection(T item1, IEnumerable<T> items2);

        /// <summary>
        /// Difference between <paramref name="item1"/> and <paramref name="items2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="items2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that belong to 
        /// <paramref name="item1"/> but not <paramref name="items2"/>.
        /// </returns>
        R Difference(T item1, IEnumerable<T> items2);

        /// <summary>
        /// Symmetric difference between <paramref name="item1"/> and <paramref name="items2"/>.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="items2"></param>
        /// <returns>
        /// The <typeparamref name="R"/> that belong to 
        /// <paramref name="item1"/> or <paramref name="items2"/> but not both.
        /// </returns>
        R SymmetricDifference(T item1, IEnumerable<T> items2);
    }

    /// <inheritdoc/>
    public interface IBinaryOperationsMany<T> :
        IBinaryOperationsMany<T, T>
    { }

    /// <inheritdoc/>
    public interface IBinaryOperationsManyCollection<T> :
        IBinaryOperationsMany<T, IEnumerable<T>>
    { }

}
