using DS.ClassLib.VarUtils.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Iterator over a generic collection with abilily to move forward and back.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TwoWayIterator<T> : ITwoWayEnumerator<T>
    {
        private List<T> _items;
        private int _index;

        /// <summary>
        /// Instansiate iterator over a generic collection with abilily to move forward and back. 
        /// </summary>
        /// <param name="items"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TwoWayIterator(List<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("enumerator");

            _items = items;
            _index = -1;
        }

        /// <inheritdoc/>
        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _items.Count)
                    throw new InvalidOperationException();

                return _items[_index];
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }


        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_index < _items.Count - 1)
            {
                ++_index;
                return true;
            }

            return false;
        }

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

        /// <inheritdoc/>
        public void Reset()
        {
            _items.Clear();
            _index = -1;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
