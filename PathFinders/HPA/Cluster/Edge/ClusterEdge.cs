using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    struct ClusterEdge
    {
            public List<Location> NodesLocations { get; set; }

        public enum EdgeSide
        {
            Left,
            Right,
            Down,
            Up

        }
    }
}
