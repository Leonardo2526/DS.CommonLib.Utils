using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ConsoleApp.Test
{
    internal class TransformTest
    {
        private double _tolerance = 0.001;

        public TransformTest()
        {
            var (alpha, beta, gamma) = Run(out List<Transform> transforms, out List<Transform> reverseTransforms);
            Console.WriteLine($"alpha = {alpha}");
            Console.WriteLine($"beta = {beta}");
            Console.WriteLine($"gamma = {gamma}");
            Console.WriteLine($"transforms count is = {transforms.Count}");

            var point = new Point3d( -5, 0, 0 );

            var transformedPoint = TransformPoint(point, reverseTransforms);
            Console.WriteLine(transformedPoint);

            var reversedPoint = TransformPoint(transformedPoint, transforms);
            Console.WriteLine(reversedPoint);
        }

        private (double alpha, double beta, double gamma) Run(out List<Transform> transforms, out List<Transform> reverseTransforms)
        {
            Point3d zeroPoint = new Point3d(0, 0, 0);

            //basis1
            Vector3d x1 = new Vector3d(1, 0, 0);
            Vector3d y1 = new Vector3d(0, 1, 0);
            Vector3d z1 = new Vector3d(0, 0, 1);
            Plane xy1 = new Plane(zeroPoint, x1, y1);

            //basis2
            //var angle = 90;
            //Vector3d x2 = x1;
            //x2.Rotate(angle.DegToRad(), z1);
            //Vector3d y2 = y1;
            //y2.Rotate(angle.DegToRad(), z1);
            //Vector3d z2 = z1;

            var angle = 90;
            Vector3d x2 = x1;
            x2.Rotate(angle.DegToRad(), y1);
            Vector3d y2 = y1;
            Vector3d z2 = z1;
            z2.Rotate(angle.DegToRad(), y1);

            //Vector3d x2 = new Vector3d(0, 0, -1);
            //x2 = Vector3d.Divide(x2, x2.Length);
            //Vector3d y2 = new Vector3d(0, 1, 0);
            //y2 = Vector3d.Divide(y2, y2.Length);
            //Vector3d z2 = new Vector3d(1, 0, 0);
            //z2 = Vector3d.Divide(z2, z2.Length);


            Plane xy2 = new Plane(zeroPoint, x2, y2);

            var intersection = Intersection.PlanePlane(xy1, xy2, out Line intersectionLine);
            Vector3d N = intersectionLine.Direction;

            //double alpha = Vector3d.VectorAngle(x1, N);
            //double beta = Vector3d.VectorAngle(z1, z2);
            //double gamma = Vector3d.VectorAngle(x2, N);

            var transform = Transform.ChangeBasis(x1, y1, z1, x2, y2, z2);

            transform.GetEulerZYZ(out double alpha1, out double beta1, out double gamma1);
           
            double alpha = alpha1.RadToDeg();
            double beta = beta1.RadToDeg();
            double gamma = gamma1.RadToDeg();

            transforms = new List<Transform>();
            reverseTransforms = new List<Transform>();

            if (alpha != 0)
            {
                transforms.Add(Transform.Rotation(-alpha1, z1, zeroPoint));
                reverseTransforms.Add(Transform.Rotation(alpha1, z1, zeroPoint));
            }

            if (beta != 0)
            { 
                transforms.Add(Transform.Rotation(-beta1, N, zeroPoint));
                reverseTransforms.Add(Transform.Rotation(beta1, N, zeroPoint));
            }

            if (gamma != 0)
            { 
                transforms.Add(Transform.Rotation(-gamma1, z2, zeroPoint));
                reverseTransforms.Add(Transform.Rotation(gamma1, z2, zeroPoint));
            }

            return (alpha, beta, gamma);
        }

        private Point3d TransformPoint(Point3d point, List<Transform> transforms)
        {
            foreach (var transform in transforms)
            {
                point.Transform(transform);
            }

            return point;
        }
    }
}
