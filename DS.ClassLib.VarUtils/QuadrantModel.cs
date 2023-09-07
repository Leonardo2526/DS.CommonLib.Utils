using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public class QuadrantModel
    {
        private static readonly int _cTolerance = 3;
        private static readonly double _cT = Math.Pow(0.1, _cTolerance);
        private readonly bool _defaultAvailability = true;
        private Rectangle3d _priorityQuadrant;
        private readonly bool _onlyPositive = true;

        public QuadrantModel()
        {
            int countCorrection = _onlyPositive ? 2 : 0;

            for (int i = 0; i < BoxOrigin.XYquadrants.Length; i++)
            {
                var q = BoxOrigin.XYquadrants[i];
                var isEnable = _onlyPositive && (i == 1 || i == 2) ? false : _defaultAvailability;
                XYquadrants[i] = (q, isEnable);
            }

            for (int i = 0; i < BoxOrigin.XZquadrants.Length; i++)
            {
                var q = BoxOrigin.XZquadrants[i];
                var isEnable = _onlyPositive && (i == 1 || i == 2) ? false : _defaultAvailability;
                XZquadrants[i] = (q, isEnable);
            }

            for (int i = 0; i < BoxOrigin.YZquadrants.Length; i++)
            {
                var q = BoxOrigin.YZquadrants[i];
                var isEnable = _onlyPositive && (i == 1 || i == 2) ? false : _defaultAvailability;
                YZquadrants[i] = (q, isEnable);
            }

            Quadrants = new (Rectangle3d, bool)[12]
            {
                XYquadrants[0], XYquadrants[1], XYquadrants[2], XYquadrants[3],
                XZquadrants[0], XZquadrants[1], XZquadrants[2], XZquadrants[3],
                YZquadrants[0], YZquadrants[1], YZquadrants[2], YZquadrants[3]
            };

        }

        public Rectangle3d PriorityQuadrant
        { get => _priorityQuadrant; set => _priorityQuadrant = value; }

        public (Rectangle3d, bool)[] Quadrants = new (Rectangle3d, bool)[12];
        public (Rectangle3d, bool)[] XYquadrants = new (Rectangle3d, bool)[4];
        public (Rectangle3d, bool)[] XZquadrants = new (Rectangle3d, bool)[4];
        public (Rectangle3d, bool)[] YZquadrants = new (Rectangle3d, bool)[4];

        public bool TrySetPriority(Vector3d vector)
        {
            var activeXYQuadrants = XYquadrants.ToList().Where(q => q.Item2);
            var activeXZQuadrants = XZquadrants.ToList().Where(q => q.Item2);

            return TrySetPriorityQuadran(activeXYQuadrants) || TrySetPriorityQuadran(activeXZQuadrants);

            bool TrySetPriorityQuadran(IEnumerable<(Rectangle3d, bool)> quadrants)
            {
                foreach (var quadrant in quadrants)
                {
                    var point = Point3d.Origin + vector;
                    var contaiment = quadrant.Item1.Contains(point);
                    if (contaiment != PointContainment.Coincident ||
                        contaiment != PointContainment.Inside)
                    { continue; }

                    var plane = quadrant.Item1.Plane;
                    var dist = plane.DistanceTo(point);
                    if (dist < _cT)
                    { _priorityQuadrant = quadrant.Item1; return true; }
                }
                return false;
            }

        }
    }
}
