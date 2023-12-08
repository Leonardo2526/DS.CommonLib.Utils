using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Supports a simple iteration over a generic collection with abilily to move forward and back.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITwoWayEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// Advances the enumerator to the previous element of the collection.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator was successfully advanced to the previous element.
        /// <para>
        /// <see langword="false"/> if
        /// the enumerator has passed the start of the collection.
        /// </para>
        /// </returns>
        bool MoveBack();
    }
}
