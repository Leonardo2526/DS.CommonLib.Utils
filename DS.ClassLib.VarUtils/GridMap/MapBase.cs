using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils.GridMap.d2
{
    public abstract class MapBase<T> : IMap
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
        public T[,,] Matrix { get; private set; }

        public abstract T[,,] Fill(Point3D startPoint, Point3D endPoint, 
            List<Point3D> path, List<Point3D> unpassiblePoints = null);


        protected abstract T[,,] GetMatrix();

        public abstract void Show();
    }
}