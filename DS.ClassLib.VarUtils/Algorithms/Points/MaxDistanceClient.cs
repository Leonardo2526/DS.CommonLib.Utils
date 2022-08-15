using DS.ClassLib.VarUtils.Algorithms.Points.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Algorithms.Points
{
    public class MaxDistanceClient
    {
        private readonly List<PointModel> _points;
        private readonly MaxDistnaceStrategy _strategy;

        public MaxDistanceClient(List<PointModel> points, MaxDistnaceStrategy strategy)
        {
            _points = points;
            _strategy = strategy;
        }

        public PointModel Point1 { get; private set; }
        public PointModel Point2 { get; private set; }

        public double GetMaxDistance()
        {
           var max = _strategy.AlgorithmInterface(_points);
            Point1 = _strategy.Point1;
            Point2 = _strategy.Point2;

            return max;
        }
    }
}
