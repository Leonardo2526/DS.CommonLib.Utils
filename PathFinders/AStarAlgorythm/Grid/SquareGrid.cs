using AStarAlgorythm.CLZ;
using DS.PathSearch;
using System.Collections.Generic;

namespace AStarAlgorythm
{
    public class SquareGrid : IWeightedGraph<Location>
    {
        // Примечания по реализации: для удобства я сделал поля публичными,
        // но в реальном проекте, возможно, стоит следовать стандартному
        // стилю и сделать их скрытыми.

        public static readonly Location[] DIRS = new[]
            {
                new Location(-1, 0, 0),
                new Location(0, 1, 0),
                new Location(1, 0, 0),
                new Location(0, -1, 0)
        };

        public int width, height;
        public HashSet<Location> walls = new HashSet<Location>();
        public HashSet<Location> forests = new HashSet<Location>();

        public SquareGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool InBounds(Location id)
        {
            return 0 <= id.X && id.X < width
                && 0 <= id.Y && id.Y < height;
        }

        public bool Passable(Location id)
        {
            return !walls.Contains(id);
        }
        public bool PassableCLZ(Location id)
        {
            foreach (Location item in CLZCretor.CLZPoints)
            {
                Location location = new Location(id.X + item.X, id.Y + item.Y, id.Z + item.Z);
                if (walls.Contains(location))
                    return false;
            }

            return true;
        }

        public int Cost(Location a, Location b)
        {
            return forests.Contains(b) ? 5 : 1;
        }

        public IEnumerable<Location> Neighbors(Location id)
        {
            foreach (var dir in DIRS)
            {
                Location next = new Location(id.X + dir.X, id.Y + dir.Y, id.Z + dir.Z);
                if (InBounds(next) && Passable(next))
                //if (InBounds(next) && Passable(next) && PassableCLZ(next))
                    {
                    yield return next;
                }
            }
        }
    }
}
