using System;
using System.Collections.Generic;
using FrancoGustavo;

namespace HPA
{
    class MatrixDrawer : IMatrixDrawer

    {
        readonly int[,,] Matrix;

        public MatrixDrawer(int[,,] mMatrix)
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
                    else if (IfPathContainsPoint(x, y, z))
                    {
                        Console.Write("* ");
                        continue;
                    }

                    if (Matrix[x, y, z] == 1) { Console.Write("# "); continue; }
                    else { Console.Write("0 "); }
                }
                Console.WriteLine();
            }
        }

        private bool IfPathContainsPoint(int x, int y, int z)
        {
            if (AbstractGraph.Path != null)
            {
                foreach (AbstractNode pathNode in AbstractGraph.Path)
                {
                    if (pathNode.LocationL0.X == x && pathNode.LocationL0.Y == y && pathNode.LocationL0.Z == z)
                        return true;
                }
            }

            return false;
        }

    }
}
