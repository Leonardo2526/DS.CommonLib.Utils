using Rhino;
using Rhino.Geometry;
using System;

namespace DS.ClassLib.VarUtils
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

        /// <summary>
        /// Specifies if <paramref name="number"/> is natural number or not.
        /// </summary>
        /// <remarks>
        /// <paramref name="includeZero"/> specifies if '0' can be included to natural number checker or not.
        /// </remarks>
        /// <param name="number"></param>
        /// <param name="includeZero"></param>
        /// <returns><see langword="true"/> if <paramref name="number"/> is natural number. Otherwise returns <see langword="false"/>.</returns>
        public static bool IsNatural(this int number, bool includeZero = true)
        {          
            return CheckNumber(number, includeZero);

            static bool CheckNumber(int number, bool includeZero)
            {
                if (!includeZero)
                { if (number > 0) { return true; } }
                else if (number >= 0) { return true; }

                return false;
            }
        }

        /// <summary>
        /// Split <paramref name="number"/> to whole and fraction parts.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// <paramref name="number"/> splitted on two int value.
        /// </returns>
        public static (int wholeNumber, int fractNumber) IntSplit(this double number)
        {
            int mWhole = (int)Math.Truncate(number);
            int mFractNumber = number.FractionNumber();

            return (mWhole, mFractNumber);
        }

        /// <summary>
        /// Split <paramref name="number"/> to whole and fraction parts.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// <paramref name="number"/> splitted on two double values.
        /// </returns>
        public static (double wholeNumber, double fractNumber) Split(this double number)
        {
            int mWhole = (int)Math.Truncate(number);
            return (mWhole, number - mWhole);
        }

        public static int FractionNumber(this double number)
        {
            string strNumber = number.FractionString();
            int.TryParse(strNumber, out int mNumber);

            return mNumber;
        }

        public static string FractionString(this double number)
        {
            string strNumber = number.ToString();
            int index = strNumber.IndexOf(",");
            if (index >= 0)
            { strNumber = strNumber.Substring(index + 1); }

            return strNumber;
        }

        public static (string num1, string num2) SetEqualFractLength(this double number1, double number2)
        {
            int fractionLength1 = number1.FractionString().Length;
            int fractionLength2 = number2.FractionString().Length;
            int maxLength = Math.Max(fractionLength1, fractionLength2);

            return 
                (GetStringNumber(number1, fractionLength1, maxLength), 
                GetStringNumber(number2, fractionLength2, maxLength));

            string GetStringNumber(double number, int fractionLength, int maxLength)
            {
                if (fractionLength == maxLength) { return number.ToString(); }
                else 
                { 
                    int delta = maxLength - fractionLength;
                    var stringNum = number.ToString();
                    for (int i = 0; i < delta; i++)
                    {
                        stringNum += "0";
                    }
                    return stringNum;
                }
            }
        }

        public static (int numerator, int denominator) GetFraction(this double number, double inaccuracy =  0)
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                for (int j = 1; j < int.MaxValue; j++)
                {
                    var fraction = i / j;
                    if(number - inaccuracy <= fraction && fraction >=  number + inaccuracy)
                    { return (i,j); }
                }
            }

            return (0,0);   
        }

        /// <summary>
        /// Get <paramref name="number"/> sign.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// Returns 1 if <paramref name="number"/> sing is greater than zero. 
        /// <para>
        /// Otherwise returns -1.
        /// </para>
        /// <para>
        /// If number value is zero returns 0.
        /// </para>
        /// </returns>
        public static int Sign(this double number)
        {
            if(number == 0) return 0;
            return (int)(number / (Math.Abs(number)));
        }

        /// <summary>
        /// Specifies if <paramref name="a"/> is equal to <paramref name="b"/> 
        /// with given <paramref name="tolerance"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns>
        /// <see langword="true"/> if difference beteween <paramref name="a"/> 
        /// and <paramref name="b"/> is less <paramref name="tolerance"/>.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsAlmostEqual(
            this double a,
            double b,
            double tolerance = RhinoMath.ZeroTolerance)
            => Math.Abs(a - b) < tolerance;
    }

}
