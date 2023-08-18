using DS.PathSearch;
using System;
using System.Collections.Generic;


namespace AStarAlgorythm
{
    public class PathSearch
    {
        public Dictionary<Location, Location> cameFrom
            = new Dictionary<Location, Location>();
        public Dictionary<Location, int> costSoFar
            = new Dictionary<Location, int>();
        public List<Location> Path
            = new List<Location>();

        // Примечание: обобщённая версия A* абстрагируется от Location
        // и Heuristic
        static public int Heuristic(Location a, Location b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        public PathSearch(IWeightedGraph<Location> graph, Location start, Location goal)
        {
            var frontier = new PriorityQueue<Location>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal))
                {
                    Path = Reconstruct_path(cameFrom, start, goal);
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    int newCost = costSoFar[current]
                        + graph.Cost(current, next);
                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        int priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
        }
        static List<Location> Reconstruct_path(Dictionary<Location, Location> cameFrom, Location start, Location goal)
        {
            Location current = goal;
            List<Location> path = new List<Location>();
            path.Add(current);
            while (!current.Equals(start))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            return path;
        }


    }

}
