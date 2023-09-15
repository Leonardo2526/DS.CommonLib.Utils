using Castle.Core.Internal;
using DS.ClassLib.VarUtils.Basis;
using Rhino;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents <see cref="Octant"/> that can change activity of it's own objects.
    /// </summary>
    public class OctantModel : Octant, IOctantModel
    {
        private static readonly int _cTolerance = 3;
        private static readonly double _aTolerance = RhinoMath.ToRadians(_cTolerance);
        private readonly bool _defaultActivity;
        private (BoundingBox Box, bool IsEnable) _boxModel;
        private readonly (Rectangle3d Rectangle, bool IsEnable)[] _quadrants = 
            new (Rectangle3d, bool)[3];
        private readonly (Vector3d Axis, bool IsEnable)[] _basisModel = 
            new (Vector3d, bool)[3];
        private bool _isEnable;

        /// <summary>
        /// Instansiate an object that represents <see cref="Octant"/> 
        /// that can change activity of it's own objects.
        /// </summary>
        public OctantModel(Basis3d basis, Rectangle3d[] quadrants, BoundingBox box,
            bool defaultActivity = true) :
            base(basis, quadrants, box)
        {
            _defaultActivity = defaultActivity;
            _isEnable = defaultActivity;
            _basisModel = GetBasisModel();
            _boxModel = (Box, _defaultActivity);
            _quadrants = GetQuadrantsModel();
        }

        #region Properties

        /// <inheritdoc/>
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                SetBasisActivity(value);
                SetQuadrantsActivity(value);
                SetBoxActivity(value);
            }
        }

        /// <inheritdoc/>
        public (BoundingBox Box, bool IsEnable) BoxModel => _boxModel;

        /// <inheritdoc/>
        public (Rectangle3d Rectangle, bool IsEnable)[] QuadrantsModel => _quadrants;

        /// <inheritdoc/>
        public (Vector3d Axis, bool IsEnable)[] BasisModel => _basisModel;

        /// <summary>
        /// Quadrants that are active.
        /// </summary>
        public IEnumerable<Rectangle3d> ActiveQuadrants =>
            _quadrants.Where(m => m.IsEnable == true).Select(x => x.Rectangle);

        #endregion

        #region PublicMethods

        /// <inheritdoc/>
        public void SetBasisActivity(bool activity, Vector3d axis = default)
        {
            if (axis.Equals(default))
            { _basisModel.ForEach(b => b.IsEnable = activity); }

            var basis = _basisModel.FirstOrDefault(m => m.Axis.IsParallelTo(axis, _aTolerance) == 1);
            if (!basis.Equals(default))
            { basis.IsEnable = activity; }
        }

        /// <inheritdoc/>
        public void SetQuadrantsActivity(bool activity, OrthoPlane orthoPlane = 0)
        {
            switch (orthoPlane)
            {
                case OrthoPlane.XY:
                    _quadrants[0].IsEnable = activity;
                    break;
                case OrthoPlane.XZ:
                    _quadrants[1].IsEnable = activity;
                    break;
                case OrthoPlane.YZ:
                    _quadrants[2].IsEnable = activity;
                    break;
                default:
                    for (int i = 0; i < _quadrants.Length; i++)
                    { _quadrants[i].IsEnable = activity; }
                    break;
            }
        }

        /// <inheritdoc/>
        public void SetQuadrantsActivity(bool activity, Vector3d axis)
        {
            for (int i = 0; i < QuadrantsModel.Length; i++)
            {
                var rect = _quadrants[i].Rectangle;
                if (rect.ContainsStrict(Basis.Origin + axis))
                { _quadrants[i].IsEnable = activity; }
            }
        }

        /// <inheritdoc/>
        public void SetQuadrantsActivity(bool activity, Vector3d axis, OrthoPlane orthoPlane = 0)
        {
            Vector3d normal = default;
            switch (orthoPlane)
            {
                case OrthoPlane.XY:
                    normal = Vector3d.ZAxis;
                    break;
                case OrthoPlane.XZ:
                    normal = Vector3d.YAxis;
                    break;
                case OrthoPlane.YZ:
                    normal = Vector3d.XAxis;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < QuadrantsModel.Length; i++)
            {
                var rect = _quadrants[i].Rectangle;
                if (!normal.Equals(default) &&
                    rect.Plane.Normal.IsParallelTo(normal) == 0) { continue; }
                if (rect.ContainsStrict(Basis.Origin + axis))
                { _quadrants[i].IsEnable = activity; }
            }
        }

        /// <inheritdoc/>
        public void SetBoxActivity(bool activity)
        {
            _boxModel.IsEnable = activity;
        }

        /// <inheritdoc/>
        public bool IsValid(Point3d point)
        {
            if (!IsEnable) { return false; }

            return ActiveQuadrants.Any(q => q.ContainsStrict(point)) ||
               (_boxModel.IsEnable && _boxModel.Box.Contains(point, true));
        }

        /// <inheritdoc/>
        public bool IsValid(Vector3d vector)
        {
            return IsValid(Basis.Origin + vector);
        }


        #endregion


        private (Rectangle3d Rectangle, bool IsEnable)[] GetQuadrantsModel()
        {
            var quadrantsModel = new (Rectangle3d, bool)[3];
            quadrantsModel[0] = (Quadrants[0], _defaultActivity);
            quadrantsModel[1] = (Quadrants[1], _defaultActivity);
            quadrantsModel[2] = (Quadrants[2], _defaultActivity);

            return quadrantsModel;
        }

        private (Vector3d Axis, bool IsEnable)[] GetBasisModel()
        {
            var basisModel = new (Vector3d, bool)[3];
            basisModel[0] = (Basis.X, _defaultActivity);
            basisModel[1] = (Basis.Y, _defaultActivity);
            basisModel[2] = (Basis.Z, _defaultActivity);

            return basisModel;
        }

    }
}
