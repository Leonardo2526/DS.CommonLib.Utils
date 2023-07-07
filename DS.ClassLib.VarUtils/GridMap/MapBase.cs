using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap.d2
{
    public abstract class MapBase : IMap
    {
        protected readonly Vector3D _stepVector;

        protected MapBase(Point3D minPoint, Point3D maxPoint, Vector3D stepVector)
        {
            MinPoint = minPoint;
            MaxPoint = maxPoint;
            _stepVector = stepVector;
            Matrix = GetMatrix();
        }

        public  Point3D MinPoint { get; private set; }
        public Point3D MaxPoint { get; private set; }
        public int[,,] Matrix { get; private set; }

        public abstract int[,,] Fill(Point3D startPoint, Point3D endPoint, 
            List<Point3D> path, List<Point3D> unpassiblePoints = null);


        protected abstract int[,,] GetMatrix();

        public abstract void Show();
    }
}