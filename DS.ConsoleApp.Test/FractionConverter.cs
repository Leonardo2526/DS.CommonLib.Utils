using DS.ClassLib.VarUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.ClassLib.VarUtils.Strings;

namespace DS.ConsoleApp.Test
{
    internal class FractionConverter
    {
        public static string Convert(decimal pvalue,
    bool skip_rounding = false, decimal dplaces = (decimal)0, decimal inaccuracy = 0)
        {
            decimal value = pvalue;
            decimal inaccuracyValue = inaccuracy;

            if (!skip_rounding)
            {
                value = FractionConverter.DecimalRound(pvalue, dplaces);
                inaccuracyValue = FractionConverter.DecimalRound(inaccuracy, dplaces);
            }

            if (value == Math.Round(value, 0)) // whole number check
                return value.ToString();

            // get the whole value of the fraction
            decimal mWhole = Math.Truncate(value);

            // get the fractional value
            decimal mFraction = value - mWhole;

            // initialize a numerator and denomintar
            uint mNumerator = 0;
            uint mDenomenator = 1;
            uint mInaccuracy = 0;

            // ensure that there is actual a fraction
            if (mFraction > 0m)
            {
                // convert the value to a string so that
                // you can count the number of decimal places there are
                string strFraction = mFraction.ToString().Remove(0, 2);

                // store the number of decimal places
                uint intFractLength = (uint)strFraction.Length;


                // set the numerator to have the proper amount of zeros
                mNumerator = (uint)Math.Pow(10, intFractLength);

                // parse the fraction value to an integer that equals
                // [fraction value] * 10^[number of decimal places]
                uint.TryParse(strFraction, out mDenomenator);

                if(inaccuracyValue != 0)
                {
                    var insValue = (double)inaccuracyValue;
                    var (num1, num2) = insValue.SetEqualFractLength((double)value);
                    var fractionNum = num1.GetFraction();
                    uint.TryParse(fractionNum, out mInaccuracy);
                }

                // get the greatest common divisor for both numbers
                uint gcd = GCDInc(mDenomenator, mNumerator, mInaccuracy);
                //uint gcd = GreatestCommonDivisor(mDenomenator, mNumerator);

                // divide the numerator and the denominator by the greatest common divisor
                mNumerator = mNumerator / gcd;
                if(gcd> mDenomenator) { mDenomenator = gcd; }
                mDenomenator = mDenomenator / gcd;
            }

            // create a string builder
            StringBuilder mBuilder = new StringBuilder();

            // add the whole number if it's greater than 0
            if (mWhole > 0m)
            {
                mBuilder.Append(mWhole);
            }

            // add the fraction if it's greater than 0m
            if (mFraction > 0m)
            {
                if (mBuilder.Length > 0)
                {
                    mBuilder.Append(" ");
                }

                mBuilder.Append(mDenomenator);
                mBuilder.Append("/");
                mBuilder.Append(mNumerator);
            }

            return mBuilder.ToString();
        }

        private static uint GCD(uint num1, uint num2)
        {
            uint Remainder;

            while (num2 != 0)
            {
                Remainder = num1 % num2;
                num1 = num2;
                num2 = Remainder;
            }

            return num1;
        }

        private static uint GCDInc(uint num1, uint num2, uint inaccuracy)
        {
            uint maxValue = 0;
            for (uint i = 0; i <= inaccuracy; i++)
            {
                uint value1 = GCD(num1 + i, num2);
                uint value2 = GCD(num1 - i, num2);
                if (value1 > maxValue) { maxValue = value1; }
                if (value2 > maxValue) { maxValue = value2; }
            }

            return maxValue;
        }

        private static uint GreatestCommonDivisor(uint valA, uint valB)
        {
            // return 0 if both values are 0 (no GSD)
            if (valA == 0 &&
              valB == 0)
            {
                return 0;
            }
            // return value b if only a == 0
            else if (valA == 0 &&
                  valB != 0)
            {
                return valB;
            }
            // return value a if only b == 0
            else if (valA != 0 && valB == 0)
            {
                return valA;
            }
            // actually find the GSD
            else
            {
                uint first = valA;
                uint second = valB;

                while (first != second)
                {
                    if (first > second)
                    {
                        first = first - second;
                    }
                    else
                    {
                        second = second - first;
                    }
                }

                return first;
            }

        }

        // Rounds a number to the nearest decimal.
        // For instance, carpenters do not want to see a number like 4/5.
        // That means nothing to them
        // and you'll have an angry carpenter on your hands
        // if you ask them cut a 2x4 to 36 and 4/5 inches.
        // So, we would want to convert to the nearest 1/16 of an inch.
        // Example: DecimalRound(0.8, 0.0625) Rounds 4/5 to 13/16 or 0.8125.
        private static decimal DecimalRound(decimal val, decimal places)
        {
            string sPlaces = FractionConverter.Convert(places, true);
            string[] s = sPlaces.Split('/');

            if (s.Count() == 2)
            {
                int nPlaces = System.Convert.ToInt32(s[1]);
                decimal d = Math.Round(val * nPlaces);
                return d / nPlaces;
            }

            return val;
        }
    }
}
