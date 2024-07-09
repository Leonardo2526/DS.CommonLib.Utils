using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Selectors
{
    /// <summary>
    /// The interface is used to select one or many <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiSelector<T> : ISelector<T>
    {
        /// <summary>
        /// Select many <typeparamref name="T"/> objects.
        /// </summary>
        /// <returns>
        /// A collection of selected <typeparamref name="T"/> objects.
        /// </returns>
       IEnumerable<T> SelectMany();

        /// <summary>
        /// Select all existing <typeparamref name="T"/> objects.
        /// </summary>
        /// <returns>
        /// A collection of all existing <typeparamref name="T"/> objects.
        /// </returns>
        IEnumerable<T> SelectAll();
    }
}
