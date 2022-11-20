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
            string defaultDir = "E:\\YandexDisk\\Олимпроект\\";
            //var specialFolder = Environment.ExpandEnvironmentVariables(defaultDir);
            var dialog = new FolderBrowserDialog() { SelectedPath = defaultDir };
            //var dialog = new FolderBrowserDialog() { RootFolder = Environment.SpecialFolder.Desktop };
            var res = dialog.ShowDialog(); 
            var sourceDir = res == DialogResult.OK ? dialog.SelectedPath: defaultDir;

            if (sourceDir is null || sourceDir == "") { return; }

            var list = Directory.EnumerateFiles(sourceDir, "*.rvt", SearchOption.AllDirectories).ToList();

            if (!list.Any())
            {
                Console.WriteLine("List is 0");
            }
            else
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }

            Console.ReadLine();
            //DialogFormUtils.OpenFormToRead();
        }
    }
}
