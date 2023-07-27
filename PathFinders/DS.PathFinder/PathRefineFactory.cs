using DS.ClassLib.VarUtils.Points;
using DS.PathFinder.Algorithms.AStar;
using Rhino.Geometry;
using System.Collections.Generic;

namespace DS.PathFinder
{
    /// <summary>
    /// An object that represents factory to refine path.
    /// </summary>
    public class PathRefineFactory : IRefineFactory<Point3d>
    {
        private readonly int _tolerance = 5;


        /// <inheritdoc/>
        public List<Point3d> Refine(List<PathNode> path)
        {
            var points = new List<Point3d>();
            if (path == null || path.Count == 0)
            { return points; }

            var firstNode = path[0];
            var basePoint = firstNode.Point;
            var baseDir = firstNode.Dir;

            for (int i = 1; i < path.Count; i++)
            {
                var currentNode = path[i];
                var currentPoint = currentNode.Point;
                var currentDir = path[i].Dir;
                if (currentDir.Length != 0)
                { currentDir = Vector3d.Divide(currentDir, currentDir.Length); }

                if (baseDir.Length == 0 || currentDir.Round(_tolerance) != baseDir.Round(_tolerance))
                {
                    points.Add(basePoint);
                    baseDir = currentDir;
                }
                basePoint = currentPoint;
            }

            points.Add(basePoint);

            return points;
        }
    }
}
