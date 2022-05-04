using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils
{
    public static class Extensions
    {
        /// <summary>
        /// Get normalize number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Return absolute and round to integer number</returns>
        public static int Normalize(this double number)
        {
            return (int)Math.Round(Math.Abs(number));
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="angleRad"></param>
        /// <returns>Return angle in degress.</returns>
        public static double RadToDeg(this double angleRad)
        {
            return angleRad * (180 / Math.PI);
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="angleRad"></param>
        /// <returns>Return angle in degress.</returns>
        public static double RadToDeg(this int angleRad)
        {
            return angleRad * (180 / Math.PI);
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="angleDeg"></param>
        /// <returns>Return angle in radians.</returns>
        public static double DegToRad(this double angleDeg)
        {
            return (angleDeg * Math.PI) / 180;
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="angleDeg"></param>
        /// <returns>Return angle in radians.</returns>
        public static double DegToRad(this int angleDeg)
        {
            return (angleDeg * Math.PI) / 180;
        }
    }
}
