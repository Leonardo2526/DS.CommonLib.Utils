using DS.ClassLib.VarUtils.Directions;
using DS.ClassLib.VarUtils.Points;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ConsoleApp.Test
{
    internal class UserDirectionFactoryTest
    {
        UserDirectionFactory _factory;
        private OrthoBasis _basisMultiplicator;

        public UserDirectionFactoryTest()
        {
            _factory = new UserDirectionFactory();
            Run();

            var planeDir = _factory.Plane2_Directions;

            Output(planeDir);

            Console.WriteLine();
            Console.WriteLine("\nMultiple: \n");

            var mDirs = planeDir.Multiply(_basisMultiplicator);
            foreach (var dir in mDirs)
            { Console.WriteLine($"({dir})"); }
        }

        private List<Vector3D> Run()
        {
            var angles = new List<int> { 30 };
            var main = new Vector3D(1, 0, 0);
            main.Normalize();
            //var main = new Vector3D(1, 1, 0);
            var normal = new Vector3D(0, 1, 0);
            normal.Normalize();
         
            var crossProduct = Vector3D.CrossProduct(main, normal);

            var basis = new OrthoNormBasis(main, normal, crossProduct);

            _basisMultiplicator = new OrthoBasis(
                Vector3D.Multiply(basis.X, 2),
                Vector3D.Multiply(basis.Y, 3),
                Vector3D.Multiply(basis.Z, 5)
                );

            _factory.Build(basis, angles);

            return _factory.Directions;
        }

        private void Output(List<Vector3D> vectors)
        {
            foreach (var dir in vectors)
            {Console.WriteLine( $"({dir})" ); }
        }
    }
}
