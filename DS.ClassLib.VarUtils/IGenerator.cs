using System.Collections.Generic;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface is used to generate <typeparamref name="T"/> items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<T>
    {
        /// <summary>
        /// Generate a collection of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        /// Generate result.
        /// </returns>
        IEnumerable<T> Generate();
    }
}
