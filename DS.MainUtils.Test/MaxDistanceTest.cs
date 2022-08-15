using DS.ClassLib.VarUtils.Algorithms.Points;
using DS.ClassLib.VarUtils.Algorithms.Points.Strategies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Test
{
    [TestClass]
    public class MaxDistanceTest
    {
        [DataTestMethod]
        public void Test1()
        {
            var points = new List<PointModel>()
            { new PointModel(0,0,0), new PointModel(1,0,0), new PointModel(2,0,0)};

            var client = new MaxDistanceClient(points, new NaiveStrategy());
            double max = client.GetMaxDistance();
            Debug.WriteLine(max);
            Assert.IsTrue(max == 2);
        }

        [DataTestMethod]
        public void Test2()
        {
            var points = new List<PointModel>()
            {
                new PointModel(0,-100,0),
                new PointModel(0,10,0),
                new PointModel(0,0,50),
                new PointModel(100,0,0)
            };

            var client = new MaxDistanceClient(points, new NaiveStrategy());
            double max = client.GetMaxDistance();
            Debug.WriteLine(max);

            double d1 = Math.Round(client.Point1.DistanceTo(points.First()));
            double d2 = Math.Round(client.Point2.DistanceTo(points.First()));

            double min = Math.Min(d1, d2);
            Assert.IsTrue(min == 0);

            d1 = Math.Round(client.Point1.DistanceTo(points.Last()));
            d2 = Math.Round(client.Point2.DistanceTo(points.Last()));

            min = Math.Min(d1, d2);

            Assert.IsTrue(min == 0);
        }
    }
}
