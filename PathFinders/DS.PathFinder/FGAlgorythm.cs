using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Directions;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.Points;
using FrancoGustavo.Algorithm;
using IMap = DS.ClassLib.VarUtils.GridMap.IMap;

namespace FrancoGustavo
{
    public class FGAlgorythm
    {
        public static List<PointPathFinderNode> GetFloatPathByMap(Point3D upperBound, Point3D lowerBound, Point3D startPoint, Point3D endPoint,
          ITraceCollisionDetector collisionDetector,
          IDirectionFactory directionFactory, OrthoBasis stepBasis)
        {
            List<PointPathFinderNode> path = new List<PointPathFinderNode>();

            var negateBasis = stepBasis.Negate();
            var orths = new List<Vector3D>() { stepBasis.X, stepBasis.Y, stepBasis.Z, negateBasis.X, negateBasis.Y, negateBasis.Z };

            var step = 1;
            var mHEstimate = 10;
            var nodeBuilder = new NodeBuilder(HeuristicFormula.Manhattan, mHEstimate, startPoint, endPoint, step,  orths, collisionDetector, 0, true);
            TestPathFinder mPathFinder = null;  
            //var mPathFinder = new TestPathFinder(upperBound, lowerBound, pathRequiment, collisionDetector, nodeBuilder, null, null);

            var userDirectionFactory = directionFactory as UserDirectionFactory;
            if (userDirectionFactory == null) { return null; }

            var dirs = userDirectionFactory.Plane1_Directions;
            var mDirs = dirs.Multiply(stepBasis);

            path = mPathFinder.FindPath(
                   startPoint,
                    endPoint, mDirs);
          
            return path;
        }

    }
}
