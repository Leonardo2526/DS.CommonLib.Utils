using System;
using System.Collections.Generic;
using FrancoGustavo;

namespace HPA
{
    class AbstractMapDrawer : IMatrixDrawer

    {
        readonly int[,,] Matrix;

        public AbstractMapDrawer(int[,,] mMatrix)
        {
            Matrix = mMatrix;
        }

        public void Draw()
        {
            int z = 0;
            for (var y = 0; y <= Matrix.GetUpperBound(1); y++)
            {
                for (var x = 0; x <= Matrix.GetUpperBound(0); x++)
                {
                    if (Matrix[x, y, z] == 8)
                    {
                        Console.Write("A ");
                        continue;
                    }
                    else if (Matrix[x, y, z] == 9)
                    {
                        Console.Write("Z ");
                        continue;
                    }
                    else if (Matrix[x, y, z] == 5)
                    {
                        Console.Write("N ");
                        continue;
                    }
                  

                    if (Matrix[x, y, z] == 1) { Console.Write("# "); continue; }
                    else { Console.Write("0 "); }
                }
                Console.WriteLine();
            }
        }
    }
}
