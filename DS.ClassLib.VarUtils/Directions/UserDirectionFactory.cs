using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Directions
{
    /// <summary>
    ///  An object that represents factory to get directions.
    /// </summary>
    public class UserDirectionFactory : IDirectionFactory
    {
        private Vector3d _basisX;
        private Vector3d _basisY;
        private Vector3d _basisZ;
        private List<int> _angles;
        private List<Vector3d> _plane1_Directions;
        private List<Vector3d> _plane2_Directions;
        private List<Vector3d> _directions;
        private List<Vector3d> _plane3_Directions;

        /// <summary>
        /// Instantiate an object that represents factory to get directions.
        /// </summary>
        public UserDirectionFactory()
        { }

        /// <inheritdoc/>
        public IDirectionFactory Build(Vector3d basisX, Vector3d basisY, Vector3d basisZ, List<int> angles = null)
        {
            _basisX = basisX;
            _basisY = basisY;
            _basisZ = basisZ;
            _angles = angles.Where(a => a != 90).ToList();

            return this;
        }

        /// <summary>
        /// List of unit vectors in basis.X and Basis.Y plane.
        /// </summary>
        public List<Vector3d> Plane1_Directions => _plane1_Directions ??= GetPlaneDirections(_basisX, _basisY);

        /// <summary>
        /// List of unit vectors in basis.X and Basis.Z plane.
        /// </summary>
        public List<Vector3d> Plane2_Directions => _plane2_Directions ??= GetPlaneDirections(_basisX, _basisZ);

        /// <summary>
        /// List of unit vectors in basis.Y and Basis.Z plane.
        /// </summary>
        public List<Vector3d> Plane3_Directions => _plane3_Directions ??= GetPlaneDirections(_basisY, _basisZ);

        /// <inheritdoc/>
        public List<Vector3d> Directions => _directions ??= GetDirections();

        /// <inheritdoc/>
        public List<Vector3d> GetDirections()
        {
            _directions = new List<Vector3d>();

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

        private List<Vector3d> GetPlaneDirections(Vector3d vector1, Vector3d vector2)
        {
            var vector1Negate = Vector3d.Negate(vector1);
            var vector2Negate = Vector3d.Negate(vector2);

            var directions = new List<Vector3d>
            {
                vector1, vector1Negate, vector2, vector2Negate
            };

            var userDirections = GetUserDirections(vector1, vector2, _angles);
            directions.AddRange(userDirections);

            return directions;
        }

        private List<Vector3d> GetUserDirections(Vector3d vector1, Vector3d vector2, List<int> angles)
        {
            var userDirections = new List<Vector3d>();

            var normVector = Vector3d.CrossProduct(vector2, vector1);

            foreach (int angle in angles)
            {
                var subVectors = GetSubVectors(vector1, normVector, angle);
                userDirections.AddRange(subVectors);

                var vector1_3dReversed = vector1.DeepCopy();
                vector1_3dReversed.Reverse();
                subVectors = GetSubVectors(vector1_3dReversed, normVector, angle);
                userDirections.AddRange(subVectors);
            }

            return userDirections;
        }

        private List<Vector3d> GetSubVectors(Vector3d baseVector, Vector3d normVector, int angle)
        {
            var vectors = new List<Vector3d>();
            var radAngle = angle.DegToRad();

            var base3dCopy = baseVector.DeepCopy();
            if (base3dCopy.Rotate(radAngle, normVector)) { vectors.Add(base3dCopy); }

            base3dCopy = baseVector.DeepCopy();
            if (base3dCopy.Rotate(-radAngle, normVector)) { vectors.Add(base3dCopy); }

            return vectors;
        }
      
    }
}
