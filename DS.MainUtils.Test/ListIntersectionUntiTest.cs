using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils.Test
{
    [TestClass]
    public class ListIntersectionUntiTest
    {
        [DataTestMethod]
        //[DataRow(new int[] { 1, 2, 3 }, new int[] { 30, 2, 10 })]
        [DataRow(new int[] { 2, 10}, new int[] { 30, 2, 10 })]
        //[DataRow(new int[] {1}, new int[] {30,2,10})]
        public void IntIntersectionsTest(int[] list1, int[] list2)
        {          
            List<int> intersection = ListIntersection.GetIntersections(list1.ToList(), list2.ToList());
            Assert.IsTrue(intersection.Count == 2);
            //Assert.IsTrue(intersection.First() == 2);
        }
    }
}
