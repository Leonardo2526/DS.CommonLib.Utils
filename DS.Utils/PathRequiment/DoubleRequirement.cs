using DS.PathSearch.GridMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PathSearch.PathRequiment
{
    public class DoubleRequirement : IDoublePathRequiment
    {
        public double Clearance { get; }

        public double MinAngleDistance { get; }
    }
}
