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
    public class DirectionIterator : IEnumerator<Vector3d>
    {
        private readonly List<Vector3d> _directions = new List<Vector3d>();
        private readonly IEnumerator<int> _twoAngleEnum;
        private readonly IEnumerator<int> _angleEnum360;
        private readonly IEnumerator<Plane> _planeEnum;

        private IEnumerator<int> _angleEnum;
        private int _position = -1;
        private Vector3d _parentDir = new Vector3d(1, 0, 0);

        //private readonly AngleEnumerable _angleEnum;
        //private readonly AngleEnumerable _angleEnum360;
        //private readonly PlaneEnumerable _planeEnum;

        public DirectionIterator(List<Plane> planes, List<int> angles)
        {
            _twoAngleEnum = new AngleEnumerable(angles).GetEnumerator();
            _angleEnum360 = new AngleEnumerable(angles, 360).GetEnumerator();
            _angleEnum = _angleEnum360;
            _planeEnum = new PlaneEnumerable(planes).GetEnumerator();
        }

        public Vector3d ParentDir
        {
            get => _parentDir;
            set
            {
                _parentDir = value;
                _angleEnum = value.Length == 0 ? _angleEnum360 : _twoAngleEnum;

                //if (value.Length == 0)
                //{
                //    _angleEnum = _angleEnum360;
                //}
                //else 
                //{ 
                //    _parentDir = value;
                //    _angleEnum = _twoAngleEnum;
                //}
            }
        }

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (_position == -1)
            {
                var b = _planeEnum.MoveNext();
                var startDir = _parentDir.Length == 0 ? _planeEnum.Current.XAxis : _parentDir;
                _directions.Add(startDir);
                _position++;
                return b;
            }

            if (_angleEnum.MoveNext())
            { 
                var b = AddDirection(_angleEnum.Current.DegToRad(), _planeEnum.Current);
                if (b) { return true; }
                else { return MoveNext(); }
            }
            else
            {
                if (_planeEnum.MoveNext())
                {
                    _angleEnum.Reset();
                    return MoveNext();
                }
                else { return false; }
            }
        }

        private bool AddDirection(double angle, Plane plane)
        {
            //var dir = plane.XAxis;
            var dir = _angleEnum.Equals(_angleEnum360) ? plane.XAxis : _parentDir;
            var a = Math.Round(Vector3d.VectorAngle(dir, plane.Normal).RadToDeg());
            if (a != 90) 
            { return false; }

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

        public void Reset()
        {
            _position = -1;
            _directions.Clear();
            _angleEnum.Reset();
            _planeEnum.Reset();
        }
    }
}
