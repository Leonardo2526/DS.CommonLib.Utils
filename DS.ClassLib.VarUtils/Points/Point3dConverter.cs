using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Points
{
    /// <inheritdoc/>
    public class Point3dConverter : IPoint3dConverter
    {
        private readonly Transform _transform;
        private readonly Transform _inverseTransform;
        private readonly List<Point3d> _pointsToTransform;

        /// <inheritdoc/>
        public Point3dConverter(Transform transform, Transform inverseTransform)
        {
            _transform = transform;
            _inverseTransform = inverseTransform;
            _pointsToTransform = new List<Point3d>();
        }

        /// <inheritdoc/>
        public Point3d ConvertToUCS1(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _inverseTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();
            return point;
        }

        /// <inheritdoc/>
        public Point3d ConvertToUCS2(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _transform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();
            return point;
        }

        /// <inheritdoc/>
        public Vector3d ConvertToUCS1(Vector3d uCS2Vector)
        {
            var point= new Point3d(uCS2Vector.X, uCS2Vector.Y, uCS2Vector.Z);
            point = ConvertToUCS1(point);
            return new Vector3d(point.X, point.Y, point.Z); 
        }

        /// <inheritdoc/>
        public Vector3d ConvertToUCS2(Vector3d uCS1Vector)
        {
            var point = new Point3d(uCS1Vector.X, uCS1Vector.Y, uCS1Vector.Z);
            point = ConvertToUCS2(point);
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

    }
}
