using QuickGraph.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DS.ClassLib.VarUtils;

namespace ConsoleApp1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var types = new List<Type>
         {
             typeof(int), typeof(Class1)
         };


            var e = new Class1();
            var s = "test";
            var i = 0;

            Console.WriteLine(e.IsTypeElement(types));
            Console.WriteLine(s.IsTypeElement(types));
            Console.WriteLine(i.IsTypeElement(types));

            Console.WriteLine(types.Contains(e.GetType()));
            Console.WriteLine(types.Contains(s.GetType()));
            Console.WriteLine(types.Contains(i.GetType()));


            //Test.Run2();
            Console.ReadLine();
        }
    }

    class Class1
    {

    }
}
