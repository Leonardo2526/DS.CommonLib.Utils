using System.Collections.Generic;

namespace HPA
{
    class EdgesPairsBuilder
    {
        public Cluster Parent;
        public List<Cluster> Neighbors;

        public EdgesPairsBuilder(Cluster parent, List<Cluster> neighbors)
        {
            Parent = parent;
            Neighbors = neighbors;
        }

        List<EdgesPair> EdgesPairsList { get; set; } = new List<EdgesPair>();


        public List<EdgesPair> Build()
        {
            EdgesPair edgesPair = new EdgesPair();

            foreach (Cluster neighbor in Neighbors)
            {
                if ((neighbor.Location.X-Parent.Location.X) <0)
                {
                    edgesPair = new EdgesPair(Parent, neighbor,
              Parent.GetEdge(ClusterEdge.EdgeSide.Left),
              neighbor.GetEdge(ClusterEdge.EdgeSide.Right));
                }
                else if((neighbor.Location.X - Parent.Location.X) > 0)
                {
                    edgesPair = new EdgesPair(Parent, neighbor,
               Parent.GetEdge(ClusterEdge.EdgeSide.Right), neighbor.GetEdge(ClusterEdge.EdgeSide.Left));
                }
                else if((neighbor.Location.Y - Parent.Location.Y) > 0)
                {
                    edgesPair = new EdgesPair(Parent, neighbor,
               Parent.GetEdge(ClusterEdge.EdgeSide.Down), neighbor.GetEdge(ClusterEdge.EdgeSide.Up));
                }
                else if ((neighbor.Location.Y - Parent.Location.Y) < 0)
                {
                    edgesPair = new EdgesPair(Parent, neighbor,
               Parent.GetEdge(ClusterEdge.EdgeSide.Up), neighbor.GetEdge(ClusterEdge.EdgeSide.Down));
                }

                EdgesPairsList.Add(edgesPair);

            }

            return EdgesPairsList;
        }

    }
}
