using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface represents direction settings.
    /// </summary>
    public interface IDirectionSettings
    {
        /// <summary>
        /// Priority direction.
        /// </summary>
        Vector3d PriorityDirection { get; }

        /// <summary>
        /// All directions with activity value.
        /// </summary>
        List<(Vector3d, bool)> Directions { get; }
    }
}
