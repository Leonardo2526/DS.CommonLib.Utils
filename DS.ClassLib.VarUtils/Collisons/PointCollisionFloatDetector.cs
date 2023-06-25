using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.GridMap.d2;
using DS.ClassLib.VarUtils.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.Collisons
{
    public class PointCollisionFloatDetector : ITraceCollisionDetector
    {
        private readonly MapBase<float> _map;
        private readonly float[,,] _matrix;

        public PointCollisionFloatDetector(MapBase<float> map)
        {
            _map = map;
            _matrix = map.Matrix;
        }

        public List<ICollision> Collisions { get; } = new List<ICollision>();

        public List<ICollision> GetCollisions(Point3D point1, Point3D point2)
        {
            var pointsBetween = GetPoints(point1, point2);
            pointsBetween.ForEach(p => { Collisions.Add(new PointCollision()); });


            return Collisions;
        }

        private List<Point3D> GetPoints(Point3D point1, Point3D point2)
        {
            var points = new List<Point3D>();

            for (int x = 0; x < _matrix.GetUpperBound(0); x++)
            {
                for (int y = 0; y < _matrix.GetUpperBound(1); y++)
                {
                    for (int z = 0; z < _matrix.GetUpperBound(2); z++)
                    {
                        var point = new Point3D(x, y, z);
                        if (point.IsBetweenPoints(point1, point2) && _matrix[x,y,z] == 1) 
                        { points.Add(point); }
                    }
                }
            }

            return points;
        }
    }
}
