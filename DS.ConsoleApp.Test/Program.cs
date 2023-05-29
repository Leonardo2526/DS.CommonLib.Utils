using DS.ClassLib.FileSystemUtils;
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
            NPoint3DTest.RunTest1();
            Console.ReadLine();
        }
    }
}
