using DS.ClassLib.VarUtils.Basis;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ConsoleApp.Test
{
    internal class PointConverterTest
    {
        private Point3dConverter _pointConverter;

        public PointConverterTest()
        {
            Run();
            var p = Test1(_pointConverter);
            InverseTest1(_pointConverter, p);
        }

        private void Run()
        {
            var basis1 = new Basis3dOrigin();

            var op2 = new Point3d(2, 1, 0);
            var x2 = new Vector3d(0, 1, 0);
            var y2 = new Vector3d(-1, 0, 0);
            var z2 = new Vector3d(0, 0, 1);
            var basis2 = new Basis3d(op2, x2, y2, z2);

            Transform rotationTransform = Transform.ChangeBasis(
                    basis1.X, basis1.Y, basis1.Z,
                    basis2.X, basis2.Y, basis2.Z);
        
            var translationTransform =
                Transform.Translation((basis1.Origin - basis2.Origin).Round(3));

            _pointConverter = new Point3dConverter(rotationTransform, translationTransform);
        }

        private Point3d Test1(Point3dConverter point3DConverter)
        {
            var ucs1_p1 = new Point3d(2, 1, 0);
            var ucs2_p1 = point3DConverter.ConvertToUCS2(ucs1_p1);
            Console.WriteLine(ucs2_p1);

            return ucs2_p1;
        }

        private void InverseTest1(Point3dConverter point3DConverter, Point3d ucs2Point)
        {
            Console.WriteLine(point3DConverter.ConvertToUCS1(ucs2Point));
        }

    }
}
