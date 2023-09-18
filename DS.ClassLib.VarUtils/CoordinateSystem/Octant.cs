using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents octant of Euclidean three-dimensional coordinate system.
    /// </summary>
    public class Octant
    {
        private readonly List<Rectangle3d> _quadrants;

        /// <summary>
        /// Instansiate an object that represents octant of Euclidean three-dimensional coordinate system.
        /// </summary>
        public Octant(Basis3d basis, Rectangle3d[] quadrants, BoundingBox box)
        {
            Basis = basis;
            Quadrants = quadrants;
            _quadrants = quadrants.ToList();
            Box = box;
        }

        /// <summary>
        /// Three vectors that represent octant's half-axis.
        /// </summary>
        public Basis3d Basis { get; }

        /// <summary>
        /// Three planes each bounded by two half-axes.
        /// </summary>
        public Rectangle3d[] Quadrants { get; }

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.X"/> and <see cref="Basis.Basis3d.Y"/>.
        /// </summary>
        public Rectangle3d XYQuadrant =>
            _quadrants.First(q => q.Plane.Normal.IsParallelTo(Vector3d.ZAxis) != 0);

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.X"/> and <see cref="Basis.Basis3d.Z"/>.
        /// </summary>
        public Rectangle3d XZQuadrant =>
          _quadrants.First(q => q.Plane.Normal.IsParallelTo(Vector3d.YAxis) != 0);

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.Y"/> and <see cref="Basis.Basis3d.Z"/>.
        /// </summary>
        public Rectangle3d YZQuadrant =>
          _quadrants.First(q => q.Plane.Normal.IsParallelTo(Vector3d.XAxis) != 0);

        /// <summary>
        /// Octant's solid geometry.
        /// </summary>
        public BoundingBox Box { get; }

    }
}
