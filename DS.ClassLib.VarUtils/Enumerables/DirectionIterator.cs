using DS.ClassLib.VarUtils.Points;
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
    /// An object that represents iterator to get directions.
    /// </summary>
    public class DirectionIterator : IEnumerator<Vector3d>
    {
        private readonly List<Vector3d> _directions = new List<Vector3d>();
        private readonly IEnumerator<int> _twoAngleEnum;
        private readonly IEnumerator<int> _angleEnum360;
        private readonly IEnumerator<Plane> _planeEnum;

        private IEnumerator<int> _angleEnum;
        private int _position = -1;
        private Vector3d _parentDir = new Vector3d(1, 0, 0);

        /// <summary>
        /// Instantiate an object that represents iterator to get directions.
        /// </summary>
        /// <param name="planes"></param>
        /// <param name="angles"></param>
        public DirectionIterator(List<Plane> planes, List<int> angles)
        {
            _twoAngleEnum = new AngleEnumerable(angles).GetEnumerator();
            _angleEnum360 = new AngleEnumerable(angles, 360).GetEnumerator();
            _angleEnum = _angleEnum360;
            _planeEnum = new PlaneEnumerable(planes).GetEnumerator();
        }

        /// <summary>
        /// Parent node direction.
        /// </summary>
        public Vector3d ParentDir
        {
            get => _parentDir;
            set
            {
                _parentDir = value;
                _angleEnum = value.Length == 0 ? _angleEnum360 : _twoAngleEnum;
            }
        }

        /// <inheritdoc/>
        public Vector3d Current
        {
            get
            {
                if (_position == -1 || _position >= _directions.Count)
                    throw new ArgumentException();
                return _directions[_position];
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
            if (_position == -1)
            { return MoveOnFirst(); }

            if (_angleEnum.MoveNext())
            {
                if (AddDirection(_angleEnum.Current.DegToRad(), _planeEnum.Current)) { return true; }
                else { return MoveNext(); }
            }
            else
            {
                if (NextValidPlane(_parentDir))
                {
                    _angleEnum.Reset();
                    return MoveNext();
                }
                else
                { return false; }
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _position = -1;
            _directions.Clear();
            _angleEnum.Reset();
            _planeEnum.Reset();
        }

        private bool MoveOnFirst()
        {
            if (NextValidPlane(_parentDir))
            {
                var startDir = _parentDir.Length == 0 ? _planeEnum.Current.XAxis : _parentDir;
                _directions.Add(startDir);
                _position++;
                return true;
            }
            else
            { return false; }
        }

        private bool NextValidPlane(Vector3d parentDir)
        {
            while (_planeEnum.MoveNext())
            {
                if (parentDir.Length == 0) { return true; }
                var a = Math.Round(Vector3d.VectorAngle(parentDir, _planeEnum.Current.Normal).RadToDeg());
                if (a == 90)
                { return true; }
            }
            return false;
        }

        private bool AddDirection(double angle, Plane plane)
        {
            var dir = _angleEnum.Equals(_angleEnum360) ? plane.XAxis : _parentDir;
            var b = dir.Rotate(angle, plane.Normal);
            if (b)
            {
                dir = dir.Round(5);
                if (!_directions.Contains(dir))
                {
                    _directions.Add(dir);
                    _position++;
                }
                else { return false; }
            }

            return b;
        }

    }
}
