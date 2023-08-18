using DS.PathSearch;
using DS.PathSearch.GridMap;
using FrancoGustavo;
using System.Collections.Generic;

namespace HPA
{
    class IntraEdge
    {
        readonly Cluster _cluster;
        private ClusterMap clusterMap = new ClusterMap();

        public IntraEdge(Cluster cluster)
        {
            this._cluster = cluster;
            clusterMap.Matrix = _cluster.Matrix;
        }

        public void SetWeight()
        {
            if (_cluster.Equals(AbstractGraph.StartNodeCluster))
                GetByStartGoal(AbstractGraph.StartNode);
            else if (_cluster.Equals(AbstractGraph.GoalNodeCluster))
                GetByStartGoal(AbstractGraph.GoalNode);
            else
                Get();
        }

        private void GetByStartGoal(AbstractNode abstractNode)
        {
            int j;
            for (j = 0; j < _cluster.AbstractNodes.Count; j++)
            {
                if (_cluster.AbstractNodes[j].Equals(abstractNode))
                    break;
            }

            for (int i = 0; i < _cluster.AbstractNodes.Count - 1; i++)
            {
                if (i == j)
                    continue;
                else
                {
                    int length = GetPathLength(_cluster.AbstractNodes[j], _cluster.AbstractNodes[i]);
                    if (length == 0)
                        continue;

                    AbstractGraph.WeightMatrix[_cluster.AbstractNodes[j].Id, _cluster.AbstractNodes[i].Id] = length;
                    AbstractGraph.WeightMatrix[_cluster.AbstractNodes[i].Id, _cluster.AbstractNodes[j].Id] = length;
                }
            }
        }

        private void Get()
        {
            foreach (AbstractNode startNode in _cluster.AbstractNodes)
            {
                foreach (AbstractNode goalNode in _cluster.AbstractNodes)
                {
                    if (AbstractGraph.WeightMatrix[startNode.Id, goalNode.Id] != 0 || startNode.Equals(goalNode))
                        continue;
                    else
                    {
                        int length = GetPathLength(startNode, goalNode);
                        if (length == 0)
                            continue;
                        AbstractGraph.WeightMatrix[startNode.Id, goalNode.Id] = length;
                        AbstractGraph.WeightMatrix[goalNode.Id, startNode.Id] = length;
                    }

                }
            }
        }

        private int GetPathLength(AbstractNode startNode, AbstractNode goalNode)
        {
            clusterMap.Start = startNode.LocationL1;
            clusterMap.Goal = goalNode.LocationL1;

            int count;

            //Call A*
            List<PathFinderNode> intraPath = FGAlgorythm.GetPathByMap(clusterMap, new PathRequiment2());

            if (intraPath == null)
                return 0;
            else
            {
                count = intraPath.Count - 1;

                //write path to AbstractGraph
                List<AbstractNode> path = new List<AbstractNode>();
                foreach (PathFinderNode item in intraPath)
                {
                    Location location = new Location(item.X, item.Y, item.Z);
                    AbstractNode abstractNode = new AbstractNode()
                    {
                        LocationL0 = LocationConvertor.L1ToL0(location, _cluster)
                    };

                    path.Add(abstractNode);
                }
                AbstractGraph.IntraPathes[startNode.Id, goalNode.Id] = path;
            }

            return count;
        }
    }
}
