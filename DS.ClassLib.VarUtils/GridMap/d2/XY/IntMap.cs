using DS.ClassLib.VarUtils.Points;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 10x10 with 2 walls. Start and goal points by map's angles.
    /// </summary>
    public class IntMap : MapBase<int>
    {
        public IntMap(Point3D minPoint, Point3D maxPoint, Vector3D stepVector) : base(minPoint, maxPoint, stepVector)
        {
        }

        public override int[,,] Fill(Point3D startPoint, Point3D endPoint, 
            List<Point3D> path, List<Point3D> unpassiblePoints = null)
        {
            path.ForEach(p => { Matrix[(int)p.X, (int)p.Y, (int)p.Z] = 5; });
            if (unpassiblePoints != null)
            { unpassiblePoints.ForEach(p => { Matrix[(int)p.X, (int)p.Y, (int)p.Z] = 1; }); }

            Matrix[(int)startPoint.X, (int)startPoint.Y, (int)startPoint.Z] = 8;
            Matrix[(int)endPoint.X, (int)endPoint.Y, (int)endPoint.Z] = 9;

            return Matrix;
        }

        public override void Show()
        {
            var drawer = new ConsoleIntMapDrawer(this);
            drawer.Draw();
        }

        protected override int[,,] GetMatrix()
        {
            int maxXCount = (int)Math.Round((MaxPoint.X - MinPoint.X) / _stepVector.X);
            int maxYCount = (int)Math.Round((MaxPoint.Y - MinPoint.Y) / _stepVector.Y);
            int maxZCount = (int)Math.Round((MaxPoint.Z - MinPoint.Z) / _stepVector.Z);

            var matrix = new int[maxXCount + 1, maxYCount + 1, maxZCount + 1];
            return matrix;
        }
    }
}
