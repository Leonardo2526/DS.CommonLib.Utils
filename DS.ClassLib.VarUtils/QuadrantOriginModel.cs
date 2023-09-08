using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public class QuadrantOriginModel
    {
        private static readonly int _cTolerance = 3;
        private static readonly double _cT = Math.Pow(0.1, _cTolerance);
        private readonly bool _defaultAvailability = true;
        private Rectangle3d _priorityQuadrant;
        private Point3d _origin = Point3d.Origin;
        private readonly (Rectangle3d, bool)[] _quadrants = new (Rectangle3d, bool)[12];

        public QuadrantOriginModel()
        {
            for (int i = 0; i < BoxOrigin.XYquadrants.Length; i++)
            {
                var q = BoxOrigin.XYquadrants[i];
                XYquadrants[i] = (q, _defaultAvailability);
            }

            for (int i = 0; i < BoxOrigin.XZquadrants.Length; i++)
            {
                var q = BoxOrigin.XZquadrants[i];
                XZquadrants[i] = (q, _defaultAvailability);
            }

            for (int i = 0; i < BoxOrigin.YZquadrants.Length; i++)
            {
                var q = BoxOrigin.YZquadrants[i];
                YZquadrants[i] = (q, _defaultAvailability);
            }
        }

        #region Properties

        public Rectangle3d PriorityQuadrant
        { get => _priorityQuadrant; set => _priorityQuadrant = value; }

        public (Rectangle3d, bool)[] XYquadrants = new (Rectangle3d, bool)[4];
        public (Rectangle3d, bool)[] XZquadrants = new (Rectangle3d, bool)[4];
        public (Rectangle3d, bool)[] YZquadrants = new (Rectangle3d, bool)[4];

        private (Rectangle3d, bool)[] _Quadrants
        {
            get
            {
                int j = 0;
                for (int i = 0; i < XYquadrants.Length; i++)
                {_quadrants[i + j] = XYquadrants[i];}
                j += XYquadrants.Length;
                for (int i = 0; i < XZquadrants.Length; i++)
                { _quadrants[i + j] = XZquadrants[i]; }
                j += XZquadrants.Length;
                for (int i = 0; i < YZquadrants.Length; i++)
                { _quadrants[i + j] = YZquadrants[i]; }

                return _quadrants;
            }
        }

        private IEnumerable<(Rectangle3d, bool)> _ActiveQuadrants =>
             GetActiveQuadrants();

        private IEnumerable<(Rectangle3d, bool)> _ActiveXYquadrants =>
             GetActiveQuadrants(OrthoPlane.XY);

        private IEnumerable<(Rectangle3d, bool)> _ActiveXZquadrants =>
          GetActiveQuadrants(OrthoPlane.XZ);

        private IEnumerable<(Rectangle3d, bool)> _ActiveYZquadrants => 
            GetActiveQuadrants(OrthoPlane.YZ);

        #endregion

        #region Methods

        public (Rectangle3d, bool)[] GetQuadrants() { return _Quadrants; }

        public IEnumerable<(Rectangle3d, bool)> GetActiveQuadrants(OrthoPlane orthoPlane = 0)
        {
            var activeQuadrants = orthoPlane switch
            {
                OrthoPlane.XY => XYquadrants,
                OrthoPlane.XZ => XZquadrants,
                OrthoPlane.YZ => YZquadrants,
                _ => _Quadrants,
            };

            return activeQuadrants.ToList().Where(q => q.Item2);
        }

        public bool TrySetPriority(Vector3d vector, OrthoPlane orthoPlane = 0)
        {
            var activeQuadrants = orthoPlane switch
            {
                OrthoPlane.XY => _ActiveXYquadrants,
                OrthoPlane.XZ => _ActiveXZquadrants,
                OrthoPlane.YZ => _ActiveYZquadrants,
                _ => _ActiveQuadrants,
            };

            _priorityQuadrant = activeQuadrants.
                FirstOrDefault(q => q.Item1.ContainsStrict(_origin + vector)).Item1;

            return _priorityQuadrant.IsValid;

        }

        public bool IsEnable(Vector3d vector, OrthoPlane orthoPlane = 0)
        {
            var activeQuadrants = orthoPlane switch
            {
                OrthoPlane.XY => _ActiveXYquadrants,
                OrthoPlane.XZ => _ActiveXZquadrants,
                OrthoPlane.YZ => _ActiveYZquadrants,
                _ => _ActiveQuadrants,
            };
            return activeQuadrants.Any(q => q.Item1.ContainsStrict(_origin + vector));
        }

        public void DisableQuadrants(OrthoPlane orthoPlane = 0)
        {
            var quadrants = orthoPlane switch
            {
                OrthoPlane.XY => XYquadrants,
                OrthoPlane.XZ => XZquadrants,
                OrthoPlane.YZ => YZquadrants,
                _ => _Quadrants,
            };

            if (quadrants.Length == 4) { ChangeActivity(quadrants, false); }
            else
            {
                ChangeActivity(XYquadrants, false);
                ChangeActivity(XZquadrants, false);
                ChangeActivity(YZquadrants, false);
            }
        }

        public void EnableQuadrants(OrthoPlane orthoPlane = 0)
        {
            var quadrants = orthoPlane switch
            {
                OrthoPlane.XY => XYquadrants,
                OrthoPlane.XZ => XZquadrants,
                OrthoPlane.YZ => YZquadrants,
                _ => _Quadrants,
            };

            if (quadrants.Length == 4) { ChangeActivity(quadrants, true); }
            else
            {
                ChangeActivity(XYquadrants, true);
                ChangeActivity(XZquadrants, true);
                ChangeActivity(YZquadrants, true);
            }
        }

        public void EnableQuadrants(Vector3d vector, OrthoPlane orthoPlane = 0)
        {
            var quadrants = orthoPlane switch
            {
                OrthoPlane.XY => XYquadrants,
                OrthoPlane.XZ => XZquadrants,
                OrthoPlane.YZ => YZquadrants,
                _ => _Quadrants,
            };
            if(quadrants.Length == 4) { ChangeActivity(vector, quadrants, true); }
            else
            {
                ChangeActivity(vector, XYquadrants, true);
                ChangeActivity(vector, XZquadrants, true);
                ChangeActivity(vector, YZquadrants, true);
            }
        }

        private void ChangeActivity(Vector3d vector, (Rectangle3d, bool)[] quadrants, bool activity)
        {
            for (int i = 0; i < quadrants.Length; i++)
            {
                var rect = quadrants[i].Item1;
                if (rect.ContainsStrict(_origin + vector))
                { quadrants[i].Item2 = activity; }
            }
        }

        private void ChangeActivity((Rectangle3d, bool)[] quadrants, bool activity)
        {
            for (int i = 0; i < quadrants.Length; i++)
            { quadrants[i].Item2 = activity;}
        }

        #endregion

    }


}
