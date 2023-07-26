using System;
using System.Collections.Generic;
using System.Drawing;
using DS.PathSearch;



namespace FrancoGustavo
{
    class GridDrawer : IGridDrawer
    {
        readonly int[, ,] Matrix;
        readonly List<PathFinderNode> Path;

        public GridDrawer(int[, ,] mMatrix, List<PathFinderNode> mPath)
        {
            Matrix = mMatrix;
            Path = mPath;
        }

        public void DrawSquare()
        {
            for (var z = 0; z <= Matrix.GetUpperBound(2); z++)
            {
                for (var y = 0; y <= Matrix.GetUpperBound(1); y++)
                {
                    for (var x = 0; x <= Matrix.GetUpperBound(0); x++)
                    {
                        bool pt = false;
                        if (Matrix[x, y, z] == 1) { Console.Write("# "); continue; }
                        else if (Matrix[x, y, z] == 8) { Console.Write("A "); continue; }
                        else if (Matrix[x, y, z] == 9) { Console.Write("Z "); continue; }

                        if (Path != null)
                        {
                            foreach (PathFinderNode item in Path)
                            {
                                if (item.X == x && item.Y == y && item.Z == z)
                                {
                                    Console.Write("* ");
                                    pt = true;
                                    break;
                                }
                            }
                        }

                        if (pt)
                            continue;
                        else { Console.Write("0 "); }
                    }
                    Console.WriteLine();
                }
            }
          
        }

    }
}
