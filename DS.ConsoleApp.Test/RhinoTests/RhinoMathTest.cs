using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test.RhinoTests
{
    internal class RhinoMathTest
    {
        public RhinoMathTest()
        {
            
        }

        private void UnitScateTest()
        {
            double v = RhinoMath.UnitScale(UnitSystem.Millimeters, UnitSystem.Feet);
            Console.WriteLine(304 * v);
        }
    }
}
