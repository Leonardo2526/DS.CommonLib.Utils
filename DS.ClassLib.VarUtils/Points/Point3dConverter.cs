using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Points
{
    public class Point3dConverter : IPoint3dConverter
    {
        private readonly Transform _transform;
        private readonly Transform _inverseTransform;
        private readonly List<Point3d> _pointsToTransform;

        public Point3dConverter(Transform transform, Transform inverseTransform)
        {
            _transform = transform;
            _inverseTransform = inverseTransform;
            _pointsToTransform = new List<Point3d>();
        }

        public Point3d ConvertToUCS1(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _inverseTransform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();
            return point;
        }

        public Point3d ConvertToUCS2(Point3d pointToConvert)
        {
            _pointsToTransform.Add(pointToConvert);
            var point = _transform.TransformList(_pointsToTransform).First();
            _pointsToTransform.Clear();
            return point;
        }
    }
}
