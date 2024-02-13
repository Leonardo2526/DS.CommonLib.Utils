using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Test
{
    [TestClass]
    public class LineContainsPointTest
    {
        [DataTestMethod]
        public void TestSinglePoint_001_ShouldPass()
        {
            var p1 = new Point3d();
            var p2 = new Point3d(3, 0, 0);
            var line = new Line(p1, p2);

            var testPoint = new Point3d(2, 0, 0);

            Assert.IsTrue(line.Contains(testPoint));
        }

        [DataTestMethod]
        [DataRow(new double[3] { 0, 0, 0 }, new double[3] { 5, 0, 0 }, new double[3] { 1, 0, 0 })]
        [DataRow(new double[3] { 0, 0, 0 }, new double[3] { 5, 0, 0 }, new double[3] { -10, 0, 0 })]
        public void TestMultiplePoints_002_ShouldPass(double[] linePointData1, double[] linePointData2, double[] testPointData)
            => Assert.IsTrue(IsContains(linePointData1, linePointData2, testPointData));



        [DataTestMethod]
        [DataRow(new double[3] { 0, 0, 0 }, new double[3] { 5, 0, 0 }, new double[3] { -1, 0, 0 })]
        public void TestMultiplePoints_003_ShouldFail(double[] linePointData1, double[] linePointData2, double[] testPointData)
            => Assert.IsFalse(IsContains(linePointData1, linePointData2, testPointData));

        private static bool IsContains(double[] linePointData1, double[] linePointData2, double[] testPointData)
        {
            var p1 = new Point3d(linePointData1[0], linePointData1[1], linePointData1[2]);
            var p2 = new Point3d(linePointData2[0], linePointData2[1], linePointData2[2]);
            var line = new Line(p1, p2);

            var testPoint = new Point3d(testPointData[0], testPointData[1], testPointData[2]);
            return line.Contains(testPoint);
        }
    }
}
