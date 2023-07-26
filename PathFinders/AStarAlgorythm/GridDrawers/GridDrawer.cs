using System;
using DS.PathSearch;


namespace AStarAlgorythm
{
    class GridDrawer : IGridDrawer
    {
        readonly PathSearch PathSearch;
        readonly Location Start;
        readonly Location Goal;

        public GridDrawer(PathSearch pathSearch, Location start, Location goal)
        {
            this.PathSearch = pathSearch;
            this.Start = start;
            this.Goal = goal;
        }

        public void DrawSquare(SquareGrid unpass)
        {
            // Печать массива cameFrom
            int z = 0;
            for (var y = 0; y < AStar.MaxGridPoint.Y; y++)
            {
                for (var x = 0; x < AStar.MaxGridPoint.X; x++)
                {
                    Location id = new Location(x, y, z);
                    Location ptr = id;
                    if (!PathSearch.cameFrom.TryGetValue(id, out ptr))
                    {
                        ptr = id;
                    }

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

                    if (ptr.X == x + 1) { Console.Write("\u2192 "); }
                    else if (ptr.X == x - 1) { Console.Write("\u2190 "); }
                    else if (ptr.Y == y + 1) { Console.Write("\u2193 "); }
                    else if (ptr.Y == y - 1) { Console.Write("\u2191 "); }
                    else { Console.Write("0 "); }
                }
                Console.WriteLine();
            }
        }

    }
}
