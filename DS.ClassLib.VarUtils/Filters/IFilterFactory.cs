using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Filters
{
    public interface IFilterFactory<TSource>
    {
       Func<TSource, bool> GetFilter();
    }
}
