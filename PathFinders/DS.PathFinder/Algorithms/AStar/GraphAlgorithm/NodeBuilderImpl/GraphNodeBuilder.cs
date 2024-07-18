using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathFinder.Algorithms.AStar.GraphAlgorithm
{
    public partial class GraphNodeBuilder
    {
        private readonly INodeVectoryFactory _nodeVectorFactory;
        private readonly Func<double> _heuristicFormula;

        public GraphNodeBuilder(INodeVectoryFactory nodeVectorFactory, Func<double> heuristicFormula)
        {
            _nodeVectorFactory = nodeVectorFactory;
            _heuristicFormula = heuristicFormula;
        }

        public ISpecifyNodeLocation Create()
            =>new LocationSpecificator(_nodeVectorFactory, _heuristicFormula);
        
    }
}
