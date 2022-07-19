using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils
{
    public static class ListIntersection
    {
        public static List<T> GetIntersections<T>(List<T> list1, List<T> list2)
        {
            var intersecion = list1.Intersect(list2).ToList();
            return intersecion;
        }
         
        public static List<T> GetNoIntersections<T>(List<T> list1, List<T> list2)
        {
            var NoIntersections = new List<T>();

            foreach (var one in list1)
            {
                if (!list2.Any(two => two.Equals(one)))
                {
                    NoIntersections.Add(one);
                }
            }
            return NoIntersections;
        }
    }
}
