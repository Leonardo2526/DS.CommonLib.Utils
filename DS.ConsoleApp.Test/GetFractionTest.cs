using DS.ClassLib.VarUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal static class GetFractionTest
    {
        public static void Run()
        {
            decimal dnumber;
            //dnumber = 0.055M;

            double number = 1 / Math.Sqrt(3);
            double inaccuracy = 0.017;

            //double number = 0.57;
            //double inaccuracy = 0;
            //dnumber = (decimal)number;

            number =Math.Round(number, 3);
            var result =  FractionConverter.Convert((decimal)number, true, 0.001M, (decimal)inaccuracy);

            //var (numerator, denominator) = number.GetFraction(inaccuracy);

            Console.WriteLine(result);
            //Console.WriteLine(numerator + " / " + denominator);
        }
    }
}
