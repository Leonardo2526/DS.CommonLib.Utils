using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.GridMap.d2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace DS.ClassLib.VarUtils
{
    public class ConsoleFloatMapDrawer: IMapDrawer
    {
        private readonly MapBase<float> _map;
        private readonly int _stepPrec;

        public ConsoleFloatMapDrawer(MapBase<float> map)
        {
            _map = map;
            _stepPrec = map.StepPrec;
        }

        public void Draw()
        {
            int z = 0;
            for (var y = 0; y <= (int)(_map.Matrix.GetUpperBound(1)); y++)
            {
                for (var x = 0; x <= (int)(_map.Matrix.GetUpperBound(0)); x++)
                {
                    if (_map.Matrix[x, y, z] == 8)
                    {
                        Console.Write("A ");
                        continue;
                    }
                    else if (_map.Matrix[x, y, z] == 9)
                    {
                        Console.Write("Z ");
                        continue;
                    }
                    else if (_map.Matrix[x, y, z] == 5)
                    {
                        Console.Write("* ");
                        continue;
                    }
                    else if (_map.Matrix[x, y, z] == 1) 
                    {
                        Console.Write("| "); 
                        continue; 
                    }
                    else { Console.Write("0 "); }
                }
                Console.WriteLine();
            }
        }
    }
}
