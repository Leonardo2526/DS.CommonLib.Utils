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
        private static Point3d ZeroPoint= new Point3d(0,0,0);

        private Vector3d _deltaStep;
        private Vector3d _deltaVector;
        private readonly IPointVisualisator<Point3d> _pointVisualisator;

        public PointPathFinderNode(Point3d point, Point3d parentPoint, Point3d anglePoint, 
            IPointVisualisator<Point3d> pointVisualisator = null)
        {
            Point = point;
            Parent = parentPoint;
            ANP = anglePoint;
            _pointVisualisator = pointVisualisator;
        }

        public double F => G + H + B;

        public double G { get; set; }
        public double H { get; set; }
        public double B { get; set; }

        public Point3d Point { get; set; }
        public Point3d Parent { get; set; }
        public Point3d ANP { get; set; }
        public Vector3d Dir { get; set; }

        public Vector3d StepVector { get; set; }

        public List<Vector3d> DirList => new List<Vector3d>() { Dir };

        public Vector3d DeltaVector { get; set; }

        public double LengthToANP => Math.Round(ANP.DistanceTo(Point), _tolerance);

        public void UpdateStep( Point3d endPoint, List<Vector3d> normOrths, double step)
        {
            //get intersection point
            Point3d intersecioinPoint = GetIntersectionPoint(this, endPoint, normOrths);
            //_pointVisualisator?.Show(intersecioinPoint);

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


            StepVector = Vector3d.Multiply(Dir, calcStep);

            Point3d GetIntersectionPoint(PointPathFinderNode node, Point3d endPoint, List<Vector3d> normOrths)
            {
                Point3d intersecioinPoint = new Point3d();
                var line1 = new Line(node.Point, node.Point + node.Dir);

                List<Point3d> foundPoints = new List<Point3d>();
                foreach (var nOrth in normOrths)
                {
                    var line2 = new Line(endPoint, (endPoint + nOrth));
                    var intersection = Intersection.LineLine(line1, line2, out double a, out double b, _dTolerance, false);
                    if (intersection)
                    {
                        var pointAtLine1 = line1.PointAt(a);
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