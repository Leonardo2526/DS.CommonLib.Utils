using DS.ClassLib.VarUtils.Points;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal static class NPoint3DTest
    {
        public static void RunTest1()
        {
            var p = new NPoint3D(0, 0, 1);
            Console.WriteLine(p);
        }
    }
}
