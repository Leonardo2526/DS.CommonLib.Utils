using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    public class Program1
    {
        static int GCD(int num1, int num2)
        {
            int Remainder;

            while (num2 != 0)
            {
                Remainder = num1 % num2;
                num1 = num2;
                num2 = Remainder;
            }

            return num1;
        }

        static int GCDInc(int num1, int num2, int inaccuracy)
        {
            int maxValue = 0;
            for (int i = 0; i < inaccuracy; i++)
            {
                var value1 = GCD(num1 + i, num2);
                var value2 = GCD(num1 - i, num2);
                if (value1 > maxValue) {  maxValue = value1; } 
                if (value2 > maxValue) {  maxValue = value2; }
            }

            return maxValue;
        }

        static int Main0(string[] args)
        {
            int x, y, inaccuracy;

            Console.Write("Enter the First Number: ");
            x = int.Parse(Console.ReadLine());

            Console.Write("Enter the Second Number: ");
            y = int.Parse(Console.ReadLine());

            Console.Write("Enter the inaccuracy: ");
            inaccuracy = int.Parse(Console.ReadLine());

            Console.Write("\nThe Greatest Common Divisor of ");

            Console.WriteLine("{0} and {1} is {2}", x, y, GCDInc(x, y, inaccuracy));

            Console.ReadLine();
            return 0;
        }
    }
}
