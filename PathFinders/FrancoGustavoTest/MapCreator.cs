using DS.PathSearch.GridMap;
using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindTest
{
    class MapCreator : IMap
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public int[,,] Matrix { get; set; }
    }
}
