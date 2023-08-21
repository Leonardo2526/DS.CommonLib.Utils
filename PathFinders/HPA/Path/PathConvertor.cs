using DS.PathSearch;
using DS.PathSearch.GridMap;
using FrancoGustavo;
using System.Collections.Generic;

namespace HPA
{
    struct PathConvertor
    {
        List<PathFinderNode> AbstractPath;

        public List<AbstractNode> Convert(List<PathFinderNode> abstractPath)
        {
            AbstractPath = abstractPath;
            foreach (PathFinderNode pathNode in AbstractPath)
            {
                Location location = new Location(pathNode.X, pathNode.Y, pathNode.Z);
                AbstractNode abstractNode = new AbstractNode
                {
                    LocationL0 = location,
                    Id = pathNode.Id
                };

                AbstractGraph.Path.Add(abstractNode);
            }

            return AbstractGraph.Path;
        }
    }
}
