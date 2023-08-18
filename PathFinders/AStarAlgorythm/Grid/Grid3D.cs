using DS.PathSearch;
using System.Collections.Generic;
using AStarAlgorythm.CLZ;


namespace AStarAlgorythm
{
    public class Grid3D : IWeightedGraph<Location>
    {
        // Примечания по реализации: для удобства я сделал поля публичными,
        // но в реальном проекте, возможно, стоит следовать стандартному 
        // стилю и сделать их скрытыми.

        public static readonly Location[] DIRS = new[]
            {
                new Location(-1, 0, 0),
                new Location(0, 1, 0),
                new Location(1, 0, 0),
                new Location(0, -1, 0),
                new Location(0, 0, 1),
                new Location(0, 0, -1)
        };

        public int width, length, height;
        public HashSet<Location> walls = new HashSet<Location>();
        public HashSet<Location> forests = new HashSet<Location>();

        public Grid3D(int width, int length, int height)
        {
            this.width = width;
            this.length = length;
            this.height = height;
        } 

        public bool InBounds(Location id)
        {
            return 0 <= id.X && id.X < width
                && 0 <= id.Y && id.Y < length
                && 0 <= id.Z && id.Z < height;
        }

        public bool Passable(Location id)
        {
            return !walls.Contains(id);
        }
        public bool PassableCLZByBordersFor(Location id)
        {
            Location location = new Location(id.X, id.Y, id.Z);
            for (int i = 0; i < CLZCretor.CLZPoints.Count; i++)
            {
                Location item = CLZCretor.CLZPoints[i];
                location.X = id.X + item.X;
                location.Y = id.Y + item.Y;
                location.Z = id.Z + item.Z;

                if (walls.Contains(location))
                    return false;
            }
           
            return true;
        }


        public bool PassableCLZByBorders(Location id)
        {
            foreach (Location item in CLZCretor.CLZPoints)
            {
                Location location = new Location(id.X + item.X, id.Y + item.Y, id.Z + item.Z);
                if (walls.Contains(location))
                    return false;
            }

            return true;
        }

        public bool PassableCLZByDirection(Location id, Location next)
        {
            Location vector = new Location(next.X - id.X, next.Y - id.Y, next.Z - id.Z);
            CLZCretor cLZCretor = new CLZCretor(new CLZByDirection(vector));

            foreach (Location item in CLZCretor.CLZPoints)
            {
                Location location = new Location(id.X + item.X, id.Y + item.Y, id.Z + item.Z);
                if (walls.Contains(location))
                    return false;
            }

            return true;
        }

        public bool PassableCLZByMaxDistance(Location id)
        {
            double maxDistance = AStar.WidthClearanceRCS * 1.4;
            foreach (Location item in walls)
            {
                DistanceTo distanceTo = new DistanceTo(item, id);
                double distance = distanceTo.Distance;
                if (distance < maxDistance)
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
                if (InBounds(next) && Passable(next) && PassableCLZByBorders(next))
                    //if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }
}
