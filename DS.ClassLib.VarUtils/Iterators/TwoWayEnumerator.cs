using System;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Iterator over a generic collection with abilily to move forward and back 
    ///  though fixed <paramref name="items"/> collection. 
    /// </summary>
    /// <typeparam name="T"></typeparam>     
    /// <param name="items"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public class TwoWayEnumerator<T>(IEnumerable<T> items) :
        Enumerator<T>(items), ITwoWayEnumerator<T>
    {
        /// <inheritdoc/>
        public bool MoveBack()
        {
            if (_index <= 0)
            {
                return false;
            }

            --_index;
            return true;
        }
    }
}
