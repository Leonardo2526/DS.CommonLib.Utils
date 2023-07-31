using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents iterator over <see cref="Plane"/>'s.
    /// </summary>
    public class PlaneIterator : IEnumerator<Plane>
    {
        private List<Plane> _planes;
        private int _position = -1;

        /// <summary>
        /// Instansiate an object that represents iterator over <see cref="Plane"/>'s.
        /// </summary>
        public PlaneIterator(List<Plane> planes)
        {
            _planes = planes;
        }

        /// <inheritdoc/>
        public Plane Current
        {
            get
            {
                if (_position == -1 || _position >= _planes.Count)
                    throw new ArgumentException();
                return _planes[_position];
            }
        }

        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_position < _planes.Count - 1)
            {
                _position++;
                return true;
            }
            else
                return false;
        }

        /// <inheritdoc/>
        public void Reset() => _position = -1;
    }
}
