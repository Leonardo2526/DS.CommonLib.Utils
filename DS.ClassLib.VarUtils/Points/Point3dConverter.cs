using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils.Points
{
    /// <inheritdoc/>
    public class Point3dConverter : IPoint3dConverter
    {
        private readonly Transform _rotationTransform;
        private readonly Transform _inverseRotationTransform;
        private readonly Transform _translationTransform;
        private readonly Transform _inverseTranslationTransform;
        private readonly List<Point3d> _pointsToTransform;

        /// <inheritdoc/>
        public Point3dConverter(Transform rotationTransform, Transform translationTransform)
        {
            _rotationTransform = rotationTransform;
            rotationTransform.TryGetInverse(out _inverseRotationTransform);

            _translationTransform = translationTransform;
            translationTransform.TryGetInverse(out _inverseTranslationTransform);

            _pointsToTransform = new List<Point3d>();
        }

        /// <inheritdoc/>
        public Point3d ConvertToUCS1(Point3d pointToConvert)
        {
            var point = RotateToUCS1(pointToConvert);
            return TranslateToUCS1(point);
        }

        /// <inheritdoc/>
        public Point3d ConvertToUCS2(Point3d pointToConvert)
        {
            var point = TranslateToUCS2(pointToConvert);
            return RotateToUCS2(point);
        }

        /// <inheritdoc/>
        public Vector3d ConvertToUCS1(Vector3d uCS2Vector)
        {
            var point = new Point3d(uCS2Vector.X, uCS2Vector.Y, uCS2Vector.Z);
            point = RotateToUCS1(point);
            return new Vector3d(point.X, point.Y, point.Z);
        }

        /// <inheritdoc/>
        public Vector3d ConvertToUCS2(Vector3d uCS1Vector)
        {
            var point = new Point3d(uCS1Vector.X, uCS1Vector.Y, uCS1Vector.Z);
            point = RotateToUCS2(point);
            return new Vector3d(point.X, point.Y, point.Z);
        }

        /// <inheritdoc/>
        public Basis3d ConvertToUCS1(Basis3d uCS2Basis)
        {
            var origin = ConvertToUCS1(uCS2Basis.Origin);
            var x = ConvertToUCS1(uCS2Basis.X);
            var y = ConvertToUCS1(uCS2Basis.Y);
            var z = ConvertToUCS1(uCS2Basis.Z);
            return new Basis3d(origin, x, y, z);
        }

        /// <inheritdoc/>
        public Basis3d ConvertToUCS2(Basis3d uCS1Basis)
        {
            var origin = ConvertToUCS2(uCS1Basis.Origin);
            var x = ConvertToUCS2(uCS1Basis.X);
            var y = ConvertToUCS2(uCS1Basis.Y);
            var z = ConvertToUCS2(uCS1Basis.Z);
            return new Basis3d(origin, x, y, z);
        }


        private Point3d TranslateToUCS1(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _inverseTranslationTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();

            return point;
        }

        private Point3d TranslateToUCS2(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _translationTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();

            return point;
        }

        private Point3d RotateToUCS1(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _inverseRotationTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();

            return point;
        }

        private Point3d RotateToUCS2(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _rotationTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();

            return point;
        }
    }
}
