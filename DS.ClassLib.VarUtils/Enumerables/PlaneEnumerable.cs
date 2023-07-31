using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Enumerables
{
    /// <summary>
    /// An object that represents the enumerator to iterate over <see cref="Plane"/>'s.
    /// </summary>
    public class PlaneEnumerable : IEnumerable<Plane>
    {
        private readonly List<Plane> _planes;

        /// <summary>
        /// Instantiate an object that represents the enumerator to iterate over <see cref="Plane"/>'s.
        /// </summary>
        public PlaneEnumerable(List<Plane> planes)
        {
            _planes = planes;
        }

        /// <inheritdoc/>
        public IEnumerator<Plane> GetEnumerator() => _planes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _planes.GetEnumerator();
    }
}
