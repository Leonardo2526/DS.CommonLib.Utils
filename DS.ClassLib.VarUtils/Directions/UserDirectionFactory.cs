using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Directions
{
    /// <summary>
    ///  An object that represents factory to get directions.
    /// </summary>
    public class UserDirectionFactory : IDirectionFactory
    {
        private Vector3D _main;
        private Vector3d _main3d;
        private Vector3D _normal;
        private Vector3d _normal3d;
        private Vector3D _crossProduct;
        private Vector3d _crossProduct3d;
        private List<int> _angles;
        private List<Vector3D> _plane1_Directions;
        private List<Vector3D> _plane2_Directions;
        private List<Vector3D> _directions;
        private List<Vector3D> _plane3_Directions;
        private OrthoNormBasis _basis;

        /// <summary>
        /// Instantiate an object that represents factory to get directions.
        /// </summary>
        public UserDirectionFactory()
        { }

        /// <inheritdoc/>
        public IDirectionFactory Build(OrthoNormBasis basis, List<int> angles = null)
        {
            _basis = basis;

            _main = basis.X;
            _normal = basis.Y;
            _crossProduct = basis.Z;

            _main3d = _main.Convert();
            _normal3d = _normal.Convert();
            _crossProduct3d = _crossProduct.Convert();

            _angles = angles;

            return this;
        }

        /// <summary>
        /// List of unit vectors in basis.X and Basis.Y plane.
        /// </summary>
        public List<Vector3D> Plane1_Directions => _plane1_Directions ??= GetPlaneDirections(_main, _normal);

        /// <summary>
        /// List of unit vectors in basis.X and Basis.Z plane.
        /// </summary>
        public List<Vector3D> Plane2_Directions => _plane2_Directions ??= GetPlaneDirections(_main, _crossProduct);

        /// <summary>
        /// List of unit vectors in basis.Y and Basis.Z plane.
        /// </summary>
        public List<Vector3D> Plane3_Directions => _plane3_Directions ??= GetPlaneDirections(_normal, _crossProduct);

        /// <inheritdoc/>
        public List<Vector3D> Directions => _directions ??= GetDirections();

        /// <inheritdoc/>
        public List<Vector3D> GetDirections()
        {
            _directions = new List<Vector3D>();

            _directions.AddRange(Plane1_Directions);
            Plane2_Directions.ForEach(x =>
            {
                if (!_directions.Contains(x))
                { _directions.Add(x); }
            });

            Plane3_Directions.ForEach(x =>
            {
                if (!_directions.Contains(x))
                { _directions.Add(x); }
            });
            return _directions;
        }

        private List<Vector3D> GetPlaneDirections(Vector3D vector1, Vector3D vector2)
        {
            var vector1Negate = vector1.DeepCopy();
            vector1Negate.Negate();

            var vector2Negate = vector2.DeepCopy();
            vector2Negate.Negate();

            var directions = new List<Vector3D>
            {
                vector1, vector1Negate, vector2, vector2Negate
            };

            var userDirections = GetUserDirections(vector1, vector2, _angles);
            directions.AddRange(userDirections);

            return directions;
        }

        private float GetSin(int angle)
        {
            var rad = angle.DegToRad();
            return (float)Math.Sin(rad);
        }

        private float GetCos(int angle)
        {
            var rad = angle.DegToRad();
            return (float)Math.Cos(rad);
        }

        private List<Vector3D> GetUserDirections(Vector3D vector1, Vector3D vector2, List<int> angles)
        {
            var userDirections = new List<Vector3D>();

            var vector1_3d = vector1.DeepCopy().Convert();
            var vector2_3d = vector2.DeepCopy().Convert();
            var normVector = Vector3d.CrossProduct(vector2_3d, vector1_3d);

            foreach (int angle in angles)
            {
                var subVectors = GetSubVectors(vector1_3d, normVector, angle);
                userDirections.AddRange(subVectors);

                var vector1_3dReversed = vector1_3d.DeepCopy();
                vector1_3dReversed.Reverse();
                subVectors = GetSubVectors(vector1_3dReversed, normVector, angle);
                userDirections.AddRange(subVectors);
            }

            return userDirections;
        }

        private List<Vector3D> GetSubVectors(Vector3d baseVector, Vector3d normVector, int angle)
        {
            var vectors = new List<Vector3D>();
            var radAngle = angle.DegToRad();

            var base3dCopy = baseVector.DeepCopy();
            if (base3dCopy.Rotate(radAngle, normVector)) { vectors.Add(base3dCopy.Convert()); }

            base3dCopy = baseVector.DeepCopy();
            if (base3dCopy.Rotate(-radAngle, normVector)) { vectors.Add(base3dCopy.Convert()); }

            return vectors;
        }
      
    }
}
