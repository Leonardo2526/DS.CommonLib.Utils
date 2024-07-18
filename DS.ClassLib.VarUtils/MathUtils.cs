using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Useful methods for <see cref="Math"/>.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Compute standard deviation of <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double StandardDeviation(IEnumerable<double> arr)
        {
            var mean = arr.Average();
            return StandardDeviation(arr,  mean);
        }

        /// <summary>
        /// Compute standard deviation of <paramref name="arr"/>
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="mean"></param>
        /// <returns></returns>
        public static double StandardDeviation(IEnumerable<double> arr, double mean)
        {
            double square = 0;
            foreach (var item in arr)
            { square += Math.Pow(item - mean, 2); }

            return Math.Sqrt(square / arr.Count());
        }
    }
}
