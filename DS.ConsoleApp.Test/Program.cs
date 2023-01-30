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
            //Task task = Task.Run(() => { LicensingTest.Run(); });

            LicensingTest.Run();
            Console.ReadLine();
            //Task task = Task.Run(() => { Console.ReadLine(); });
            //DialogFormUtils.OpenFormToRead();
        }
    }
}
