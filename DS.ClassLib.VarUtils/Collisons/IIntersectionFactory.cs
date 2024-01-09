using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Collisons
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
}
