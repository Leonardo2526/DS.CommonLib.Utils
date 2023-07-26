using DS.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    interface IDataInput
    {
        Location Start { get; set; }
        Location Goal { get; set; }
        Location MaxGridPoint { get; set; }
        List<Location> Unpassablelocations { get; set; }
    }
}
