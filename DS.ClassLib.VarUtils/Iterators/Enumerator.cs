using System;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    ///  Iterator over a generic collection with abilily to move forward.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Enumerator<T> : IEnumerator<T>
    {
        protected readonly IEnumerator<T> _enumerator;
        protected readonly List<T> _buffer;
        protected int _index;

        /// <summary>
        /// Instansiate iterator over a generic collection with abilily to move forward
        /// through fixed <paramref name="items"/> collection. 
        /// </summary>       
        /// <param name="items"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Enumerator(IEnumerable<T> items)
        {            
            if (items == null)
                throw new ArgumentNullException("enumerator");

            _enumerator = items.GetEnumerator();
            _buffer = new List<T>();
            _index = -1;
        }

        /// <inheritdoc/>
        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _buffer.Count)
                    throw new InvalidOperationException();

                return _buffer[_index];
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        /// <inheritdoc/>
        public virtual bool MoveNext()
        {
            if (_index < _buffer.Count - 1)
            {
                ++_index;
                return true;
            }

            if (_enumerator.MoveNext())
            {
                _buffer.Add(_enumerator.Current);
                ++_index;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _enumerator.Reset();
            _buffer.Clear();
            _index = -1;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _enumerator.Dispose();
        }

    }
}
