using DS.ClassLib.FileSystemUtils;
using DS.ClassLib.VarUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //new UserDirectionFactoryTest();
            GetFractionTest.Run();
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{
        //    var number1 = -0.01;
        //    var number2 = 3460.1;
        //    (string num1, string num2) = number1.SetEqualFractLength(number2);
        //    Console.WriteLine(num1);
        //    Console.WriteLine(num2);
        //    Console.ReadLine();
        //}


        //static void Main(string[] args)
        //{
        //    //var number = 0.010;
        //    var number = 80.0555;
        //    var fractLength = number.FractionString().Length;
        //    var (wholeNumber, fractNumber) = number.Split();
        //    Console.WriteLine("The fractLength is: " + fractLength);
        //    Console.WriteLine("The wholeNumber is: " + wholeNumber);
        //    Console.WriteLine("The fractNumber is: " + fractNumber);
        //    //GetFractionTest.Run();
        //    Console.ReadLine();
        //}
    }
}
