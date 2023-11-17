using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Solutions
{
    public class Solution<T> : ISolution
    {
        public Solution(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }

}
