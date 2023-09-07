using Rhino;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public class DirectionSettings : IDirectionSettings
    {
        private readonly double _angleTolerance = RhinoMath.ToRadians(3);
        private readonly List<(Vector3d, bool)> _directions;
        private Vector3d _priorityDirection;

        public DirectionSettings(List<(Vector3d, bool)> directions)
        {
            _directions = directions;
        }

        /// <summary>
        /// Specifies priority direction.
        /// <para>
        /// Get <see langword="null"/> if it was failed to set.
        /// </para>
        /// </summary>
        public Vector3d PriorityDirection
        {
            get => _priorityDirection;
            set 
            {
                if (Directions.Exists(d => d.Item1.IsParallelTo(value, _angleTolerance) == 1 && d.Item2))
                { _priorityDirection = value; }
            }
        }

        /// <inheritdoc/>
        public List<(Vector3d, bool)> Directions => _directions;


    }
}
