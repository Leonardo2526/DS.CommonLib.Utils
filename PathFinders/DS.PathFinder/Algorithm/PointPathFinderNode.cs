using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace FrancoGustavo
{
    public struct PointPathFinderNode
    {
        private static int _tolerance = 2;
        private static double _dTolerance = Math.Pow(0.1, _tolerance);
        private static Point3D ZeroPoint= new Point3D(0,0,0);

        private Vector3D _deltaStep;
        private Vector3D _deltaVector;
        private readonly IPointVisualisator<Point3D> _pointVisualisator;
        private readonly IPoint3dConverter _pointConverter;

        public PointPathFinderNode(Point3D point, Point3D parentPoint, Point3D anglePoint, 
            IPointVisualisator<Point3D> pointVisualisator = null, IPoint3dConverter pointConverter = null)
        {
            Point = point;
            Parent = parentPoint;
            ANP = anglePoint;
            _pointVisualisator = pointVisualisator;
            _pointConverter = pointConverter;
        }

        public double F => G + H + B;

        public double G { get; set; }
        public double H { get; set; }
        public double B { get; set; }

        public Point3D Point { get; set; }
        public Point3D Parent { get; set; }
        public Point3D ANP { get; set; }
        public Vector3D Dir { get; set; }

        public Vector3D StepVector { get; set; }

        public Vector3D NormDir
        {
            get
            {
                var dir = Dir.DeepCopy();
                dir.Normalize();
                return dir;
            }
        }

        public List<Vector3D> DirList => new List<Vector3D>() { Dir };
        public Vector3D DeltaVector { get; set; }

        public double LengthToANP => Math.Round(ANP.DistanceTo(Point), _tolerance);

        public void UpdateStep( Point3D endPoint, List<Vector3D> normOrths, double step)
        {
            //get intersection point
            Point3D intersecioinPoint = GetIntersectionPoint(this, endPoint, normOrths);
            var point3d = _pointConverter.ConvertToUCS1(intersecioinPoint.Convert()).Convert();
            //_pointVisualisator?.Show(point3d);

            //get real calculation step
            double calcStep;
            if (intersecioinPoint == ZeroPoint)
            {
                calcStep = step;
            }
            else
            {
                var vectorLength = (intersecioinPoint - Point).Length;
                int stepsCount = (int)Math.Round(vectorLength / step);
                calcStep = vectorLength / stepsCount;
            }


            StepVector = Vector3D.Multiply(Dir, calcStep);

            Point3D GetIntersectionPoint(PointPathFinderNode node, Point3D endPoint, List<Vector3D> normOrths)
            {
                Point3D intersecioinPoint = new Point3D();
                var line1 = new Line(node.Point.Convert(), (node.Point + node.Dir).Convert());

                List<Point3D> foundPoints = new List<Point3D>();
                foreach (var nOrth in normOrths)
                {
                    var line2 = new Line(endPoint.Convert(), (endPoint + nOrth).Convert());
                    var intersection = Intersection.LineLine(line1, line2, out double a, out double b, _dTolerance, false);
                    if (intersection)
                    {
                        var pointAtLine1 = line1.PointAt(a).Convert();
                        if (pointAtLine1.Round(_tolerance) == node.Point.Round(_tolerance)) { continue; }
                        else
                        { foundPoints.Add(pointAtLine1); }
                        //{ intersecioinPoint = pointAtLine1; break; }
                    }
                }

                if(foundPoints.Count == 1) 
                {intersecioinPoint = foundPoints.FirstOrDefault();}
                else if(foundPoints.Count > 1)
                { intersecioinPoint = foundPoints.OrderByDescending(p => node.Point.DistanceTo(p)).Last(); }

                return intersecioinPoint;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj is not PointPathFinderNode) throw new InvalidCastException();
            return obj is PointPathFinderNode node &&
                Math.Round((Point - node.Point).Length, _tolerance) < _dTolerance
                //&& (ANP - node.ANP).Length < _dTolerance
                ;
        }

        public override int GetHashCode()
        {
            return -1396796455 + Point.GetHashCode();
        }
    }
}