using System;

namespace DS.MainUtils
{
    public static class NumberExtensions
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


        /// <summary>
        /// Check if number and can be divided to devisibleNumber.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="devisibleNumber"></param>
        /// <returns>Return true if number can be divided to devisibleNumber without rest. Return false if it isn't.</returns>
        public static bool IsDevisible(this int input, int devisibleNumber)
        {
            if (input % devisibleNumber == 0)
            {
                return true;
            }

            return false;
        }
    }
}
