using DS.ClassLib.VarUtils.Points;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap.d2
{
    /// <summary>
    /// 2-dimensional grid map size 10x10 with 2 walls. Start and goal points by map's angles.
    /// </summary>
    public class FloatMap : MapBase<float>
    {
        public FloatMap(Point3D minPoint, Point3D maxPoint, Vector3D stepVector) : base(minPoint, maxPoint, stepVector)
        {
        }

        public override float[,,] Fill(Point3D startPoint, Point3D endPoint,
            List<Point3D> path, List<Point3D> unpassiblePoints = null)
        {

            path.ForEach(p =>
            {Matrix.SetValue(5, (long)p.X, (long)p.Y, (long)p.Z);});

            unpassiblePoints.ForEach(p => 
            {Matrix.SetValue(1, (long)p.X, (long)p.Y, (long)p.Z);});
          
            Matrix.SetValue(8, (long)startPoint.X, (long)startPoint.Y, (long)startPoint.Z);
            Matrix.SetValue(9, (long)endPoint.X, (long)endPoint.Y, (long)endPoint.Z);


            //Matrix[(int)startPoint.X, (int)startPoint.Y, (int)startPoint.Z] = 8;
            //Matrix[(int)endPoint.X, (int)endPoint.Y, (int)endPoint.Z] = 9;
            var v = Matrix.GetValue((long)endPoint.X, (long)endPoint.Y, (long)endPoint.Z);

            return Matrix;
        }

        public override void Show()
        {
            var drawer = new ConsoleFloatMapDrawer(this);
            drawer.Draw();
        }

        //protected override float[,,] GetMatrix()
        //{
        //    var size = (long)Math.Round(6.17);
        //    //var size = (int)Math.Round(float.MaxValue) + (int)Math.Round(float.MinValue);
        //    var matrix = new float[size, size, size];
        //    return matrix;
        //}

        protected override float[,,] GetMatrix()
        {
            int maxXCount = (int)Math.Round((MaxPoint.X - MinPoint.X) / _stepVector.X);
            int maxYCount = (int)Math.Round((MaxPoint.Y - MinPoint.Y) / _stepVector.Y);
            int maxZCount = (int)Math.Round((MaxPoint.Z - MinPoint.Z) / _stepVector.Z);

            var matrix = new float[maxXCount + 1, maxYCount + 1, maxZCount + 1];
            return matrix;
        }
    }
}
