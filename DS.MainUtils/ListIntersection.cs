using System.Collections.Generic;
using System.Linq;

namespace DS.MainUtils
{
    public static class ListIntersection
    {
        public static List<int> GetIntersections(List<int> list1, List<int> list2)
        {
            var intersecion = list1.Intersect(list2).ToList();
            return intersecion;
        }

        public static List<int> GetNoIntersections(List<int> list1, List<int> list2)
        {
            var NoIntersections = new List<int>();

            foreach (var one in list1)
            {
                if (!list2.Any(two => two == one))
                {
                    NoIntersections.Add(one);
                }
            }
            return NoIntersections;
        }
    }
}
