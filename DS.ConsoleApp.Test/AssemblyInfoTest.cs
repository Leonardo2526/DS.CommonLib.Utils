using DS.ClassLib.VarUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal static class AssemblyInfoTest
    {
        public static void Run()
        {
            //var assembly = Assembly.GetEntryAssembly();
            var assembly = Assembly.GetExecutingAssembly();
            //assembly = null;
            var version = assembly.Version();
            var buildTime = assembly.BuildTime();
          
            Console.WriteLine(assembly?.FullName);
            Console.WriteLine(version);
            Console.WriteLine(buildTime);
        }
    }
}
