using DS.ClassLib.VarUtils.Directions;
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
        IDirectionFactory _factory;

        public UserDirectionFactoryTest()
        {
            _factory = new UserDirectionFactory();
            var dirs = Run();
            Output();
        }

        private List<Vector3D> Run()
        {
            var angles = new List<int> { 30};
            var main = new Vector3D(0, 1, 0);
            //var main = new Vector3D(1, 1, 0);
            var normal = new Vector3D(0, 0, 1);

            _factory.Build(main, normal, angles);

            return _factory.Directions;
        }

        private void Output()
        {
            UserDirectionFactory userDirectionFactory = _factory as UserDirectionFactory;
            if(userDirectionFactory == null) { return; }

            var directions = userDirectionFactory.Plane1_Directions;
            Console.WriteLine("Plane1_Directions: ");
            foreach (var dir in directions)
            {Console.WriteLine( $"({dir})" ); }

            directions = userDirectionFactory.Plane2_Directions;
            Console.WriteLine("\nPlane2_Directions: ");
            foreach (var dir in directions)
            { Console.WriteLine($"({dir})"); }

            directions = userDirectionFactory.Directions;
            Console.WriteLine("\nAll_Directions: ");
            foreach (var dir in directions)
            { Console.WriteLine($"({dir})"); }
        }
    }
}
