using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Enumerables
{
    public class AngleEnumerable : IEnumerable<int>
    {
        private readonly List<int> _angles;

        public AngleEnumerable(List<int> angles)
        {
            _angles = GetAngles(angles);
        }

        public AngleEnumerable(List<int> angles, int maxAngle)
        {
            _angles = GetAngles(angles, maxAngle);
        }

        public IEnumerator<int> GetEnumerator() => _angles.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _angles.GetEnumerator();

        private List<int> GetAngles(List<int> angles)
        {
            var itAngles = new List<int>();

            foreach (var angle in angles)
            {
                itAngles.Add(angle);
                itAngles.Add(-angle);
            }

            return itAngles;
        }

        private List<int> GetAngles(List<int> angles, int maxAngle)
        {
            var itAngles = new List<int>();

            foreach (var angle in angles)
            {
                int a = angle;
                while (a < maxAngle)
                {
                    if (!itAngles.Contains(a))
                    { itAngles.Add(a); }
                    a += angle;
                }
            }

            return itAngles.OrderBy(a => a).ToList();
        }
    }
}
