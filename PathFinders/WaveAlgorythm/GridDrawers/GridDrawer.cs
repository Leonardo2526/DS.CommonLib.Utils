using System;
using DS.System;

namespace WaveAlgorythm
{
    class GridDrawer : IGridDrawer
    {
        readonly Location Start;
        readonly Location Goal;

        public GridDrawer(Location start, Location goal)
        {
            this.Start = start;
            this.Goal = goal;
        }

        public void DrawSquare(SquareGrid unpass)
        {
            int z = 0;
            // Печать массива cameFrom
            for (var y = 0; y < PathSearch.MaxGridPoint.Y; y++)
            {
                for (var x = 0; x < PathSearch.MaxGridPoint.X; x++)
                {
                    Location id = new Location(x, y, z);
                    Location ptr = id;                 

                    if (id.Equals(Start))
                    {
                        Console.Write("A ");
                        continue;
                    }
                    else if (id.Equals(Goal))
                    {
                        Console.Write("Z ");
                        continue;
                    }

                    if (unpass.walls.Contains(id)) { Console.Write("# "); continue; }
                    if (PathSearch.Path.Contains(id)) { Console.Write("* "); continue; }
                    if (unpass.forests.Contains(id)) { Console.Write("| "); continue; }
                    else { Console.Write("0 "); }
                }
                Console.WriteLine();
            }
        }

    }
}
