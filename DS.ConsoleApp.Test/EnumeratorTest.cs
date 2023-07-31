using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Enumerables;
using Rhino.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class EnumeratorTest
    {
        public EnumeratorTest()
        {
            var dirIterator = CreateDirectionIterator();
            DirectionsOutput(dirIterator);

            dirIterator.Reset();
            dirIterator.ParentDir = new Vector3d(0, 1, 0);
            DirectionsOutput(dirIterator);

            dirIterator.Reset();
            dirIterator.ParentDir = new Vector3d(0, 0, 1);
            DirectionsOutput(dirIterator);

            dirIterator.Reset();
            dirIterator.ParentDir = new Vector3d(0, -1, 1);
            DirectionsOutput(dirIterator);

            //IEnumerable enumerableObject = CreatePlaneEnumerator();
            //IEnumerable enumerableObject = CreateAngleEnumerator();
            //var enumerator = enumerableObject.GetEnumerator();

            //PlaneOutput(enumerator);
            //AngleOutput(enumerator);

            //enumerator.Reset();
            //enumerator.MoveNext();
            //var current = enumerator.Current;
            //Console.WriteLine(current);
        }

        private IEnumerable<Plane> CreatePlaneEnumerator()
        {
            var planes = new List<Plane>();
            var plane1 = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1));
            var plane2 = new Plane(new Point3d(1,1,1), new Vector3d(1,0,0));
            planes.Add(plane1);
            planes.Add(plane2);

            var planeEnum = new PlaneEnumerable(planes);

            return planeEnum;
        }
        private void PlaneOutput(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())   // пока не будет возвращено false
            {
                var item = (Plane)enumerator.Current; // получаем элемент на текущей позиции
                Console.WriteLine(item.Normal);
            }
        }


        private IEnumerable<int> CreateAngleEnumerator()
        {
            var angles = new List<int>()
            {
                15, 30 , 45 ,60, 90
            };

            var planeEnum = new AngleEnumerable(angles, 360);

            return planeEnum;
        }
        private void AngleOutput(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())   // пока не будет возвращено false
            {
                var item = (int)enumerator.Current; // получаем элемент на текущей позиции
                Console.WriteLine(item);
            }
        }


        private DirectionIterator CreateDirectionIterator()
        {
            var planes = new List<Plane>();
            var plane1 = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1));
            var plane2 = new Plane(new Point3d(0, 0, 0), new Vector3d(1, 0, 0));
            planes.Add(plane1);
            planes.Add(plane2);

            var angles = new List<int>()
            {
               30
            };

            //var parentDir = new Vector3d(1, 0, 0);
            var parentDir = new Vector3d(0, 0, 0);

            var dirIterator = new DirectionIterator(planes, angles)
            {
                ParentDir = parentDir
            };

            return dirIterator;
        }

        private void DirectionsOutput(DirectionIterator dirIterator)
        {
            Console.WriteLine("\nParent direction is: " + dirIterator.ParentDir);
            while (dirIterator.MoveNext())   // пока не будет возвращено false
            {
                var item = (Vector3d)dirIterator.Current; // получаем элемент на текущей позиции
                Console.WriteLine(item);
            }
        }
    }
}
