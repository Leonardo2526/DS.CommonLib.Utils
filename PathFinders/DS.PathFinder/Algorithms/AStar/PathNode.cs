using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;
using System;

namespace DS.PathFinder.Algorithms.AStar
{
    /// <summary>
    /// An object that represents path node.
    /// </summary>
    public struct PathNode
    {
        /// <summary>
        /// Path priority.
        /// </summary>
        public double F { get; set; }

        /// <summary>
        /// Distance to parent node.
        /// </summary>
        public double G { get; set; }

        /// <summary>
        /// Distance to end point.
        /// </summary>
        public double H { get; set; }

        /// <summary>
        /// Point of node.
        /// </summary>
        public Point3d Point { get; set; }

        /// <summary>
        /// Parent point.
        /// </summary>
        public Point3d Parent { get; set; }

        /// <summary>
        /// Angle point.
        /// </summary>
        public Point3d ANP { get; set; }

        /// <summary>
        /// Node direction.
        /// </summary>
        public Vector3d Dir { get; set; }

        /// <summary>
        /// <see cref="Vector3d"/> that specifies relative position of <see cref="Point"/> with respect to <see cref="Parent"/> point.
        /// </summary>
        public Vector3d StepVector { get; set; }

        /// <summary>
        /// Basis at <see cref="Point"/> of node.
        /// </summary>
        public Basis3d Basis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public override bool Equals(object obj)
        {
            if (obj is not PathNode) throw new InvalidCastException();
            return obj is PathNode node && Point == node.Point;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -1396796455 + Point.GetHashCode();
        }
    }
}