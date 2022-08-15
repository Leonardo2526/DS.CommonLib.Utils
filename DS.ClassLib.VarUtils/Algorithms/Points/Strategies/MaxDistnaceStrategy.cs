using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Algorithms.Points.Strategies
{
    public abstract class MaxDistnaceStrategy
    {
        public PointModel Point1 { get; protected set; }
        public PointModel Point2 { get; protected set; }

        public abstract double AlgorithmInterface(List<PointModel> points);
    }
}
